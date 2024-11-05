namespace Store4.PIs.Errors
{
    public class ApiErrorResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ApiErrorResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message?? GetDefaultMessageForStatusCode(statusCode);

        }
        private string? GetDefaultMessageForStatusCode(int statusCode)
        {
            var message = statusCode switch
            {
                400 => " a bad request , u have made",
                401 => " authorized , u are not",
                404 => " Resource ,was not found",
                500 => " server error",
                _ => null


            };
            return message;
        }
    }
}
