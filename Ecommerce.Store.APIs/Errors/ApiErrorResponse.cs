namespace Ecommerce.Store.APIs.Errors
{
    public class ApiErrorResponse
    {
        public int StatusCode{ get; set; }
        public string? Message{ get; set; }

        public ApiErrorResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message?? GetDefaultMessageForStatusCode(statusCode);
        }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            var message = statusCode switch
            {
                400 => "A Bad Request, You Have Made",
                401 => "Authorized, You Are Not",
                404 => "Resource was not found",
                500 => "Server Error",
                _ => null
            };


            return message;
        }


    }
}
