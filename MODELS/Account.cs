using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace $safeprojectname$.Models
{
    [Table("Accounts")]
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccountName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public decimal CurrentAccountBalance { get; set; }
        public AccountType AccountType { get; set; } // This is the Enum to show the type of the Account Created (Savings or Current) etc  
        public string AccountNumberGenerated { get; set; } // Account Number is generated here 

        // Hash and Salt of the Account Transaction pin is stored here
        public byte[] PinHash { get; set; }
        public byte[] pinSalt { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastUpdated { get; set; }

        // Random object Creation
        Random rand = new Random();

        // Account Number generation (in a constructor)
        public Account()
        {
            AccountNumberGenerated = Convert.ToString((long)rand.NextDouble() * 9_000_000_000L + 1_000_000_000L); // This is done
            // to create a random 10 digit Account Number.

            AccountName = $"{FirstName} {LastName}"; // Done to generate the complete Account Holder Name.
        }
    }

    public enum AccountType
    {
        Savings,
        Current,
        Corporate,
        Government
    }
}
