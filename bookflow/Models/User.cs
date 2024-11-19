
namespace bookflow.Models
{
    public class User : BaseModel
    {
        public string Name { get; set; }
        public string CPF { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
    }

    public enum UserRole
    {
        Admin,
        Member
    }
}
