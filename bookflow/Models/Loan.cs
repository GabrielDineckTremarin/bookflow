
namespace bookflow.Models
{

    public class Loan : BaseModel
    {
        public DateTime Date { get; set; }
        public DateTime ReturnDate { get; set; }
        public LoanStatus Status { get; set; }
        public string UserId { get; set; }
        public string CopyId { get; set; }
    }
    public enum LoanStatus
    {
        Active,
        Returned,
        Overdue
    }
}
