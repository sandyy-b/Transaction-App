using $safeprojectname$.Models;

namespace $safeprojectname$.Services
{
    public interface IAccountService
    {
        Account Authenticate(string AccountNumber, string Pin);
        IEnumerable<Account> GetAllAccounts();
        Account Create(Account account, string Pin, string ConfirmPin);
        void Update(Account account, string Pin = null);
        void Delete(int Id);
        Account GetbyId(int Id);
        Account GetbyAccountNumber(string AccountNumber);
    }
}
