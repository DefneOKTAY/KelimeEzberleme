using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form9 : Form
    {
        private string gizliKelime = "";
        private int kalanHak = 6;

        public Form9()
        {
            InitializeComponent();
        }

        private async void Form9_Load(object sender, EventArgs e)
        {
            var kelimeler = await BugunDogruBilinenKelimeleriGetir();
            var uygunlar = kelimeler.Where(k => k.Length <= 5).ToList();

            if (uygunlar.Count == 0)
            {
                MessageBox.Show("Bugün 5 harften kısa doğru bilinen kelime yok!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            gizliKelime = uygunlar.OrderBy(x => Guid.NewGuid()).First().ToUpper();
            lblBilgi.Text = $"Tahmin et! ({gizliKelime.Length} harfli kelime)";
        }

        private async Task<List<string>> BugunDogruBilinenKelimeleriGetir()
        {
            List<string> kelimeler = new List<string>();
            string conStr = "Server=localhost;Database=KelimeEzberlemeKG;Trusted_Connection=True;";
            string sql = @"
                SELECT w.EngWordName
                FROM DogruBilinenler d
                JOIN Words w ON d.WordId = w.WordId
                WHERE d.UserId = @userId AND CAST(d.TestDate AS DATE) = CAST(GETDATE() AS DATE)";

            using (SqlConnection conn = new SqlConnection(conStr))
            {
                await conn.OpenAsync();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userId", AktifKullanici.KullaniciId);

                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    kelimeler.Add(reader.GetString(0).ToUpper());
                }
            }

            return kelimeler;
        }

        private void btnTahminEt_Click(object sender, EventArgs e)
        {
            string tahmin = txtTahmin.Text.Trim().ToUpper();

            if (tahmin.Length != gizliKelime.Length)
            {
                MessageBox.Show($"{gizliKelime.Length} harfli bir kelime girmelisin.", "Uyarı");
                return;
            }

            WordleKarsilastirVeCiz(gizliKelime, tahmin);
            txtTahmin.Clear();
            kalanHak--;

            if (tahmin == gizliKelime)
            {
                MessageBox.Show("🎉 Doğru bildin! Tebrikler!", "Başarı");
                btnTahminEt.Enabled = false;
                return;
            }

            if (kalanHak == 0)
            {
                MessageBox.Show($"❌ Oyun bitti! Doğru kelime: {gizliKelime}", "Kaybettin");
                btnTahminEt.Enabled = false;
            }
        }

        private void WordleKarsilastirVeCiz(string hedef, string tahmin)
        {
            int row = tableLayoutPanel1.RowCount++;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));

            char[] hedefChars = hedef.ToCharArray();
            char[] tahminChars = tahmin.ToCharArray();
            Color[] renkler = new Color[5];
            bool[] kullanildi = new bool[5];

            // 1. yeşil (doğru harf doğru yer)
            for (int i = 0; i < hedef.Length; i++)
            {
                if (i < tahmin.Length && tahmin[i] == hedef[i])
                {
                    renkler[i] = Color.LightGreen;
                    kullanildi[i] = true;
                }
                else
                {
                    renkler[i] = Color.LightGray;
                }
            }

            // 2. sarı (doğru harf yanlış yer)
            for (int i = 0; i < hedef.Length; i++)
            {
                if (renkler[i] == Color.LightGreen) continue;

                for (int j = 0; j < hedef.Length; j++)
                {
                    if (!kullanildi[j] && tahmin[i] == hedef[j])
                    {
                        renkler[i] = Color.Gold;
                        kullanildi[j] = true;
                        break;
                    }
                }
            }

            // Görsel olarak çiz
            for (int i = 0; i < hedef.Length; i++)
            {
                Label lbl = new Label
                {
                    Text = tahmin[i].ToString(),
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 14, FontStyle.Bold),
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = renkler[i]
                };
                tableLayoutPanel1.Controls.Add(lbl, i, row);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Form6 f6 = new Form6();
            f6.Show();
            this.Hide();
        }

      
    }
}

