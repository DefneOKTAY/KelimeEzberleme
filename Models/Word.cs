namespace KullaniciWebApi.Models
{
    public class Word
    {
        public int WordID { get; set; }
        public string EngWordName { get; set; } = null!;
        public string TurWordName { get; set; } = null!;
        public string Picture { get; set; } = null!;  // C://words/image.jpg gibi

        // Sesli okunuş opsiyonel olduğu için null olabilir
        public string? PronunciationPath { get; set; }

        // Navigation property
        public List<WordSample>? Samples { get; set; }
    }
}
