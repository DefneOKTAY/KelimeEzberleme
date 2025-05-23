using System;

namespace KullaniciWebApi.Models
{
    public class WordProgress
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public int WordId { get; set; }
        public Word? Word { get; set; }

        public int StepIndex { get; set; } = 0;

        public int CorrectStreak { get; set; } = 0;

        public DateTime LastCorrectDate { get; set; } = DateTime.Now;

        public bool IsMastered { get; set; } = false;
    }
}
