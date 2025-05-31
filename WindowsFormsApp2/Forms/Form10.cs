using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form10 : Form
    {
        private const string conStr = "Server=localhost;Database=KelimeEzberlemeKG;Trusted_Connection=True;";
        private DataTable raporTablosu;

        public Form10()
        {
            InitializeComponent();
        }

        private async void Form10_Load(object sender, EventArgs e)
        {
            int userId = AktifKullanici.KullaniciId;

            using (SqlConnection conn = new SqlConnection(conStr))
            {
                await conn.OpenAsync();

                string sorgu = @"SELECT COUNT(*) FROM GunlukTestTakip 
                                 WHERE UserId = @UserId AND TestDate = CAST(GETDATE() AS DATE)";
                using (SqlCommand cmd = new SqlCommand(sorgu, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    int sayi = (int)await cmd.ExecuteScalarAsync();

                    if (sayi > 0)
                    {
                        MessageBox.Show("Bugünkü test zaten yapılmış!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDailyTestCount.Text = "";
                        txtDailyTestCount.ReadOnly = true;
                        btnSave.Enabled = false;
                        return;
                    }
                }

                txtDailyTestCount.Text = AktifKullanici.DailyTestCount.ToString();
                txtDailyTestCount.ReadOnly = false;
                btnSave.Enabled = true;
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
        "Hangi sayfaya geçmek istiyorsunuz?\nEvet: Kelime Ekleme\nHayır: Sınav Formu",
        "Seçim Yapın",
        MessageBoxButtons.YesNoCancel,
        MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Form2 kelimeFormu = new Form2(AktifKullanici.KullaniciAdi);
                kelimeFormu.Show();
                this.Hide();
            }
            else if (result == DialogResult.No)
            {
                int soruSayisi;
                bool bugunYapildi = await BugunTestYapildiMi();

                if (!int.TryParse(txtDailyTestCount.Text.Trim(), out soruSayisi) || soruSayisi <= 0)
                {
                    if (!bugunYapildi) // ✅ Sadece test yapılmadıysa uyarı ver
                    {
                        MessageBox.Show("Geçersiz giriş yapıldı. Varsayılan 10 soruyla devam edilecek.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    soruSayisi = 10;
                }

                AktifKullanici.DailyTestCount = soruSayisi;
                Form6 sinavFormu = new Form6();
                sinavFormu.Form10danGelenSoruSayisi = soruSayisi;
                sinavFormu.Show();
                this.Hide();
            }
        }

        private async Task<bool> BugunTestYapildiMi()
        {
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                await conn.OpenAsync();

                string sql = @"SELECT COUNT(*) FROM GunlukTestTakip
                               WHERE UserId = @u AND TestDate = CAST(GETDATE() AS DATE)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@u", AktifKullanici.KullaniciId);
                    int count = (int)await cmd.ExecuteScalarAsync();
                    return count > 0;
                }
            }
        }

        private async void btnRapor_Click(object sender, EventArgs e)
        {
            if (!await BugunTestYapildiMi())
            {
                MessageBox.Show("Bugün test yapılmadı, sonuç gösterilemiyor.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = null;
                return;
            }

            DataTable dt = await GetGunlukBasariRaporu();
            raporTablosu = dt;
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["EngWordName"].HeaderText = "Kelimeler";
            dataGridView1.Columns["Sonuc"].HeaderText = "Durum";
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.DarkBlue;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private async Task<DataTable> GetGunlukBasariRaporu()
        {
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                await conn.OpenAsync();

                string sql = @"
                SELECT w.EngWordName, 'Doğru' AS Sonuc
                FROM DogruBilinenler d
                JOIN Words w ON d.WordId = w.WordID
                WHERE d.UserId = @u AND CAST(d.TestDate AS DATE) = CAST(GETDATE() AS DATE)

                UNION

                SELECT w.EngWordName, 'Yanlış' AS Sonuc
                FROM YanlisBilinenler y
                JOIN Words w ON y.WordId = w.WordID
                WHERE y.UserId = @u AND CAST(y.TestDate AS DATE) = CAST(GETDATE() AS DATE)
                ";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@u", AktifKullanici.KullaniciId);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        int dogru = 0, yanlis = 0;
                        foreach (DataRow row in dt.Rows)
                        {
                            string sonuc = row["Sonuc"].ToString();
                            if (sonuc == "Doğru") dogru++;
                            else if (sonuc == "Yanlış") yanlis++;
                        }

                        int toplam = dogru + yanlis;
                        string oran = toplam > 0 ? string.Format("{0}%", dogru * 100 / toplam) : "0%";

                        DataRow summaryRow = dt.NewRow();
                        summaryRow["EngWordName"] = "TOPLAM";
                        summaryRow["Sonuc"] = $"Doğru: {dogru}, Yanlış: {yanlis}, Başarı: {oran}";
                        dt.Rows.Add(summaryRow);

                        return dt;
                    }
                }
            }
        }

        private void btnYazdir_Click(object sender, EventArgs e)
        {
            if (raporTablosu == null || raporTablosu.Rows.Count == 0)
            {
                MessageBox.Show("Yazdırılacak rapor bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PrintDocument pd = new PrintDocument();
            pd.PrintPage += PrintDocument_PrintPage;

            PrintDialog printDlg = new PrintDialog();
            printDlg.Document = pd;

            if (printDlg.ShowDialog() == DialogResult.OK)
            {
                pd.Print();
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Bitmap bmp = new Bitmap(dataGridView1.Width, dataGridView1.Height);
            dataGridView1.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
            e.Graphics.DrawImage(bmp, 50, 50);
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (int.TryParse(txtDailyTestCount.Text, out int yeniSayi) && yeniSayi > 0)
            {
                AktifKullanici.DailyTestCount = yeniSayi;
                MessageBox.Show("Günlük test kelime sayısı kaydedildi.");
            }
            else
            {
                MessageBox.Show("Lütfen pozitif bir sayı giriniz.");
            }
        }
    }
}
