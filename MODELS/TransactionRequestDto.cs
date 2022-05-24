namespace TransactionApp.Models
{
    public class TransactionRequestDto
    {
        public decimal TransactionAmount { get; set; }
        public string TransactionSourceAccount { get; set; }
        public string TransactionDestinationAccount { get; set; }
        public TranType TransactionType { get; set; } // This is another Enum to show the type of the transaction (Deposit/ Withdrawl)
        public DateTime TransactionDate { get; set; }
    }
}
