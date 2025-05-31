using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form7 : Form
    {
        private List<Word> quizKelimeleri = new List<Word>();
        private int aktifIndex = 0;
        private Word aktifKelime;

        public Form7()
        {
            InitializeComponent();
        }

        private async void Form7_Load(object sender, EventArgs e)
        {
            await KelimeleriYukleVeGoster();
        }

        private async Task KelimeleriYukleVeGoster()
        {
            quizKelimeleri.Clear();
            aktifIndex = 0;
            aktifKelime = null;

            int userId = AktifKullanici.KullaniciId;
            string connStr = "Server=localhost;Database=KelimeEzberlemeKG;Trusted_Connection=True;";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                await conn.OpenAsync();

                string query = @"
                    SELECT wp.WordId, wp.StepIndex, wp.LastCorrectDate, wp.CorrectStreak, w.EngWordName, w.TurWordName
                    FROM WordProgresses wp
                    JOIN Words w ON wp.WordId = w.WordId
                    WHERE wp.UserId = @userId AND wp.IsMastered = 0";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            int wordId = reader.GetInt32(0);
                            int step = reader.GetInt32(1);
                            DateTime lastCorrect = reader.GetDateTime(2);
                            int correctStreak = reader.GetInt32(3);
                            string eng = reader.GetString(4);
                            string tur = reader.GetString(5);

                            int gunEkle = 0;
                            switch (step)
                            {
                                case 0: gunEkle = 1; break;
                                case 1: gunEkle = 7; break;
                                case 2: gunEkle = 30; break;
                                case 3: gunEkle = 90; break;
                                case 4: gunEkle = 180; break;
                                case 5: gunEkle = 365; break;
                                default: gunEkle = 0; break;
                            }

                            DateTime testTarihi = lastCorrect.AddDays(gunEkle);
                            if (testTarihi <= DateTime.Now)
                            {
                                quizKelimeleri.Add(new Word
                                {
                                    WordId = wordId,
                                    EngWordName = eng,
                                    TurWordName = tur,
                                    CorrectStreak = correctStreak
                                });
                            }
                        }
                    }
                }
            }

            dataGridView1.DataSource = quizKelimeleri.Select(k => new
            {
                İngilizce = k.EngWordName,
                DoğruBilmeSayısı = k.CorrectStreak
            }).ToList();

            if (quizKelimeleri.Count > 0)
            {
                aktifKelime = quizKelimeleri[aktifIndex];
                lblKelime.Text = aktifKelime.EngWordName;
            }
            else
            {
                lblKelime.Text = "Bugünlük test yok 📭";
                txtCevap.Enabled = false;
                btnCevapla.Enabled = false;
            }
        }

        private async void btnCevapla_Click(object sender, EventArgs e)
        {
            if (aktifKelime == null || aktifKelime.TurWordName == null)
            {
                MessageBox.Show("⚠️ HATA: Kelime bilgisi eksik geldi! Veritabanı kontrol edilmeli.");
                return;
            }

            string kullaniciCevap = txtCevap.Text.Trim().ToLower();
            string dogruCevap = aktifKelime.TurWordName.ToLower();
            bool dogru = kullaniciCevap == dogruCevap;

            string connStr = "Server=localhost;Database=KelimeEzberlemeKG;Trusted_Connection=True;";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                await conn.OpenAsync();

                // Log tablosuna kayıt
                string tablo = dogru ? "DogruBilinenler" : "YanlisBilinenler";
                SqlCommand cmdLog = new SqlCommand($@"
                    INSERT INTO {tablo} (UserId, WordId, TestDate)
                    VALUES (@UserId, @WordId, GETDATE())", conn);
                cmdLog.Parameters.AddWithValue("@UserId", AktifKullanici.KullaniciId);
                cmdLog.Parameters.AddWithValue("@WordId", aktifKelime.WordId);
                await cmdLog.ExecuteNonQueryAsync();

                // Progress güncelle
                string updateSql = dogru
                    ? @"
                        UPDATE WordProgresses
                        SET StepIndex = CASE WHEN StepIndex < 5 THEN StepIndex + 1 ELSE StepIndex END,
                            LastCorrectDate = GETDATE(),
                            CorrectStreak = CorrectStreak + 1,
                            IsMastered = CASE WHEN StepIndex = 5 THEN 1 ELSE 0 END
                        WHERE UserId = @UserId AND WordId = @WordId;"
                    : @"
                        UPDATE WordProgresses
                        SET StepIndex = 0,
                            LastCorrectDate = GETDATE(),
                            CorrectStreak = 0,
                            IsMastered = 0
                        WHERE UserId = @UserId AND WordId = @WordId;";

                SqlCommand cmdUpdate = new SqlCommand(updateSql, conn);
                cmdUpdate.Parameters.AddWithValue("@UserId", AktifKullanici.KullaniciId);
                cmdUpdate.Parameters.AddWithValue("@WordId", aktifKelime.WordId);
                await cmdUpdate.ExecuteNonQueryAsync();
            }

            lblDurum.Text = dogru ? "✅ Doğru!" : $"❌ Yanlış! Doğru: {aktifKelime.TurWordName}";
            aktifIndex++;
            txtCevap.Clear();

            if (aktifIndex < quizKelimeleri.Count)
            {
                aktifKelime = quizKelimeleri[aktifIndex];
                lblKelime.Text = aktifKelime.EngWordName;
            }
            else
            {
                MessageBox.Show("🎉 Tüm test zamanı gelen kelimeler tamamlandı!" +
                    "Güncel liste ekranda gösterilmektedir.");
                lblKelime.Text = "Test bitti!";
                txtCevap.Enabled = false;
                btnCevapla.Enabled = false;
                await DataGridViewGuncelle(); // Veritabanını güncel verilerle yeniden yükle
            }
        }

        private async Task DataGridViewGuncelle()
        {
            int userId = AktifKullanici.KullaniciId;
            string connStr = "Server=localhost;Database=KelimeEzberlemeKG;Trusted_Connection=True;";
            var guncelKelimeler = new List<Word>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                await conn.OpenAsync();

                string query = @"SELECT wp.WordId, wp.CorrectStreak, w.EngWordName
                                 FROM WordProgresses wp
                                 JOIN Words w ON wp.WordId = w.WordId
                                 WHERE wp.UserId = @userId AND wp.IsMastered = 0";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            guncelKelimeler.Add(new Word
                            {
                                WordId = reader.GetInt32(0),
                                CorrectStreak = reader.GetInt32(1),
                                EngWordName = reader.GetString(2)
                            });
                        }
                    }
                }
            }

            dataGridView1.DataSource = guncelKelimeler.Select(k => new
            {
                İngilizce = k.EngWordName,
                DoğruBilmeSayısı = k.CorrectStreak
            }).ToList();
        }

        private void txtCevap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnCevapla.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            form6.Show();
            this.Hide();
        }
    }

    public class Word
    {
        public int WordId { get; set; }
        public string EngWordName { get; set; }
        public string TurWordName { get; set; }
        public int CorrectStreak { get; set; }
    }
}
