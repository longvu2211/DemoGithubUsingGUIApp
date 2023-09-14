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
        private static readonly int CUSTOMER_ROLE = 4;

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

        [HttpGet("accounts/{id}")]
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

        // modify it in the future, using async 
        [HttpPost("account")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateAccountMember([FromBody] AccountMemberDto accountCreate)
        {
            if (accountCreate == null) return BadRequest(ModelState);
            var accountDuplicate = _accountMemberRepo.GetAccountMembers()
                .Result.FirstOrDefault(acc => acc.AccountId == accountCreate.AccountId);
            if (accountDuplicate != null)
            {
                ModelState.AddModelError("", "Account already exists!");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var accountMap = _mapper.Map<AccountMember>(accountCreate);
            accountMap.Role = CUSTOMER_ROLE;
            if (!_accountMemberRepo.CreateAccountMember(accountMap))
            {
                ModelState.AddModelError("", "Something went wrong saving the account!");
                return StatusCode(500, ModelState);
            }
            return Ok("Create successfully!");
        }
    }
}
