namespace ExpensesCounter.Common.Models
{
    public class ErrorResponseModel
    {
        public string Message { get; set; }

        public ErrorResponseModel(string message)
        {
            this.Message = message;
        }
    }
}