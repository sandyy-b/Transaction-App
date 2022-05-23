using System.ComponentModel.DataAnnotations;

namespace $safeprojectname$.Models
{
    public class AuthentiateModel
    {
        [Required] // Account Number validation to 10-digit Account Number using Regex
        [RegularExpression(@"^[0][1-9]\d{9}$|^[1-9]\d{9}$")]
        public string AccountNumber { get; set; }
        
        [Required]

        public string Pin { get; set; }
    }
}
