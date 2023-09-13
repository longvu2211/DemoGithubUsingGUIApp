using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPIDemo.Dto;
using WebAPIDemo.IRepositories;
using WebAPIDemo.Models;

namespace WebAPIDemo.Controllers
{
    [ApiController]
    [Route("api")]
    public class AccountMemberController : Controller
    {
        private readonly IAccountMemberRepo _accountMemberRepo;
        private readonly IMapper _mapper;

        public AccountMemberController(IAccountMemberRepo accountMemberRepo, IMapper mapper)
        {
            this._accountMemberRepo = accountMemberRepo;
            this._mapper = mapper;
        }

        [HttpGet("accounts")]
        [ProducesResponseType(200, Type = typeof(ICollection<AccountMember>))]
        public async Task<ActionResult<ICollection<AccountMember>>> GetAccountMembers()
        {
            var accounts = await _accountMemberRepo.GetAccountMembers();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var mappedAccounts = _mapper.Map<ICollection<AccountMemberDto>>(accounts);
            return Ok(mappedAccounts);
        }

        [HttpGet("account/{id}")]
        [ProducesResponseType(200, Type = typeof(AccountMember))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<AccountMember>> GetAccountMember(int id)
        {
            var account = await _accountMemberRepo.GetAccountMember(id);
            if (account == null) return NotFound();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var mappedAccount = _mapper.Map<AccountMemberDto>(account);
            return Ok(mappedAccount);
        }
    }
}
