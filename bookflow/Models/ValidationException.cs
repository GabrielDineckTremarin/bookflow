namespace bookflow.Models
{
    public class ValidationException : Exception
    {
        public ValidationException(Exception ex) : base(ex.Message, ex) { }
        public ValidationException(string message) : base(message) { }
    }
}
