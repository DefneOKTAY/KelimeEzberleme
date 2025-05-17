using Microsoft.AspNetCore.Mvc;
using KullaniciWebApi.Models;

namespace KullaniciWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WordController(AppDbContext context)
        {
            _context = context;
        }

        // 1️⃣ Yeni kelime ekleme
        [HttpPost("add")]
        public IActionResult AddWord([FromBody] Word word)
        {
            _context.Words.Add(word);
            _context.SaveChanges();
            return Ok("Kelime eklendi ✅");
        }

        // 2️⃣ Tüm kelimeleri listeleme
        [HttpGet("all")]
        public IActionResult GetAllWords()
        {
            var words = _context.Words.ToList();
            return Ok(words);
        }
    }
}
