namespace KullaniciWebApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;  // ← BU SATIR EKSİKTİ
        public string Password { get; set; } = string.Empty;
    }
}
