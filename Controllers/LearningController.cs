using KullaniciWebApi.DTOs;
using KullaniciWebApi.Models;
using KullaniciWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KullaniciWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LearningController : ControllerBase
    {
        private readonly LearningService _learningService;
        private readonly AppDbContext _context;

        public LearningController(LearningService learningService, AppDbContext context)
        {
            _learningService = learningService;
            _context = context;
        }

        // 1️⃣ Bugün öğreneceği en fazla 2 kelimeyi getir
        [HttpGet("today")]
        public async Task<IActionResult> GetTodayLearnables([FromQuery] int userId)
        {
            var words = await _learningService.GetLearnableWords(userId);
            if (words == null || words.Count == 0)
                return Ok(new { message = "Öğrenecek kelimeniz kalmadı. Yeni kelimeler ekleyin." });

            return Ok(words);
        }

        // 2️⃣ Test zamanı gelen kelimeleri getir
        [HttpGet("testable")]
        public async Task<IActionResult> GetTestableWords([FromQuery] int userId)
        {
            var testableWords = await _learningService.GetTestableWords(userId);
            if (testableWords == null || testableWords.Count == 0)
                return Ok(new { message = "Test zamanı gelen kelimeniz yok." });

            return Ok(testableWords);
        }

        // 3️⃣ Test sonucu gönder
        [HttpPost("test-result")]
        public async Task<IActionResult> SubmitTestResult([FromBody] TestResultDto result)
        {
            var progress = await _context.WordProgresses
                .FirstOrDefaultAsync(wp => wp.UserId == result.UserId && wp.WordId == result.WordId);

            if (progress == null)
            {
                progress = new WordProgress
                {
                    UserId = result.UserId,
                    WordId = result.WordId,
                    StepIndex = 0,
                    CorrectStreak = 0,
                    LastCorrectDate = DateTime.Now,
                    IsMastered = false
                };
                _context.WordProgresses.Add(progress);
            }

            if (result.IsCorrect)
            {
                progress.CorrectStreak++;
                if (progress.CorrectStreak >= 6)
                {
                    progress.IsMastered = true;
                }
                else
                {
                    progress.StepIndex++;
                    progress.LastCorrectDate = DateTime.Now;
                }
            }
            else
            {
                progress.CorrectStreak = 0;
                progress.StepIndex = 0;
                progress.LastCorrectDate = DateTime.Now;
                progress.IsMastered = false;
            }

            await _context.SaveChangesAsync();
            return Ok(new { message = "Cevap işlendi." });
        }
    }
}
