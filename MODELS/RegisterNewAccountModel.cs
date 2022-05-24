using System.ComponentModel.DataAnnotations;

namespace TransactionApp.Models
{
    public class RegisterNewAccountModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        //public decimal CurrentAccountBalance { get; set; }
        public AccountType AccountType { get; set; } // This is the Enum to show the type of the Account Created (Savings or Current) etc  
        //public string AccountNumberGenerated { get; set; } // Account Number is generated here 

        // Hash and Salt of the Account Transaction pin is stored here
        //public byte[] PinHash { get; set; }
        //public byte[] pinSalt { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastUpdated { get; set; }
        // Regular Expressions
        [Required]
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Pin should not be more than 4-digits")] // It should be a 4-digit string
        public string Pin { get; set; }

        [Required]
        [Compare("Pin", ErrorMessage = "Pins do not Match")]
        public string ConfirmPin { get; set; } // We want to compare itt with Pin
    }
}
