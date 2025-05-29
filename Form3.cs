using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace WindowsFormsApp2
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Boş alan kontrolü
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Lütfen e-posta adresinizi girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string kullaniciMail = textBox1.Text.Trim();
            string sifre = "";
            string conString = "Server=localhost;Database=KelimeEzberlemeKG;Trusted_Connection=True;";
            bool kullaniciVar = false;

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Password FROM Users WHERE Email = @Email", con);
                cmd.Parameters.AddWithValue("@Email", kullaniciMail);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    sifre = reader["Password"].ToString();
                    kullaniciVar = true;
                }
                con.Close();
            }

            if (kullaniciVar && !string.IsNullOrWhiteSpace(sifre))
            {
                try
                {
                    MessageBox.Show("Gönderilecek e-posta: " + kullaniciMail);
                    MailMessage mail = new MailMessage();
                    SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress("defneoktay35@gmail.com");
                    mail.To.Add(kullaniciMail); // TextBox1'e yazılan e-posta adresine gönderilecek
                    mail.Subject = "Kelime Kutusu - Şifre Hatırlatma";
                    mail.Body = $"Merhaba,\n\nŞifreniz: {sifre}\n\nİyi günler dileriz.";

                    smtpServer.Port = 587;
                    smtpServer.Credentials = new NetworkCredential("defneoktay35@gmail.com", "zxnl spkx amkk xmto");
                    smtpServer.EnableSsl = true;

                    smtpServer.Send(mail);
                    MessageBox.Show("Şifre e-posta adresinize gönderildi!", "Başarılı");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Mail gönderilemedi: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Bu e-posta adresi sistemde kayıtlı değil.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
