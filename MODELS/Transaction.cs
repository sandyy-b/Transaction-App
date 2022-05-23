using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace $safeprojectname$.Models
{
    [Table("Transactions")]
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public string TransactionUniqueReference { get; set; } // This will generate in every instance of this class 
        public decimal TransactionAmount { get; set; }
        public TranStatus TransactionStatus { get; set; } // This is the Enum to keep the record of the Transaction Status
        public bool IsSuccessful => TransactionStatus.Equals(TranStatus.Success); // It depends on the value of Transaction Status
        public string TransactionSourceAccount { get; set; }
        public string TransactionDestinationAccount { get; set; }
        public string TransactionParticulars { get; set; }
        public TranType TransactionType { get; set; } // This is another Enum to show the type of the transaction (Deposit/ Withdrawl)
        public DateTime TransactionDate { get; set; }
        public Transaction()
        {
            TransactionUniqueReference = $"{Guid.NewGuid().ToString().Replace("-","").Substring(1,27)}"; // We will use GUID to generate it.
        }
    }
    public enum TranStatus
    {
        Failed,
        Success,
        Error
    }

    public enum TranType
    {
        Deposit,
        Withdrawl,
        Transfer
    }
}
