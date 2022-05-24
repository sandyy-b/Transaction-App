using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TransactionApp.DAL;
using TransactionApp.Models;
using TransactionApp.Services.Interfaces;
using TransactionApp.Utils;

namespace TransactionApp.Services.Implementations
{
    public class TransactionService : ITransactionService
    {

        private BankingDbContext _dbContext;
        ILogger<TransactionService> _logger;
        private AppSettings _settings;
        private static string _ourBankSettlementAccount;
        private readonly IAccountService _accountService;

        public TransactionService(BankingDbContext dbContext, ILogger<TransactionService> logger, IOptions<AppSettings> settings,
            IAccountService accountService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _settings = settings.Value;
            _ourBankSettlementAccount = _settings.OurBankSettlementAccount;
            _accountService = accountService;
        }
        public Response CreateNewTransaction(Transaction transaction)
        {
            // Creating a new Transaction
            Response response = new Response();
            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();
            response.ResponseCode = "00";
            response.ResponseMessage = "Transaction created Successfully!";
            //response.Data = null;

            return response;
        }

        public Response FindTransactionByDate(DateTime date)
        {
            Response response = new Response();
            var transaction = _dbContext.Transactions.Where(x => x.TransactionDate == date).ToList(); // Coz there are many transactions in a day
            response.ResponseCode = "00";
            response.ResponseMessage = "Transaction created Successfully!";
            //response.Data = transaction;

            return response;
        }

