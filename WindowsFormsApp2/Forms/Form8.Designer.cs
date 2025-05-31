namespace WindowsFormsApp2
{
    partial class Form8
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
            this.lblMetin = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblMetin
            // 
            this.lblMetin.Location = new System.Drawing.Point(47, 157);
            this.lblMetin.Name = "lblMetin";
            this.lblMetin.Size = new System.Drawing.Size(765, 293);
            this.lblMetin.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button2.Location = new System.Drawing.Point(12, 576);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(61, 35);
            this.button2.TabIndex = 5;
            this.button2.Text = "◀";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(308, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(247, 74);
            this.label1.TabIndex = 6;
            this.label1.Text = "Haydi!\r\nBildiğin Kelimelerden Hikaye Oluşturalım";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form8
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 620);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.lblMetin);
            this.Name = "Form8";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hikaye Zamanı";
            this.Load += new System.EventHandler(this.Form8_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblMetin;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
    }
}