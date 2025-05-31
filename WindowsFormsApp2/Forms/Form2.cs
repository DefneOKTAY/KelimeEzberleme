using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace WindowsFormsApp2
{
    public partial class Form2 : Form
    {
        private string girenKullaniciAdi;
        public Form2(string kullaniciAdi)
        {
            InitializeComponent();
            girenKullaniciAdi = kullaniciAdi;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string engWord = textBox1.Text.Trim();
                string turWord = textBox2.Text.Trim();
                string sample = textBox3.Text.Trim();

                if (string.IsNullOrWhiteSpace(engWord) || string.IsNullOrWhiteSpace(turWord) || string.IsNullOrWhiteSpace(sample))
                {
                    MessageBox.Show("❗ Lütfen İngilizce, Türkçe ve Örnek Cümle alanlarını doldurun.");
                    return;
                }

                // Görsel henüz eklenmeyecek.
                string kaydedilecekYol = "";

                string connStr = "Server=.;Database=KelimeEzberlemeKG;Trusted_Connection=True;";

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    // 1. Kelimeyi ekle.
                    string insertWord = @"
                INSERT INTO Words (EngWordName, TurWordName, Picture, KullaniciAdi)
                VALUES (@eng, @tur, @pic, @kulAdi);
                SELECT SCOPE_IDENTITY();
            ";

                    SqlCommand cmd1 = new SqlCommand(insertWord, conn);
                    cmd1.Parameters.AddWithValue("@eng", engWord);
                    cmd1.Parameters.AddWithValue("@tur", turWord);
                    cmd1.Parameters.AddWithValue("@pic", kaydedilecekYol); // şimdilik boş
                    cmd1.Parameters.AddWithValue("@kulAdi", girenKullaniciAdi);

                    int wordID = Convert.ToInt32(cmd1.ExecuteScalar());

                    // 2. Örnek cümleyi ekle.
                    string insertSample = @"
                INSERT INTO WordSamples (WordID, Samples)
                VALUES (@wordID, @sample);
            ";

                    SqlCommand cmd2 = new SqlCommand(insertSample, conn);
                    cmd2.Parameters.AddWithValue("@wordID", wordID);
                    cmd2.Parameters.AddWithValue("@sample", sample);
                    cmd2.ExecuteNonQuery();
                }

                // 3. Başarılıysa kullanıcıya sor: Görsel eklemek ister misin?
                DialogResult result = MessageBox.Show("✅ Kelime kaydedildi!\n📷 Bu kelimeye ait bir görsel eklemek ister misiniz?", "Görsel Ekle", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    // Form5'e kelime gönder
                    Form5 f5 = new Form5(engWord); // İngilizce kelimeyi gönderiyoruz
                    f5.Show();
                    this.Hide();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("HATA: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Form6 f6 = new Form6();
            f6.Show();
            this.Hide();
        }

       
    }
}
    

