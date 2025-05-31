using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        private async void Form8_Load(object sender, EventArgs e)
        {
            int userId = AktifKullanici.KullaniciId;
            DateTime bugun = DateTime.Today;
            List<string> kelimeler = new List<string>();
            string connStr = "Server=localhost;Database=KelimeEzberlemeKG;Trusted_Connection=True;";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                await conn.OpenAsync();
                string query = @"
                    SELECT w.EngWordName
                    FROM DogruBilinenler d
                    JOIN Words w ON d.WordId = w.WordId
                    WHERE d.UserId = @userId AND CAST(d.TestDate AS DATE) = @bugun";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@bugun", bugun);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            kelimeler.Add(reader.GetString(0));
                        }
                    }
                }
            }

        
            lblMetin.Text = "📖 Hikaye oluşturuluyor...";
            string prompt = "Write a short story using these English words: " + string.Join(", ", kelimeler);
            string metin = await CohereIleHikayeOlustur(prompt);
            lblMetin.Text = metin;

        }

        private async Task<string> CohereIleHikayeOlustur(string prompt)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer yKBZCLLUDb7aexkHYsaeJgED1I6Qr15hhOmtnbqP");

            var requestBody = new
            {
                model = "command-r-plus",
                prompt = prompt,
                max_tokens = 300,
                temperature = 0.7
            };

            string jsonBody = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://api.cohere.ai/v1/generate", content);

            if (!response.IsSuccessStatusCode)
            {
                string hata = await response.Content.ReadAsStringAsync();
                MessageBox.Show("❌ Cohere API Hatası:\n" + hata);
                return "⚠️ Hikaye oluşturulamadı.";
            }

            string json = await response.Content.ReadAsStringAsync();
            JsonDocument doc = JsonDocument.Parse(json);
            return doc.RootElement.GetProperty("generations")[0].GetProperty("text").GetString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            form6.Show();
            this.Hide();
        }

    }
}