using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using WebAPIDemo.IRepositories;
using WebAPIDemo.Models;

namespace WebAPIDemo.Repositories
{
    public class AccountMemberRepo : IAccountMemberRepo
    {
        private readonly ArtTattoo2023DbContext _context;

        public AccountMemberRepo(ArtTattoo2023DbContext context)
        {
            this._context = context;
        }

        public bool CreateAccountMember(AccountMember accountMember)
        {
            _context.Add(accountMember);
            return Save();
        }

        public async Task<AccountMember> GetAccountMember(int id)
        {
            return await _context.AccountMembers.FirstOrDefaultAsync(acc => acc.AccountId == id);
        }

        public async Task<ICollection<AccountMember>> GetAccountMembers()
        {
            return await _context.AccountMembers.OrderBy(acc => acc.FullName).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >= 0 ? true : false;
        }
    }
}
