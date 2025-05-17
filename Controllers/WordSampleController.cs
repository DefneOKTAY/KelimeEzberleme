using Microsoft.AspNetCore.Mvc;
using KullaniciWebApi.Models;

namespace KullaniciWebApi.Controllers
{
    [ApiController]
    [Route("wordsample")]
    public class WordSampleController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WordSampleController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ 1. WordSample Ekle
        [HttpPost("add")]
        public IActionResult AddSample([FromBody] WordSample sample)
        {
            _context.WordSamples.Add(sample);
            _context.SaveChanges();
            return Ok("Cümle eklendi.");
        }

        // ✅ 2. Belirli kelimeye ait örnek cümleleri getir
        [HttpGet("getbyword/{wordId}")]
        public IActionResult GetSamplesByWord(int wordId)
        {
            var samples = _context.WordSamples
                .Where(s => s.WordID == wordId)
                .ToList();

            return Ok(samples);
        }

        // ✅ 3. Tüm örnek cümleleri getir (isteğe bağlı)
        [HttpGet("all")]
        public IActionResult GetAllSamples()
        {
            return Ok(_context.WordSamples.ToList());
        }
    }
}
