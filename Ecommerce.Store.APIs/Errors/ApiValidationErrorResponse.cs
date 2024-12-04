namespace Ecommerce.Store.APIs.Errors
{
    public class ApiValidationErrorResponse : ApiErrorResponse
    {
        public IEnumerable<string> Errors { get; set; } = new List<string>();

        public ApiValidationErrorResponse() : base(400)
        {
            
        }
    }
}
