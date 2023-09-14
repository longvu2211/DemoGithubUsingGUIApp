using WebAPIDemo.Models;

namespace WebAPIDemo.IRepositories
{
    public interface IArtTattooServiceRepo
    {
        Task<ICollection<ArtTattooService>> GetArtTattooServices();
        Task<bool> CreateArtTattooService(ArtTattooService artTattooService);
        Task<bool> Save();
    }
}
