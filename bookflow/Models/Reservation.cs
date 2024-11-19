
namespace bookflow.Models
{
    public class Reservation :  BaseModel
    {
        public DateTime Date { get; set; }
        public ReservationStatus Status { get; set; }
        public string UserId { get; set; }
        public string BookId { get; set; }
    }

    public enum ReservationStatus
    {
        Active,
        Cancelled,
        Finished
    }

}
