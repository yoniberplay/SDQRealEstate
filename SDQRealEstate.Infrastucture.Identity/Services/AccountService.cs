using SDQRealEstate.Core.Application.Dtos.Account;
using SDQRealEstate.Core.Application.Enums;
using SDQRealEstate.Core.Application.Interfaces.Services;
using SDQRealEstate.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDQRealEstate.Core.Application.Helpers;
using SDQRealEstate.Core.Domain.Settings;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace SDQRealEstate.Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly JWTSettings _jwtSettings;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IEmailService emailService,
            IOptions<JWTSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _jwtSettings = jwtSettings.Value;
        }
        #region Privates
        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName)
                ,new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                ,new Claim(JwtRegisteredClaimNames.Email,user.Email)
                ,new Claim("uid",user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow
            };
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var ramdomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(ramdomBytes);

            return BitConverter.ToString(ramdomBytes).Replace("-", "");
        }

        #endregion

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            AuthenticationResponse response = new();
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No hay cuentas con {request.Email}";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Credenciales no validas {request.Email}";
                return response;
            }
            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Cuenta no confirmada para {request.Email}";
                return response;
            }

            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);

            response.Id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            response.Roles = rolesList.ToList();
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            response.IsVerified = user.EmailConfirmed;

            var refreshToken = GenerateRefreshToken();
            response.RefreshToken = refreshToken.Token;
             

            return response;
        }

        public async Task<RegisterResponse> RegisterClientUserAsync(RegisterRequest request, String origin)
        {
            RegisterResponse response = new()
            {
                HasError = false
            };

            var ExistUserName = await _userManager.FindByNameAsync(request.UserName);
            if (ExistUserName != null)
            {
                response.HasError = true;
                response.Error = $"Usuario '{request.UserName}' ya esta en uso.";
                return response;
            }

            var ExistMail = await _userManager.FindByEmailAsync(request.Email);
            if (ExistMail != null)
            {
                response.HasError = true;
                response.Error = $"Email '{request.Email}' ya esta en uso.";
                return response;
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.UserName,
                LastName = request.LastName,
                FirstName = request.FirstName,
                PhoneNumber = request.Phone,
                Foto = ""

            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                if(request.File != null)
                {
                    var foto = AdmFiles.UploadFile(request.File, user.Id, "Users");
                    user.Foto = foto;
                }else user.Foto = "NINGUNA";

                if (request.Tipo.Equals("Cliente"))
                {
                    await _userManager.AddToRoleAsync(user, Roles.Cliente.ToString());
                    var verificacion = await SendVerificationEmailUri(user, origin);
                    await _emailService.SendAsync(new Core.Application.Dtos.Email.EmailRequest()
                    {
                        To = user.Email,
                        Body = $"Por favor valide su cuenta ingresando a URL {verificacion}",
                        Subject = "Confirm registration"
                    });

                } else if (request.Tipo.Equals("Agente"))
                {
                    await _userManager.AddToRoleAsync(user, Roles.Agente.ToString());
                    await _emailService.SendAsync(new Core.Application.Dtos.Email.EmailRequest()
                    {
                        To = user.Email,
                        Body = $"Gracias por unirte a nuestra familia, tu cuenta sera revisada y activada por un admistrador en un plazo de 24 horas.",
                        Subject = "Confirm registration"
                    });
                }
                else if (request.Tipo.Equals("Desarrollador"))
                {
                    user.EmailConfirmed = true;
                    await _userManager.AddToRoleAsync(user, Roles.Desarrollador.ToString());
                    await _emailService.SendAsync(new Core.Application.Dtos.Email.EmailRequest()
                    {
                        To = user.Email,
                        Body = $"Gracias por unirte a nuestra familia de Desarrolladores.",
                        Subject = "Confirm registration"
                    });
                }
                else
                {
                    user.EmailConfirmed = true;
                    await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());
                }

                await _userManager.UpdateAsync(user);

            }
            else
            {
                response.HasError = true;
                response.Error = "Ha ocurrido un error al momento del registro.";
                return response;
            }

            return response;
        }

        public async Task<string> ConfirmAccountAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return $"No hay cuentas registradas con este usuario";
            }

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return $"Cuenta confirmada para {user.Email}. ahora puedes disfrutar de nuestra app.";
            }
            else
            {
                return $"Ha ocurrido un error durante la confirmacion de {user.Email}.";
            }
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin)
        {
            ForgotPasswordResponse response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No existe cuenta registrada o activa con {request.Email}";
                return response;
            }

            var verificationUri = await SendForgotPasswordUri(user, origin);

            await _emailService.SendAsync(new Core.Application.Dtos.Email.EmailRequest()
            {
                To = user.Email,
                Body = $"Por favor cambie su password visitando URL {verificationUri}",
                Subject = "reestablecer password"
            });


            return response;
        }

        private async Task<string> SendVerificationEmailUri(ApplicationUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ConfirmEmail";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "token", code);

            return verificationUri;
        }

        private async Task<string> SendForgotPasswordUri(ApplicationUser user, string origin)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ResetPassword";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "token", code);

            return verificationUri;
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request)
        {
            ResetPasswordResponse response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No hay cuentas registradas con {request.Email}";
                return response;
            }

            request.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Ha ocurrido un error al intentar cambiar la clave";
                return response;
            }

            return response;
        }





    }
}
