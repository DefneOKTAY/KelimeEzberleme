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
using WindowsFormsApp2; 


namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.FormClosed += Form3_FormClosed;
            form3.Show();
            this.Hide();
            form3.Show();
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = textBox1.Text.Trim(); 
            string sifre = textBox2.Text.Trim();

            string conString = "Server=localhost;Database=KelimeEzberlemeKG;Trusted_Connection=True;";
            bool kullaniciVar = false;

            using (SqlConnection con = new SqlConnection(conString))
            {
                string query = "SELECT UserID, Password FROM Users WHERE UserName = @UserName";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserName", kullaniciAdi);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    kullaniciVar = true;
                    string dogruSifre = reader["Password"].ToString();

                    if (sifre == dogruSifre)
                    {
                        AktifKullanici.KullaniciAdi = kullaniciAdi;
                        AktifKullanici.KullaniciId = Convert.ToInt32(reader["UserID"]);

                        MessageBox.Show("Giriş başarılı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Kullanıcı giriş sayfasına yönlendiriliyorsunuz mesajı
                        MessageBox.Show("Kullanıcı giriş sayfasına yönlendiriliyorsunuz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        Form10 form10 = new Form10();
                        form10.Show();

                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Şifre yanlış. Şifre yenileme sayfasına yönlendiriliyorsunuz.", "Hatalı Şifre", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Form3 form3 = new Form3(); // Şifremi Unuttum formu
                        form3.Show();
                        this.Hide();
                    }
                }

                con.Close();
            }

            if (!kullaniciVar)
            {
                MessageBox.Show("Kullanıcı bulunamadı. Lütfen kayıt olun.", "Kayıt Gerekli", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Form4 form4 = new Form4(); // Kayıt Ol formu
                form4.Show();
                this.Hide();
            }
        }
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.FormClosed += Form4_FormClosed;
            form4.Show();
            this.Hide();
            form4.Show();
        }
        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
