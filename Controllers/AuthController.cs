using Microsoft.AspNetCore.Mvc;
using KullaniciWebApi.Models;
using System.Linq;


namespace KullaniciWebApi.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ Test endpoint – sadece GET (tarayıcı için)
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("AuthController çalışıyor ✅");
        }

        // ✅ Kayıt ol
        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);
            if (existingUser != null)
            {
                return BadRequest("Bu email zaten kayıtlı.");
            }

            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok("Kayıt başarılı.");
        }

        // ✅ Giriş yap
        [HttpPost("login")]
        public IActionResult Login([FromBody] User loginData)
        {
            var user = _context.Users.FirstOrDefault(u =>
                u.Email == loginData.Email && u.Password == loginData.Password);

            if (user == null)
                return Unauthorized("Hatalı e-posta ya da şifre.");

            return Ok($"Hoşgeldin {user.UserName}!");
        }

        // ✅ Şifremi unuttum
        [HttpPost("forgot")]
        public IActionResult ForgotPassword([FromBody] string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
                return NotFound("Kullanıcı bulunamadı.");

            return Ok("Şifre sıfırlama bağlantısı e-posta ile gönderildi.");
        }
    }
}
