
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form5 : Form
    {
        string aktifKelime = "";

        public Form5(string kelime)
        {
            InitializeComponent();
            aktifKelime = kelime;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            label1.Text = "Kelimeye Ait Görsel: " + aktifKelime;
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            string conStr = "Server=DEFNE\\SQLEXPRESS;Database=KelimeEzberlemeKG;Trusted_Connection=True;";
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                conn.Open();
                string query = "SELECT TOP 1 ResimYolu FROM KelimeResimleri WHERE Kelime = @kelime ORDER BY ID DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@kelime", aktifKelime);
                object sonuc = cmd.ExecuteScalar();

                if (sonuc != null)
                {
                    string yol = sonuc.ToString();
                    if (File.Exists(yol))
                    {
                        pictureBox1.Image = Image.FromFile(yol);
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) // URL'den resim yükle
        {
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            MessageBox.Show("Aktif kelime: " + aktifKelime);
            MessageBox.Show("Butona basıldı!");
            string url = textBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(url))
            {
                MessageBox.Show("Lütfen geçerli bir görsel URL'si girin.");
                return;
            }

            if (string.IsNullOrWhiteSpace(aktifKelime))
            {
                MessageBox.Show("Kelime bilgisi alınamadı.");
                return;
            }

            try
            {
                string klasor = Path.Combine(Application.StartupPath, "images5");
                Directory.CreateDirectory(klasor);

                string uzanti = Path.GetExtension(new Uri(url).AbsolutePath);
                if (string.IsNullOrEmpty(uzanti)) uzanti = ".jpg";

                string dosyaAdi = aktifKelime + "_" + Guid.NewGuid().ToString().Substring(0, 5) + uzanti;
                string hedefYol = Path.Combine(klasor, dosyaAdi);

                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(url, hedefYol);
                }

                // DEBUG: indirilen dosya yolu
                MessageBox.Show("Görsel yolu: " + hedefYol);

                if (!File.Exists(hedefYol))
                {
                    MessageBox.Show("Görsel indirilemedi.");
                    return;
                }

                string conStr = "Server=DEFNE;Database=KelimeEzberlemeKG;Trusted_Connection=True;";
                using (SqlConnection conn = new SqlConnection(conStr))
                {
                    conn.Open();

                    string kontrolQuery = "SELECT COUNT(*) FROM KelimeResimleri WHERE Kelime = @kelime AND ResimYolu = @yol";
                    SqlCommand kontrolCmd = new SqlCommand(kontrolQuery, conn);
                    kontrolCmd.Parameters.AddWithValue("@kelime", aktifKelime);
                    kontrolCmd.Parameters.AddWithValue("@yol", hedefYol);
                    int varMi = (int)kontrolCmd.ExecuteScalar();

                    if (varMi == 0)
                    {
                        string insertQuery = "INSERT INTO KelimeResimleri (Kelime, ResimYolu) VALUES (@kelime, @yol)";
                        SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                        insertCmd.Parameters.AddWithValue("@kelime", aktifKelime);
                        insertCmd.Parameters.AddWithValue("@yol", hedefYol);

                        int etkilenen = insertCmd.ExecuteNonQuery();

                        MessageBox.Show("✅ Görsel veritabanına kaydedildi.");
                    }
                    else
                    {
                        MessageBox.Show("ℹ️ Bu görsel zaten veritabanında kayıtlı.");
                    }
                }

                pictureBox1.Image = Image.FromFile(hedefYol);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Hata oluştu: " + ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(AktifKullanici.KullaniciAdi);
            form2.Show();
            this.Hide();

        }
    }
}
