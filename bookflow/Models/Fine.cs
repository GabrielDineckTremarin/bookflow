
namespace bookflow.Models
{
    public class Fine : BaseModel
    {
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
        public DateTime DateIssued { get; set; }
        public string LoanId { get; set; }
    }

}
