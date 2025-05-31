namespace WindowsFormsApp2
{
    partial class Form7
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblKelime = new System.Windows.Forms.Label();
            this.txtCevap = new System.Windows.Forms.TextBox();
            this.btnCevapla = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblDurum = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblKelime
            // 
            this.lblKelime.Location = new System.Drawing.Point(45, 105);
            this.lblKelime.Name = "lblKelime";
            this.lblKelime.Size = new System.Drawing.Size(229, 22);
            this.lblKelime.TabIndex = 0;
            this.lblKelime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCevap
            // 
            this.txtCevap.Location = new System.Drawing.Point(319, 105);
            this.txtCevap.Name = "txtCevap";
            this.txtCevap.Size = new System.Drawing.Size(173, 22);
            this.txtCevap.TabIndex = 1;
            // 
            // btnCevapla
            // 
            this.btnCevapla.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCevapla.Font = new System.Drawing.Font("Modern No. 20", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCevapla.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCevapla.Location = new System.Drawing.Point(368, 170);
            this.btnCevapla.Name = "btnCevapla";
            this.btnCevapla.Size = new System.Drawing.Size(82, 25);
            this.btnCevapla.TabIndex = 2;
            this.btnCevapla.Text = "Kontrol";
            this.btnCevapla.UseVisualStyleBackColor = true;
            this.btnCevapla.Click += new System.EventHandler(this.btnCevapla_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(199, 317);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(402, 224);
            this.dataGridView1.TabIndex = 3;
            // 
            // lblDurum
            // 
            this.lblDurum.Location = new System.Drawing.Point(514, 105);
            this.lblDurum.Name = "lblDurum";
            this.lblDurum.Size = new System.Drawing.Size(230, 22);
            this.lblDurum.TabIndex = 4;
            this.lblDurum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button2.Location = new System.Drawing.Point(12, 512);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 29);
            this.button2.TabIndex = 9;
            this.button2.Text = "◀";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(319, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 70);
            this.label1.TabIndex = 10;
            this.label1.Text = "Test Zamanı Gelenler";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(196, 218);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(405, 85);
            this.label2.TabIndex = 11;
            this.label2.Text = "Bilemediğiniz kelimeler sıfırlanıp günlük testte tekrar karşınıza çıkabilir.\r\n6 k" +
    "ez tekrar bildiğiniz kelimeler soru havuzundan çıkartılacaktır.";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form7
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 553);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.lblDurum);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnCevapla);
            this.Controls.Add(this.txtCevap);
            this.Controls.Add(this.lblKelime);
            this.MaximizeBox = false;
            this.Name = "Form7";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "6 Sefer Tekrar - Kesin Öğrenme";
            this.Load += new System.EventHandler(this.Form7_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCevap_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblKelime;
        private System.Windows.Forms.TextBox txtCevap;
        private System.Windows.Forms.Button btnCevapla;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblDurum;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}