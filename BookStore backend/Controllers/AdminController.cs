using BookStore_backend.models;
using BookStore_backend.packages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IPKG_ADMIN _package;
        public AdminController(IPKG_ADMIN package)
        {
            _package = package;

        }
        [HttpPost("/add_book")]
        public IActionResult Add_Book([FromBody] Book book)
        {
            try
            {
                _package.Add_book(book);
                return Ok("book added successfully.");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, ex.Message);

            }
        }
        [HttpGet("/get_orders")]
        public List<Order> Get_Orders()
        {
            List<Order> list = new List<Order>();
            try
            {
                list = _package.Get_Orders();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return list;
        }
    }
}
