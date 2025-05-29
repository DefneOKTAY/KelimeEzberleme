using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp2
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Boş alan kontrolü
            if (string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtKullaniciAdi.Text) ||
                string.IsNullOrWhiteSpace(txtSifre.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // SQL Server bağlantı cümlesi
            string conString = "Server=localhost;Database=KelimeEzberlemeKG;Trusted_Connection=True;";

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();

                    // E-posta kontrolü
                    string emailQuery = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
                    SqlCommand emailCmd = new SqlCommand(emailQuery, con);
                    emailCmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    int emailCount = (int)emailCmd.ExecuteScalar();

                    if (emailCount > 0)
                    {
                        MessageBox.Show("Bu e-posta adresi zaten kayıtlı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Kullanıcı adı kontrolü
                    string usernameQuery = "SELECT COUNT(*) FROM Users WHERE UserName = @UserName";
                    SqlCommand usernameCmd = new SqlCommand(usernameQuery, con);
                    usernameCmd.Parameters.AddWithValue("@UserName", txtKullaniciAdi.Text);
                    int usernameCount = (int)usernameCmd.ExecuteScalar();

                    if (usernameCount > 0)
                    {
                        MessageBox.Show("Bu kullanıcı adı zaten kullanılıyor.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Kayıt işlemi
                    string insertQuery = "INSERT INTO Users (Email, UserName, Password) VALUES (@Email, @UserName, @Password)";
                    SqlCommand cmd = new SqlCommand(insertQuery, con);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@UserName", txtKullaniciAdi.Text);
                    cmd.Parameters.AddWithValue("@Password", txtSifre.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Kayıt başarılı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form1 girisFormu = new Form1();
                    girisFormu.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
    }
}
