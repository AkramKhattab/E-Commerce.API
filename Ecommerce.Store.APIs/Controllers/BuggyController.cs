using Ecommerce.Store.APIs.Errors;
using Ecommerce.Store.Repository.Data.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Store.APIs.Controllers
{
    // {ERRORS REFERNCE for FrontEnd}

    public class BuggyController : BaseApiController
    {
        private readonly EcommerceDbContext _context;

        public BuggyController(EcommerceDbContext context) 
        { 
            _context = context;
        }

        //Request
        [HttpGet("notfound")]  //GET: /api/Buggy/notfound
        public async Task<IActionResult> GetNotFoundRequestError()
        {
            var brand = await  _context.Brands.FindAsync(100);
            if(brand is null) return NotFound(new ApiErrorResponse(404));
            return Ok(brand);
        } 
        //

        //Server
        [HttpGet("servererror")]  //GET: /api/Buggy/servererror
        public async Task<IActionResult> GetServerRequestError()
        {
            var brand = await _context.Brands.FindAsync(100);
            var brandToString = brand.ToString(); // Will Throw Exception (Null-Reference-Exception)
            return Ok(brand);
        }
        //

        //BadRequest
        [HttpGet("badrequest")]  //GET: /api/Buggy/badrequest
        public async Task<IActionResult> GetBadRequestError()
        {
            return BadRequest(new ApiErrorResponse(400));
        }
        //

        // Validation Error
        [HttpGet("badrequest/{id}")]  //GET: /api/Buggy/badrequest/ahmed
        public async Task<IActionResult> GetBadRequestError(int id) // Validation Error
        {

            return Ok();
        }
        //

        // Authorization Error
        [HttpGet("unaithorized")]  //GET: /api/Buggy/unaithorized
        public async Task<IActionResult> GetUnauthorizedError(int id) // Validation Error
        {
            return Unauthorized(new ApiErrorResponse(401));
        }
        //



    }
}
