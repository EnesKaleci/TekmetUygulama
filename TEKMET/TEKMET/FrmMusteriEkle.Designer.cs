namespace TEKMET
{
    partial class FrmMusteriEkle
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
            this.btnSil = new System.Windows.Forms.Button();
            this.btnYeni = new System.Windows.Forms.Button();
            this.btnKaydet = new System.Windows.Forms.Button();
            this.txtAdSoyad = new System.Windows.Forms.TextBox();
            this.txtTelefon = new System.Windows.Forms.TextBox();
            this.txtAdres = new System.Windows.Forms.TextBox();
            this.dgvMusteriler = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.Adres = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMusteriler)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSil
            // 
            this.btnSil.Location = new System.Drawing.Point(178, 156);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(75, 23);
            this.btnSil.TabIndex = 0;
            this.btnSil.Text = "Sil";
            this.btnSil.UseVisualStyleBackColor = true;
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);
            // 
            // btnYeni
            // 
            this.btnYeni.Location = new System.Drawing.Point(16, 156);
            this.btnYeni.Name = "btnYeni";
            this.btnYeni.Size = new System.Drawing.Size(75, 23);
            this.btnYeni.TabIndex = 1;
            this.btnYeni.Text = "Yeni";
            this.btnYeni.UseVisualStyleBackColor = true;
            this.btnYeni.Click += new System.EventHandler(this.btnYeni_Click);
            // 
            // btnKaydet
            // 
            this.btnKaydet.Location = new System.Drawing.Point(97, 156);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(75, 23);
            this.btnKaydet.TabIndex = 2;
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.UseVisualStyleBackColor = true;
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            // 
            // txtAdSoyad
            // 
            this.txtAdSoyad.Location = new System.Drawing.Point(102, 21);
            this.txtAdSoyad.Name = "txtAdSoyad";
            this.txtAdSoyad.Size = new System.Drawing.Size(151, 20);
            this.txtAdSoyad.TabIndex = 3;
            // 
            // txtTelefon
            // 
            this.txtTelefon.Location = new System.Drawing.Point(102, 52);
            this.txtTelefon.MaxLength = 11;
            this.txtTelefon.Name = "txtTelefon";
            this.txtTelefon.Size = new System.Drawing.Size(151, 20);
            this.txtTelefon.TabIndex = 4;
            this.txtTelefon.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTelefon_KeyPress);
            // 
            // txtAdres
            // 
            this.txtAdres.Location = new System.Drawing.Point(102, 78);
            this.txtAdres.Multiline = true;
            this.txtAdres.Name = "txtAdres";
            this.txtAdres.Size = new System.Drawing.Size(151, 67);
            this.txtAdres.TabIndex = 5;
            // 
            // dgvMusteriler
            // 
            this.dgvMusteriler.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMusteriler.Location = new System.Drawing.Point(259, 21);
            this.dgvMusteriler.Name = "dgvMusteriler";
            this.dgvMusteriler.Size = new System.Drawing.Size(572, 158);
            this.dgvMusteriler.TabIndex = 6;
            this.dgvMusteriler.SelectionChanged += new System.EventHandler(this.dgvMusteriler_SelectionChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 19);
            this.label1.TabIndex = 7;
            this.label1.Text = "AD SOYAD:";
            // 
            // Adres
            // 
            this.Adres.AutoSize = true;
            this.Adres.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold);
            this.Adres.Location = new System.Drawing.Point(44, 98);
            this.Adres.Name = "Adres";
            this.Adres.Size = new System.Drawing.Size(52, 19);
            this.Adres.TabIndex = 8;
            this.Adres.Text = "Adres:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(33, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 19);
            this.label4.TabIndex = 10;
            this.label4.Text = "Telefon:";
            // 
            // FrmMusteriEkle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 196);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Adres);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvMusteriler);
            this.Controls.Add(this.txtAdres);
            this.Controls.Add(this.txtTelefon);
            this.Controls.Add(this.txtAdSoyad);
            this.Controls.Add(this.btnKaydet);
            this.Controls.Add(this.btnYeni);
            this.Controls.Add(this.btnSil);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmMusteriEkle";
            this.Text = "Müsteri Ekle";
            this.Load += new System.EventHandler(this.FrmMusteriEkle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMusteriler)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSil;
        private System.Windows.Forms.Button btnYeni;
        private System.Windows.Forms.Button btnKaydet;
        private System.Windows.Forms.TextBox txtAdSoyad;
        private System.Windows.Forms.TextBox txtTelefon;
        private System.Windows.Forms.TextBox txtAdres;
        private System.Windows.Forms.DataGridView dgvMusteriler;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Adres;
        private System.Windows.Forms.Label label4;
    }
}