        public Response MakeDeposit(string AccountNumber, decimal Amount, string TransactionPin)
        {
            // Deposit Functionality in a transactionn is being performed here!
            Response response = new Response();
            Account sourceAccount;
            Account destinationAccount;
            Transaction transaction = new Transaction();

            // Firstly we'll check whether the account owner is valid or not 
            // We will inject authenticate to check it

            var authUser = _accountService.Authenticate(AccountNumber, TransactionPin);
            if (authUser == null)
            {
                throw new ApplicationException("Invalid Credentials!");
            }

            try
            {
                sourceAccount = _accountService.GetbyAccountNumber(_ourBankSettlementAccount);
                destinationAccount = _accountService.GetbyAccountNumber(AccountNumber);
                sourceAccount.CurrentAccountBalance -= Amount;
                destinationAccount.CurrentAccountBalance += Amount;

                // Checking for updates
                if ((_dbContext.Entry(sourceAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified) &&
                    (_dbContext.Entry(destinationAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified))
                {
                    // Successfull Transaction
                    transaction.TransactionStatus = TranStatus.Success;
                    response.ResponseCode = "00";
                    response.ResponseMessage = "Transaction Successfull!";
                    //response.Data = null;
                }
                else
                {

                    // Unsuccessfull Transaction
                    transaction.TransactionStatus = TranStatus.Failed;
                    response.ResponseCode = "02";
                    response.ResponseMessage = "Transaction Failed!";
                    //response.Data = null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"AN ERROR OCCURED... => {ex.Message}");
            }

            // Set other Properties of Transactions here
            transaction.TransactionType = TranType.Deposit;
            transaction.TransactionSourceAccount = _ourBankSettlementAccount;
            transaction.TransactionDestinationAccount = AccountNumber;
            transaction.TransactionAmount = Amount;
            transaction.TransactionDate = DateTime.Now;
            transaction.TransactionParticulars = $"NEW TRANSACTION FROM SOURCE => {JsonConvert.SerializeObject
            (transaction.TransactionSourceAccount)} TO DESTINATION ACCOUNT => {JsonConvert.SerializeObject
            (transaction.TransactionDestinationAccount)} ON DATE => {transaction.TransactionDate} FOR AMOUNT => {JsonConvert.SerializeObject
            (transaction.TransactionAmount)} TRANSACTION TYPE => {JsonConvert.SerializeObject
            (transaction.TransactionType)} TRANSACTION STATUS => {JsonConvert.SerializeObject
            (transaction.TransactionStatus)}";

            // Commiting to Database
            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();

            return response;
        }

        public Response MakeFundsTransfer(string FromAccount, string ToAccount, decimal Amount, string TransactionPin)
        {
            // Transfer Functionality in a transaction is being performed here!
            Response response = new Response();
            Account sourceAccount;
            Account destinationAccount;
            Transaction transaction = new Transaction();

            // Firstly we'll check whether the account owner is valid or not 
            // We will inject authenticate to check it

            var authUser = _accountService.Authenticate(FromAccount, TransactionPin);
            if (authUser == null)
            {
                throw new ApplicationException("Invalid Credentials!");
            }

            try
            {
                sourceAccount = _accountService.GetbyAccountNumber(FromAccount);
                destinationAccount = _accountService.GetbyAccountNumber(ToAccount);
                sourceAccount.CurrentAccountBalance -= Amount;
                destinationAccount.CurrentAccountBalance += Amount;

                // Checking for updates
                if ((_dbContext.Entry(sourceAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified) &&
                    (_dbContext.Entry(destinationAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified))
                {
                    // Successfull Transaction
                    transaction.TransactionStatus = TranStatus.Success;
                    response.ResponseCode = "00";
                    response.ResponseMessage = "Transaction Successfull!";
                    //response.Data = null;
                }
                else
                {

                    // Unsuccessfull Transaction
                    transaction.TransactionStatus = TranStatus.Failed;
                    response.ResponseCode = "02";
                    response.ResponseMessage = "Transaction Failed!";
                    //response.Data = null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"AN ERROR OCCURED... => {ex.Message}");
            }

            // Set other Properties of Transactions here
            transaction.TransactionType = TranType.Transfer;
            transaction.TransactionSourceAccount = FromAccount;
            transaction.TransactionDestinationAccount = ToAccount;
            transaction.TransactionAmount = Amount;
            transaction.TransactionDate = DateTime.Now;
            transaction.TransactionParticulars = $"NEW TRANSACTION FROM SOURCE => {JsonConvert.SerializeObject
            (transaction.TransactionSourceAccount)} TO DESTINATION ACCOUNT => {JsonConvert.SerializeObject
            (transaction.TransactionDestinationAccount)} ON DATE => {transaction.TransactionDate} FOR AMOUNT => {JsonConvert.SerializeObject
            (transaction.TransactionAmount)} TRANSACTION TYPE => {JsonConvert.SerializeObject
            (transaction.TransactionType)} TRANSACTION STATUS => {JsonConvert.SerializeObject
            (transaction.TransactionStatus)}";

            // Commiting to Database
            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();

            return response;

        }

        public Response MakeWithdrawl(string AccountNumber, decimal Amount, string TransactionPin)
        {
            // Withdrawl Functionality in a transaction is being performed here!
            Response response = new Response();
            Account sourceAccount;
            Account destinationAccount;
            Transaction transaction = new Transaction();

            // Firstly we'll check whether the account owner is valid or not 
            // We will inject authenticate to check it

            var authUser = _accountService.Authenticate(AccountNumber, TransactionPin);
            if (authUser == null)
            {
                throw new ApplicationException("Invalid Credentials!");
            }

            try
            {
                sourceAccount = _accountService.GetbyAccountNumber(AccountNumber);
                destinationAccount = _accountService.GetbyAccountNumber(_ourBankSettlementAccount);
                sourceAccount.CurrentAccountBalance -= Amount;
                destinationAccount.CurrentAccountBalance += Amount;

                // Checking for updates
                if ((_dbContext.Entry(sourceAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified) &&
                    (_dbContext.Entry(destinationAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified))
                {
                    // Successfull Transaction
                    transaction.TransactionStatus = TranStatus.Success;
                    response.ResponseCode = "00";
                    response.ResponseMessage = "Transaction Successfull!";
                    //response.Data = null;
                }
                else
                {

                    // Unsuccessfull Transaction
                    transaction.TransactionStatus = TranStatus.Failed;
                    response.ResponseCode = "02";
                    response.ResponseMessage = "Transaction Failed!";
                    //response.Data = null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"AN ERROR OCCURED... => {ex.Message}");
            }

            // Set other Properties of Transactions here
            transaction.TransactionType = TranType.Withdrawl;
            transaction.TransactionSourceAccount = AccountNumber;
            transaction.TransactionDestinationAccount = _ourBankSettlementAccount;
            transaction.TransactionAmount = Amount;
            transaction.TransactionDate = DateTime.Now;
            transaction.TransactionParticulars = $"NEW TRANSACTION FROM SOURCE => {JsonConvert.SerializeObject
            (transaction.TransactionSourceAccount)} TO DESTINATION ACCOUNT => {JsonConvert.SerializeObject
            (transaction.TransactionDestinationAccount)} ON DATE => {transaction.TransactionDate} FOR AMOUNT => {JsonConvert.SerializeObject
            (transaction.TransactionAmount)} TRANSACTION TYPE => {JsonConvert.SerializeObject
            (transaction.TransactionType)} TRANSACTION STATUS => {JsonConvert.SerializeObject
            (transaction.TransactionStatus)}";

            // Commiting to Database
            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();

            return response;
        }
    }
}
