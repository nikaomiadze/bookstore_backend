using BookStore_backend.models;
using BookStore_backend.packages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IPKG_USER pkg;
        public UserController(IPKG_USER package)
        {
            pkg = package;
         
        }

        [HttpPost("/add_user")]
        public IActionResult Add_User([FromBody] User user)
        {
            try { 
                    pkg.Add_user(user);
                    return Ok("User added successfully.");
               
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, ex.Message);

            }
        }
        [HttpPost("/add_order")]
        public IActionResult Add_Order([FromBody] Order order)
        {
            try
            {
                pkg.Add_order(order);
                return Ok("order added successfully.");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, ex.Message);

            }
        }
        [HttpPut("/complete_order")]
        public IActionResult Complete_Order([FromBody] Complete_order order)
        {
            try
            {
                pkg.Complete_order(order);
                return Ok("order complete successfully.");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, ex.Message);

            }
        }
        [HttpGet("/get_books")]
        public List<Book> Get_Books()
        {
            List<Book> list = new List<Book>();
            try
            {
                list = pkg.Get_Books();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return list;
        }


    }
}
