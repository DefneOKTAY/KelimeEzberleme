using Microsoft.AspNetCore.Mvc;

namespace KullaniciWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Test başarılı 🚀");
        }
    }
}
