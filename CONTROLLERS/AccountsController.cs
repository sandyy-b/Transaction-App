using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using $safeprojectname$.Models;
using $safeprojectname$.Services;

namespace $safeprojectname$.Controllers
{
    [ApiController]
    [Route("api/v3/controller")]
    public class AccountsController : ControllerBase
    {
        // Injection of Account Service
        private IAccountService _accountService;
        IMapper _mapper;
        public AccountsController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        // Register New Account
        [HttpPost]
        [Route("register_new_account")]

        public IActionResult RegisterNewAccount([FromBody] RegisterNewAccountModel newAccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(newAccount);
            }

            // Map to Account
            var account = _mapper.Map<Account>(newAccount);
            return Ok(_accountService.Create(account, newAccount.Pin, newAccount.ConfirmPin));
        }

        [HttpGet]
        [Route("get_all_accounts")]

        public IActionResult GetAllAccounts()
        {
            // We want to map this to Get Account Model
            var accounts = _accountService.GetAllAccounts();
            var cleanedAccounts = _mapper.Map<IList<GetAccountModel>>(accounts);
            return Ok(cleanedAccounts);
        }

        [HttpPost]
        [Route("authenticate")]

        public IActionResult Authenticate([FromBody] AuthentiateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }
            return Ok(_accountService.Authenticate(model.AccountNumber, model.Pin));
        }

        [HttpGet]
        [Route("get_by_account_number")]

        public IActionResult GetByAccountNumber(string AccountNumber)
        {
            if (!Regex.IsMatch(AccountNumber, @"^[0][1-9]\d{9}$|^[1-9]\d{9}$"))
            {
                return BadRequest("Account Number must be of 10-digits");
            }
            var account = _accountService.GetbyAccountNumber(AccountNumber);
            var cleanedAccount = _mapper.Map<GetAccountModel>(account);
            return Ok(cleanedAccount);

        }

        [HttpGet]
        [Route("get_account_by_id")]

        public IActionResult GetAccountById(int Id)
        {
            var account = _accountService.GetbyId(Id);
            var cleanedAccount = _mapper.Map<GetAccountModel>(account);
            return Ok(cleanedAccount);
        }

        [HttpPost]
        [Route("update_account")]
        public IActionResult UpdateAccount([FromBody] UpdateAccountModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }
            var account = _mapper.Map<Account>(model);
            _accountService.Update(account, model.Pin);
            return Ok();
        }
    }
}
