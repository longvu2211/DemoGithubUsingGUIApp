using WebAPIDemo.Models;

namespace WebAPIDemo.IRepositories
{
    public interface IAccountMemberRepo
    {
        Task<ICollection<AccountMember>> GetAccountMembers();
        Task<AccountMember> GetAccountMember(int id);
    }
}
