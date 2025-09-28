namespace TEKMET
{
    partial class FrmMusteriHesap
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
            this.lblAdSoyad = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblGuncelBakiye = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvIsler = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvOdemeler = new System.Windows.Forms.DataGridView();
            this.btnYeniIsEkle = new System.Windows.Forms.Button();
            this.btnYeniOdemeEkle = new System.Windows.Forms.Button();
            this.btnKapat = new System.Windows.Forms.Button();
            this.txtArama = new System.Windows.Forms.TextBox();
            this.dgvMusterilerListesi = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTelefon = new System.Windows.Forms.Label();
            this.btnPdfAktar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIsler)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOdemeler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMusterilerListesi)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Müşteri Adı:";
            // 
            // lblAdSoyad
            // 
            this.lblAdSoyad.AutoSize = true;
            this.lblAdSoyad.Location = new System.Drawing.Point(93, 18);
            this.lblAdSoyad.Name = "lblAdSoyad";
            this.lblAdSoyad.Size = new System.Drawing.Size(23, 13);
            this.lblAdSoyad.TabIndex = 1;
            this.lblAdSoyad.Text = "\"--\"";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(607, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "GÜNCEL BAKİYE:";
            // 
            // lblGuncelBakiye
            // 
            this.lblGuncelBakiye.AutoSize = true;
            this.lblGuncelBakiye.Location = new System.Drawing.Point(610, 35);
            this.lblGuncelBakiye.Name = "lblGuncelBakiye";
            this.lblGuncelBakiye.Size = new System.Drawing.Size(51, 13);
            this.lblGuncelBakiye.TabIndex = 3;
            this.lblGuncelBakiye.Text = "\"0.00TL\"";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvIsler);
            this.groupBox1.Location = new System.Drawing.Point(239, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(490, 152);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Müşteriye Yapılan İşler";
            // 
            // dgvIsler
            // 
            this.dgvIsler.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIsler.Location = new System.Drawing.Point(6, 19);
            this.dgvIsler.Name = "dgvIsler";
            this.dgvIsler.Size = new System.Drawing.Size(470, 110);
            this.dgvIsler.TabIndex = 0;
            this.dgvIsler.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvIsler_CellDoubleClick);
            this.dgvIsler.SelectionChanged += new System.EventHandler(this.dgvIsler_SelectionChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvOdemeler);
            this.groupBox2.Location = new System.Drawing.Point(239, 230);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(490, 128);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Seçili İşe Ait Ödemeler";
            // 
            // dgvOdemeler
            // 
            this.dgvOdemeler.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOdemeler.Location = new System.Drawing.Point(6, 19);
            this.dgvOdemeler.Name = "dgvOdemeler";
            this.dgvOdemeler.Size = new System.Drawing.Size(470, 98);
            this.dgvOdemeler.TabIndex = 0;
            // 
            // btnYeniIsEkle
            // 
            this.btnYeniIsEkle.Location = new System.Drawing.Point(12, 372);
            this.btnYeniIsEkle.Name = "btnYeniIsEkle";
            this.btnYeniIsEkle.Size = new System.Drawing.Size(75, 23);
            this.btnYeniIsEkle.TabIndex = 6;
            this.btnYeniIsEkle.Text = "Yeni İş Ekle";
            this.btnYeniIsEkle.UseVisualStyleBackColor = true;
            this.btnYeniIsEkle.Click += new System.EventHandler(this.btnYeniIsEkle_Click);
            // 
            // btnYeniOdemeEkle
            // 
            this.btnYeniOdemeEkle.Location = new System.Drawing.Point(93, 372);
            this.btnYeniOdemeEkle.Name = "btnYeniOdemeEkle";
            this.btnYeniOdemeEkle.Size = new System.Drawing.Size(75, 23);
            this.btnYeniOdemeEkle.TabIndex = 7;
            this.btnYeniOdemeEkle.Text = "Yeni Ödeme Ekle";
            this.btnYeniOdemeEkle.UseVisualStyleBackColor = true;
            this.btnYeniOdemeEkle.Click += new System.EventHandler(this.btnYeniOdemeEkle_Click);
            // 
            // btnKapat
            // 
            this.btnKapat.Location = new System.Drawing.Point(174, 372);
            this.btnKapat.Name = "btnKapat";
            this.btnKapat.Size = new System.Drawing.Size(75, 23);
            this.btnKapat.TabIndex = 8;
            this.btnKapat.Text = "Kapat";
            this.btnKapat.UseVisualStyleBackColor = true;
            this.btnKapat.Click += new System.EventHandler(this.btnKapat_Click);
            // 
            // txtArama
            // 
            this.txtArama.Location = new System.Drawing.Point(12, 327);
            this.txtArama.Name = "txtArama";
            this.txtArama.Size = new System.Drawing.Size(100, 20);
            this.txtArama.TabIndex = 9;
            this.txtArama.TextChanged += new System.EventHandler(this.txtArama_TextChanged);
            // 
            // dgvMusterilerListesi
            // 
            this.dgvMusterilerListesi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMusterilerListesi.Location = new System.Drawing.Point(12, 74);
            this.dgvMusterilerListesi.Name = "dgvMusterilerListesi";
            this.dgvMusterilerListesi.Size = new System.Drawing.Size(165, 247);
            this.dgvMusterilerListesi.TabIndex = 10;
            this.dgvMusterilerListesi.SelectionChanged += new System.EventHandler(this.dgvMusterilerListesi_SelectionChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Telefon:";
            // 
            // lblTelefon
            // 
            this.lblTelefon.AutoSize = true;
            this.lblTelefon.Location = new System.Drawing.Point(93, 35);
            this.lblTelefon.Name = "lblTelefon";
            this.lblTelefon.Size = new System.Drawing.Size(23, 13);
            this.lblTelefon.TabIndex = 12;
            this.lblTelefon.Text = "\"--\"";
            // 
            // btnPdfAktar
            // 
            this.btnPdfAktar.Location = new System.Drawing.Point(255, 372);
            this.btnPdfAktar.Name = "btnPdfAktar";
            this.btnPdfAktar.Size = new System.Drawing.Size(75, 23);
            this.btnPdfAktar.TabIndex = 13;
            this.btnPdfAktar.Text = "PDF Hesap Ekstresi";
            this.btnPdfAktar.UseVisualStyleBackColor = true;
            this.btnPdfAktar.Visible = false;
            // 
            // FrmMusteriHesap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 412);
            this.Controls.Add(this.btnPdfAktar);
            this.Controls.Add(this.lblTelefon);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgvMusterilerListesi);
            this.Controls.Add(this.txtArama);
            this.Controls.Add(this.btnKapat);
            this.Controls.Add(this.btnYeniOdemeEkle);
            this.Controls.Add(this.btnYeniIsEkle);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblGuncelBakiye);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblAdSoyad);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmMusteriHesap";
            this.Text = "Müşteri Hesap Bilgeri";
            this.Load += new System.EventHandler(this.FrmMusteriHesap_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvIsler)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOdemeler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMusterilerListesi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblAdSoyad;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblGuncelBakiye;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvIsler;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvOdemeler;
        private System.Windows.Forms.Button btnYeniIsEkle;
        private System.Windows.Forms.Button btnYeniOdemeEkle;
        private System.Windows.Forms.Button btnKapat;
        private System.Windows.Forms.TextBox txtArama;
        private System.Windows.Forms.DataGridView dgvMusterilerListesi;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTelefon;
        private System.Windows.Forms.Button btnPdfAktar;
    }
}