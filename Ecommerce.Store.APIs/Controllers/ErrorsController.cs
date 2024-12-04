using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Store.APIs.Errors;

namespace Ecommerce.Store.APIs.Controllers
{
    [Route("error/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        public IActionResult Error(int code)
        {
            return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Not Fount End Point !"));
        }
    }
}
