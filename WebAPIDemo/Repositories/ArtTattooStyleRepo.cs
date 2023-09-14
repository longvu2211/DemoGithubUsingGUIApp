using Microsoft.EntityFrameworkCore;
using WebAPIDemo.IRepositories;
using WebAPIDemo.Models;

namespace WebAPIDemo.Repositories
{
    public class ArtTattooStyleRepo : IArtTattooStyleRepo
    {
        private readonly ArtTattoo2023DbContext _context;

        public ArtTattooStyleRepo(ArtTattoo2023DbContext context)
        {
            this._context = context;
        }
        public async Task<bool> CreateArtTattooStyle(ArtTattooStyle artTattooStyle)
        {
            _context.Add(artTattooStyle);
            return await Save();
        }

        public async Task<ArtTattooStyle> GetArtTattooStyle(int id)
        {
            return await _context.ArtTattooStyles.FirstOrDefaultAsync(a => a.TattooStyleId == id);
        }

        public async Task<ICollection<ArtTattooStyle>> GetArtTattooStyles()
        {
            return await _context.ArtTattooStyles.ToListAsync();
        }

        public async Task<bool> HasArtTattooStyle(int id)
        {
            return await _context.ArtTattooStyles.AnyAsync(a => a.TattooStyleId == id);
        }

        public async Task<bool> Save()
        {
            var saved = _context.SaveChangesAsync();
            return await saved > 0 ? true : false ;
        }

        public async Task<ICollection<ArtTattooStyle>> SearchArtTattooStylesByName(string name)
        {
            return await _context.ArtTattooStyles.Where(a => a.TattooStyleName.ToLower().Contains(name.ToLower())).ToListAsync();
        }
    }
}
