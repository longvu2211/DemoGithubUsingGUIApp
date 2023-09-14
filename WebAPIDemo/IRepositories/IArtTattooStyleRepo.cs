using WebAPIDemo.Models;

namespace WebAPIDemo.IRepositories
{
    public interface IArtTattooStyleRepo
    {
        Task<ICollection<ArtTattooStyle>> GetArtTattooStyles();
        Task<ArtTattooStyle> GetArtTattooStyle(int id);
        Task<ICollection<ArtTattooStyle>> SearchArtTattooStylesByName(string name);
        Task<bool> HasArtTattooStyle(int id);
        Task<bool> CreateArtTattooStyle(ArtTattooStyle artTattooStyle);
        Task<bool> Save();
    }
}
