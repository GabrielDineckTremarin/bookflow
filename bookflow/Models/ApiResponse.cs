namespace bookflow.Models
{
    public class ApiResponse
    {
        public string message { get; set; }
        public bool success { get; set; }
        public object data { get; set; }

        public ApiResponse(object data, string message = "", bool success = true)
        {
            this.message = message;
            this.success = success;
            this.data = data;
        }

        public ApiResponse(Exception ex, string message = "Ocorreu um erro ao realizar a ação!")
        {

            this.data = null;
            this.message = ex.GetType().Name == typeof(ValidationException).Name ? ex.Message : message;
            this.success = false;
        }
    }
}
