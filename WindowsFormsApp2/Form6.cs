using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2;

namespace WindowsFormsApp2
{
    public partial class Form6 : Form
    {
        List<Word> testKelimeleri = new List<Word>();
        int aktifIndex = 0;
        public int? Form10danGelenSoruSayisi { get; set; }

        public class Word
        {
            public int WordId { get; set; }
            public string EngWordName { get; set; }
            public string TurWordName { get; set; }
        }

        public Form6()
        {
            InitializeComponent();
        }

        private async void Form6_Load(object sender, EventArgs e)
        {

            lblKullaniciAd.Text = $"Merhaba, {AktifKullanici.KullaniciAdi}!";
       
            if (await BugunTestYapildiMi())
            {
                MessageBox.Show("📅 Bugünün testi zaten yapılmış. Yeni test başlatılamaz.");
           
                return;
            }

            lblKullaniciAd.Text = $"Merhaba, {AktifKullanici.KullaniciAdi}!";
            int userId = AktifKullanici.KullaniciId;
            string conStr = "Server=localhost;Database=KelimeEzberlemeKG;Trusted_Connection=True;";
            int dailyCount = Form10danGelenSoruSayisi.HasValue && Form10danGelenSoruSayisi.Value > 0
     ? Form10danGelenSoruSayisi.Value
     : AktifKullanici.DailyTestCount > 0
         ? AktifKullanici.DailyTestCount
         : 10;
            lblGunlukSayac.Text = $"📘 Günlük test:  {dailyCount} kelime";

            List<int> bugunSorulanIdler = new List<int>();
            HashSet<int> yanlisBilinenIdler = new HashSet<int>();
            HashSet<int> eklenenIdler = new HashSet<int>();

            using (SqlConnection conn = new SqlConnection(conStr))
            {
                await conn.OpenAsync();

                // Bugün sorulan kelimeleri al
                SqlCommand kontrolCmd = new SqlCommand(@"
                    SELECT WordID FROM GunlukTestTakip
                    WHERE UserId = @UserId AND TestDate = CAST(GETDATE() AS DATE)", conn);
                kontrolCmd.Parameters.AddWithValue("@UserId", userId);
                using (SqlDataReader kontrolReader = await kontrolCmd.ExecuteReaderAsync())
                    while (await kontrolReader.ReadAsync())
                        bugunSorulanIdler.Add(kontrolReader.GetInt32(0));

                // Daha önce yanlış bilinen kelimeleri al
                SqlCommand yanlisCmd = new SqlCommand(@"
                    SELECT DISTINCT WordId FROM YanlisBilinenler
                    WHERE UserId = @UserId", conn);
                yanlisCmd.Parameters.AddWithValue("@UserId", userId);
                using (SqlDataReader yanlisReader = await yanlisCmd.ExecuteReaderAsync())
                    while (await yanlisReader.ReadAsync())
                        yanlisBilinenIdler.Add(yanlisReader.GetInt32(0));

                // Tüm kelimeleri al
                SqlCommand kelimeCmd = new SqlCommand(@"
                    SELECT WordID, EngWordName, TurWordName
                    FROM Words
                    WHERE KullaniciAdi = @KullaniciAdi", conn);
                kelimeCmd.Parameters.AddWithValue("@KullaniciAdi", AktifKullanici.KullaniciAdi);

                using (SqlDataReader reader = await kelimeCmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        int id = reader.GetInt32(0);
                        if (bugunSorulanIdler.Contains(id) || eklenenIdler.Contains(id))
                            continue;

                        if (yanlisBilinenIdler.Contains(id) || !bugunSorulanIdler.Contains(id))
                        {
                            testKelimeleri.Add(new Word
                            {
                                WordId = id,
                                EngWordName = reader.GetString(1),
                                TurWordName = reader.GetString(2)
                            });
                            eklenenIdler.Add(id);
                        }
                    }
                }
            }

            // Kırpma işlemi
            if (testKelimeleri.Count > dailyCount)
                testKelimeleri = testKelimeleri.Take(dailyCount).ToList();

            lblGunlukSayac.Text += $" / Seçilen:  {testKelimeleri.Count}";

            if (testKelimeleri.Count == 0)
            {
                lblKelime.Text = "Test edilecek kelime yok.";
                btnKontrolEt.Enabled = false;
            }
            else
            {
                lblKelime.Text = testKelimeleri[0].EngWordName;
            }
        }

        private async void btnKontrolEt_Click(object sender, EventArgs e)
        {
            if (aktifIndex >= testKelimeleri.Count) return;

            var kelime = testKelimeleri[aktifIndex];
            string kullaniciCevap = txtCevap.Text.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(kullaniciCevap))
            {
                MessageBox.Show("Lütfen bir cevap girin.");
                return;
            }

            bool dogru = kullaniciCevap == kelime.TurWordName.ToLower();
            string conStr = "Server=localhost;Database=KelimeEzberlemeKG;Trusted_Connection=True;";

            using (SqlConnection conn = new SqlConnection(conStr))
            {
                await conn.OpenAsync();

                SqlCommand kontrolCmd = new SqlCommand(@"
                    IF NOT EXISTS (
                        SELECT 1 FROM WordProgresses WHERE UserId = @UserId AND WordId = @WordId
                    )
                    BEGIN
                        INSERT INTO WordProgresses (UserId, WordId, StepIndex, CorrectStreak, LastCorrectDate, IsMastered)
                        VALUES (@UserId, @WordId, 0, 0, GETDATE(), 0)
                    END", conn);
                kontrolCmd.Parameters.AddWithValue("@UserId", AktifKullanici.KullaniciId);
                kontrolCmd.Parameters.AddWithValue("@WordId", kelime.WordId);
                await kontrolCmd.ExecuteNonQueryAsync();

                string tablo = dogru ? "DogruBilinenler" : "YanlisBilinenler";
                SqlCommand cmdLog = new SqlCommand($@"
                    INSERT INTO {tablo} (UserId, WordId, TestDate)
                    VALUES (@UserId, @WordId, GETDATE())", conn);
                cmdLog.Parameters.AddWithValue("@UserId", AktifKullanici.KullaniciId);
                cmdLog.Parameters.AddWithValue("@WordId", kelime.WordId);
                await cmdLog.ExecuteNonQueryAsync();

                string updateSql = dogru
                    ? @"UPDATE WordProgresses
                        SET StepIndex = CASE WHEN StepIndex < 5 THEN StepIndex + 1 ELSE StepIndex END,
                            CorrectStreak = CorrectStreak + 1,
                            LastCorrectDate = GETDATE(),
                            IsMastered = CASE WHEN StepIndex + 1 >= 6 THEN 1 ELSE 0 END
                        WHERE UserId = @UserId AND WordId = @WordId;"
                    : @"UPDATE WordProgresses
                        SET StepIndex = 0,
                            CorrectStreak = 0,
                            LastCorrectDate = GETDATE(),
                            IsMastered = 0
                        WHERE UserId = @UserId AND WordId = @WordId;";
                SqlCommand cmdProgress = new SqlCommand(updateSql, conn);
                cmdProgress.Parameters.AddWithValue("@UserId", AktifKullanici.KullaniciId);
                cmdProgress.Parameters.AddWithValue("@WordId", kelime.WordId);
                await cmdProgress.ExecuteNonQueryAsync();
            }

            using (SqlConnection connGunluk = new SqlConnection(conStr))
            {
                await connGunluk.OpenAsync();
                SqlCommand takipCmd = new SqlCommand(@"
                    INSERT INTO GunlukTestTakip (UserId, TestDate, WordID) 
                    VALUES (@UserId, CAST(GETDATE() AS DATE), @WordID)", connGunluk);
                takipCmd.Parameters.AddWithValue("@UserId", AktifKullanici.KullaniciId);
                takipCmd.Parameters.AddWithValue("@WordID", kelime.WordId);
                await takipCmd.ExecuteNonQueryAsync();
            }

            lblDurum.Text = dogru ? "Doğru! ✅" : $"Yanlış! ❌ Doğru: {kelime.TurWordName}";
            aktifIndex++;
            txtCevap.Clear();

            if (aktifIndex < testKelimeleri.Count)
            {
                lblKelime.Text = testKelimeleri[aktifIndex].EngWordName;
            }
            else
            {
                lblKelime.Text = "Test tamamlandı 🎉";
                btnKontrolEt.Enabled = false;
                MessageBox.Show("🎯 Günün testi tamamlandı!");
            }
        }

        private void btnTestZamaniGelenler_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7();
            form7.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form8 form8 = new Form8();
            form8.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form10 form10 = new Form10();
            form10.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form9 form9 = new Form9();
            form9.Show();
            this.Hide();
        }
        private async Task<bool> BugunTestYapildiMi()
        {
            string conStr = "Server=localhost;Database=KelimeEzberlemeKG;Trusted_Connection=True;";
            int userId = AktifKullanici.KullaniciId;

            using (SqlConnection conn = new SqlConnection(conStr))
            {
                await conn.OpenAsync();
                string query = @"SELECT COUNT(*) FROM GunlukTestTakip 
                         WHERE UserId = @UserId AND TestDate = CAST(GETDATE() AS DATE)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);
                int count = (int)await cmd.ExecuteScalarAsync();
                return count > 0;
            }
        }

    }
}














