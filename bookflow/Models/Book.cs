

namespace bookflow.Models
{
    public class Book : BaseModel
    {

        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime PublicationDate { get; set; }
        public string ISBN { get; set; }
    }

}
