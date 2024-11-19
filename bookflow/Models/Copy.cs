

namespace bookflow.Models
{
    public class Copy : BaseModel
    {
        public bool Available { get; set; }
        public string BookId { get; set; }
        public CopyCondition Condition { get; set; }
    }

    public enum CopyCondition
    {
        New,
        Used,
        Damaged,
        Lost
    }
}
