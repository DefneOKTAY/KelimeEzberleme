using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KullaniciWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KullaniciWebApi.Services
{
    public class LearningService
    {
        private readonly AppDbContext _context;

        public LearningService(AppDbContext context)
        {
            _context = context;
        }

        // 📌 Kullanıcının test zamanı gelen kelimelerini getir
        public async Task<List<Word>> GetTestableWords(int userId)
        {
            var now = DateTime.Now;

            var testable = await _context.WordProgresses
                .Include(wp => wp.Word)
                .Where(wp =>
                    wp.UserId == userId &&
                    !wp.IsMastered &&
                    (
                        (wp.StepIndex == 0 && wp.LastCorrectDate.AddDays(1) <= now) ||
                        (wp.StepIndex == 1 && wp.LastCorrectDate.AddDays(7) <= now) ||
                        (wp.StepIndex == 2 && wp.LastCorrectDate.AddMonths(1) <= now) ||
                        (wp.StepIndex == 3 && wp.LastCorrectDate.AddMonths(3) <= now) ||
                        (wp.StepIndex == 4 && wp.LastCorrectDate.AddMonths(6) <= now) ||
                        (wp.StepIndex == 5 && wp.LastCorrectDate.AddYears(1) <= now)
                    )
                )
                .Select(wp => wp.Word!)
                .ToListAsync();

            return testable;
        }

        // 📌 Kullanıcının bugün öğrenmesi gereken max 2 kelimeyi getir
        public async Task<List<Word>> GetLearnableWords(int userId)
        {
            var candidates = await _context.Words
                .Where(w =>
                    _context.WordProgresses.Any(wp =>
                        wp.WordId == w.WordID &&
                        wp.UserId == userId &&
                        wp.IsMastered == false &&
                        wp.CorrectStreak == 0
                    )
                    ||
                    !_context.WordProgresses.Any(wp =>
                        wp.WordId == w.WordID &&
                        wp.UserId == userId
                    )
                )
                .OrderBy(r => Guid.NewGuid())
                .Take(10)
                .ToListAsync();

            return candidates;
        }
    }
}

