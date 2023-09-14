using Microsoft.EntityFrameworkCore;
using WebAPIDemo.IRepositories;
using WebAPIDemo.Models;

namespace WebAPIDemo.Repositories
{
    public class ArtTattooServiceRepo : IArtTattooServiceRepo
    {
        private readonly ArtTattoo2023DbContext _context;

        public ArtTattooServiceRepo(ArtTattoo2023DbContext context)
        {
            this._context = context;
        }

        public async Task<bool> CreateArtTattooService(ArtTattooService artTattooService)
        {
            _context.Add(artTattooService);
            return await Save();
        }

        public async Task<ICollection<ArtTattooService>> GetArtTattooServices()
        {
            return await _context.ArtTattooServices.OrderBy(a => a.ServiceName).ToListAsync();
        }

        public async Task<bool> Save()
        {
            var saved = _context.SaveChangesAsync();
            return await saved > 0;
        }
    }
}
