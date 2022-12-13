using SDQRealEstate.Core.Application.ViewModels.Favorita;
using SDQRealEstate.Core.Domain.Entities;

namespace SDQRealEstate.Core.Application.Interfaces.Services
{
    public interface IFavoritaService : IGenericService<SaveFavoritaViewModel, FavoritaViewModel, Favorita>
    {
        Task<List<FavoritaViewModel>> GetAllWithProperties();
    }
}
