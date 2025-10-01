namespace TEKMET
{
    partial class FrmYeniIs
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIsTanimi = new System.Windows.Forms.TextBox();
            this.dtpIsTarihi = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.nudFiyat = new System.Windows.Forms.NumericUpDown();
            this.btnKaydet = new System.Windows.Forms.Button();
            this.btnIptal = new System.Windows.Forms.Button();
            this.btnSil = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudFiyat)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(24, 71);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 34);
            this.label1.TabIndex = 0;
            this.label1.Text = "İş Tanımı:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(38, 140);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 34);
            this.label2.TabIndex = 1;
            this.label2.Text = "İş Tarihi:";
            // 
            // txtIsTanimi
            // 
            this.txtIsTanimi.Location = new System.Drawing.Point(168, 13);
            this.txtIsTanimi.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtIsTanimi.Multiline = true;
            this.txtIsTanimi.Name = "txtIsTanimi";
            this.txtIsTanimi.Size = new System.Drawing.Size(626, 117);
            this.txtIsTanimi.TabIndex = 2;
            // 
            // dtpIsTarihi
            // 
            this.dtpIsTarihi.Location = new System.Drawing.Point(168, 146);
            this.dtpIsTarihi.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.dtpIsTarihi.Name = "dtpIsTarihi";
            this.dtpIsTarihi.Size = new System.Drawing.Size(396, 31);
            this.dtpIsTarihi.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(80, 192);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 34);
            this.label3.TabIndex = 4;
            this.label3.Text = "Fiyat:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // nudFiyat
            // 
            this.nudFiyat.Location = new System.Drawing.Point(168, 196);
            this.nudFiyat.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.nudFiyat.Maximum = new decimal(new int[] {
            -1981284352,
            -1966660860,
            0,
            0});
            this.nudFiyat.Name = "nudFiyat";
            this.nudFiyat.Size = new System.Drawing.Size(240, 31);
            this.nudFiyat.TabIndex = 5;
            // 
            // btnKaydet
            // 
            this.btnKaydet.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnKaydet.Location = new System.Drawing.Point(32, 246);
            this.btnKaydet.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(150, 60);
            this.btnKaydet.TabIndex = 6;
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.UseVisualStyleBackColor = true;
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            // 
            // btnIptal
            // 
            this.btnIptal.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnIptal.Location = new System.Drawing.Point(194, 246);
            this.btnIptal.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnIptal.Name = "btnIptal";
            this.btnIptal.Size = new System.Drawing.Size(150, 60);
            this.btnIptal.TabIndex = 7;
            this.btnIptal.Text = "Iptal";
            this.btnIptal.UseVisualStyleBackColor = true;
            this.btnIptal.Click += new System.EventHandler(this.btnIptal_Click);
            // 
            // btnSil
            // 
            this.btnSil.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSil.Location = new System.Drawing.Point(356, 246);
            this.btnSil.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(158, 60);
            this.btnSil.TabIndex = 8;
            this.btnSil.Text = "Sil";
            this.btnSil.UseVisualStyleBackColor = true;
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(577, 216);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(150, 29);
            this.checkBox1.TabIndex = 9;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // FrmYeniIs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 348);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.btnSil);
            this.Controls.Add(this.btnIptal);
            this.Controls.Add(this.btnKaydet);
            this.Controls.Add(this.nudFiyat);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpIsTarihi);
            this.Controls.Add(this.txtIsTanimi);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.MaximizeBox = false;
            this.Name = "FrmYeniIs";
            this.Text = "İş Ekleme Formu";
            this.Load += new System.EventHandler(this.FrmYeniIs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudFiyat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIsTanimi;
        private System.Windows.Forms.DateTimePicker dtpIsTarihi;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudFiyat;
        private System.Windows.Forms.Button btnKaydet;
        private System.Windows.Forms.Button btnIptal;
        private System.Windows.Forms.Button btnSil;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}