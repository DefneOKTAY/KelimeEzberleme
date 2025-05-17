using System.ComponentModel.DataAnnotations;

namespace KullaniciWebApi.Models
{
    public class WordSample
    {
        [Key]
        public int Id { get; set; }

        public int WordID { get; set; }
        public string Samples { get; set; } = null!;

        // Navigation property
        public Word? Word { get; set; }
    }
}
