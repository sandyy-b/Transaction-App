using System.ComponentModel.DataAnnotations;

namespace $safeprojectname$.Models
{
    public class UpdateAccountModel
    {
        [Key]
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]/d{4}$", ErrorMessage = "Pin should not be more than 4-digits")] // It should be a 4-digit string
        public string Pin { get; set; }

        [Required]
        [Compare("Pin", ErrorMessage = "Pins do not Match")]
        public string ConfirmPin { get; set; } // We want to compare itt with Pin
        public DateTime DateLastUpdated { get; set; }
    }
}
