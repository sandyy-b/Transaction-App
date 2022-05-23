﻿using System.ComponentModel.DataAnnotations;

namespace $safeprojectname$.Models
{
    public class GetAccountModel
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
        public DateTime DateCreated { get; set; }
        public DateTime DateLastUpdated { get; set; }
    }
}
