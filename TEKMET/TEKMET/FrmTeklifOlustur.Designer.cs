namespace TEKMET
{
    partial class FrmTeklifOlustur
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
            this.cmbMusteriler = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpTarih = new System.Windows.Forms.DateTimePicker();
            this.dgvTeklifKalemleri = new System.Windows.Forms.DataGridView();
            this.btnSatirEkle = new System.Windows.Forms.Button();
            this.btnSatirSil = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lblAraToplam = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblKdv = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblGenelToplam = new System.Windows.Forms.Label();
            this.btnPdfOlustur = new System.Windows.Forms.Button();
            this.btnTpl = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNotlar = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnTeklifiSil = new System.Windows.Forms.Button();
            this.btnTeklifleriListele = new System.Windows.Forms.Button();
            this.btnTeklifiYukle = new System.Windows.Forms.Button();
            this.lstKayitliTeklifler = new System.Windows.Forms.ListBox();
            this.btnYnTklf = new System.Windows.Forms.Button();
            this.btnKaydet = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTeklifAdi = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTeklifKalemleri)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Müşteri:";
            // 
            // cmbMusteriler
            // 
            this.cmbMusteriler.FormattingEnabled = true;
            this.cmbMusteriler.Location = new System.Drawing.Point(63, 9);
            this.cmbMusteriler.Name = "cmbMusteriler";
            this.cmbMusteriler.Size = new System.Drawing.Size(121, 21);
            this.cmbMusteriler.TabIndex = 1;
            this.cmbMusteriler.SelectedIndexChanged += new System.EventHandler(this.cmbMusteriler_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(576, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tarih:";
            // 
            // dtpTarih
            // 
            this.dtpTarih.Location = new System.Drawing.Point(616, 9);
            this.dtpTarih.Name = "dtpTarih";
            this.dtpTarih.Size = new System.Drawing.Size(200, 20);
            this.dtpTarih.TabIndex = 3;
            // 
            // dgvTeklifKalemleri
            // 
            this.dgvTeklifKalemleri.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTeklifKalemleri.Location = new System.Drawing.Point(25, 236);
            this.dgvTeklifKalemleri.Name = "dgvTeklifKalemleri";
            this.dgvTeklifKalemleri.Size = new System.Drawing.Size(773, 240);
            this.dgvTeklifKalemleri.TabIndex = 4;
            this.dgvTeklifKalemleri.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTeklifKalemleri_CellContentClick);
            this.dgvTeklifKalemleri.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTeklifKalemleri_CellValueChanged);
            this.dgvTeklifKalemleri.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvTeklifKalemleri_UserDeletedRow);
            // 
            // btnSatirEkle
            // 
            this.btnSatirEkle.Location = new System.Drawing.Point(28, 482);
            this.btnSatirEkle.Name = "btnSatirEkle";
            this.btnSatirEkle.Size = new System.Drawing.Size(31, 23);
            this.btnSatirEkle.TabIndex = 5;
            this.btnSatirEkle.Text = "(+)";
            this.btnSatirEkle.UseVisualStyleBackColor = true;
            this.btnSatirEkle.Click += new System.EventHandler(this.btnSatirEkle_Click_1);
            // 
            // btnSatirSil
            // 
            this.btnSatirSil.Location = new System.Drawing.Point(65, 482);
            this.btnSatirSil.Name = "btnSatirSil";
            this.btnSatirSil.Size = new System.Drawing.Size(31, 23);
            this.btnSatirSil.TabIndex = 6;
            this.btnSatirSil.Text = "(-)";
            this.btnSatirSil.UseVisualStyleBackColor = true;
            this.btnSatirSil.Click += new System.EventHandler(this.btnSatirSil_Click_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 508);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "ARA TOPLAM:";
            // 
            // lblAraToplam
            // 
            this.lblAraToplam.AutoSize = true;
            this.lblAraToplam.Location = new System.Drawing.Point(110, 508);
            this.lblAraToplam.Name = "lblAraToplam";
            this.lblAraToplam.Size = new System.Drawing.Size(44, 13);
            this.lblAraToplam.TabIndex = 8;
            this.lblAraToplam.Text = "0,00 TL";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(43, 531);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "KDV (%20):";
            // 
            // lblKdv
            // 
            this.lblKdv.AutoSize = true;
            this.lblKdv.Location = new System.Drawing.Point(110, 531);
            this.lblKdv.Name = "lblKdv";
            this.lblKdv.Size = new System.Drawing.Size(44, 13);
            this.lblKdv.TabIndex = 10;
            this.lblKdv.Text = "0,00 TL";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 553);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "GENEL TOPLAM:";
            // 
            // lblGenelToplam
            // 
            this.lblGenelToplam.AutoSize = true;
            this.lblGenelToplam.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblGenelToplam.Location = new System.Drawing.Point(104, 549);
            this.lblGenelToplam.Name = "lblGenelToplam";
            this.lblGenelToplam.Size = new System.Drawing.Size(56, 19);
            this.lblGenelToplam.TabIndex = 12;
            this.lblGenelToplam.Text = "0,00 TL";
            // 
            // btnPdfOlustur
            // 
            this.btnPdfOlustur.Location = new System.Drawing.Point(25, 603);
            this.btnPdfOlustur.Name = "btnPdfOlustur";
            this.btnPdfOlustur.Size = new System.Drawing.Size(169, 23);
            this.btnPdfOlustur.TabIndex = 13;
            this.btnPdfOlustur.Text = "Teklifi PDF Olarak Kaydet";
            this.btnPdfOlustur.UseVisualStyleBackColor = true;
            this.btnPdfOlustur.Click += new System.EventHandler(this.btnPdfOlustur_Click);
            // 
            // btnTpl
            // 
            this.btnTpl.Location = new System.Drawing.Point(25, 574);
            this.btnTpl.Name = "btnTpl";
            this.btnTpl.Size = new System.Drawing.Size(75, 23);
            this.btnTpl.TabIndex = 14;
            this.btnTpl.Text = "Topla";
            this.btnTpl.UseVisualStyleBackColor = true;
            this.btnTpl.Click += new System.EventHandler(this.btnTpl_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(307, 491);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Teklif Notları:";
            // 
            // txtNotlar
            // 
            this.txtNotlar.Location = new System.Drawing.Point(383, 491);
            this.txtNotlar.Multiline = true;
            this.txtNotlar.Name = "txtNotlar";
            this.txtNotlar.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotlar.Size = new System.Drawing.Size(418, 119);
            this.txtNotlar.TabIndex = 16;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnTeklifiSil);
            this.groupBox1.Controls.Add(this.btnTeklifleriListele);
            this.groupBox1.Controls.Add(this.btnTeklifiYukle);
            this.groupBox1.Controls.Add(this.lstKayitliTeklifler);
            this.groupBox1.Location = new System.Drawing.Point(15, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(304, 167);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Müşterinin Kayıtlı Teklifleri";
            // 
            // btnTeklifiSil
            // 
            this.btnTeklifiSil.Location = new System.Drawing.Point(223, 60);
            this.btnTeklifiSil.Name = "btnTeklifiSil";
            this.btnTeklifiSil.Size = new System.Drawing.Size(75, 35);
            this.btnTeklifiSil.TabIndex = 23;
            this.btnTeklifiSil.Text = "Teklifleri Sil";
            this.btnTeklifiSil.UseVisualStyleBackColor = true;
            this.btnTeklifiSil.Click += new System.EventHandler(this.btnTeklifiSil_Click);
            // 
            // btnTeklifleriListele
            // 
            this.btnTeklifleriListele.Location = new System.Drawing.Point(223, 19);
            this.btnTeklifleriListele.Name = "btnTeklifleriListele";
            this.btnTeklifleriListele.Size = new System.Drawing.Size(75, 35);
            this.btnTeklifleriListele.TabIndex = 22;
            this.btnTeklifleriListele.Text = "Teklifleri Listele";
            this.btnTeklifleriListele.UseVisualStyleBackColor = true;
            this.btnTeklifleriListele.Click += new System.EventHandler(this.btnTeklifleriListele_Click);
            // 
            // btnTeklifiYukle
            // 
            this.btnTeklifiYukle.Location = new System.Drawing.Point(224, 101);
            this.btnTeklifiYukle.Name = "btnTeklifiYukle";
            this.btnTeklifiYukle.Size = new System.Drawing.Size(74, 39);
            this.btnTeklifiYukle.TabIndex = 18;
            this.btnTeklifiYukle.Text = "Seçili Teklifi Yükle";
            this.btnTeklifiYukle.UseVisualStyleBackColor = true;
            this.btnTeklifiYukle.Click += new System.EventHandler(this.btnTeklifiYukle_Click);
            // 
            // lstKayitliTeklifler
            // 
            this.lstKayitliTeklifler.FormattingEnabled = true;
            this.lstKayitliTeklifler.Location = new System.Drawing.Point(10, 19);
            this.lstKayitliTeklifler.Name = "lstKayitliTeklifler";
            this.lstKayitliTeklifler.Size = new System.Drawing.Size(208, 134);
            this.lstKayitliTeklifler.TabIndex = 18;
            // 
            // btnYnTklf
            // 
            this.btnYnTklf.Location = new System.Drawing.Point(301, 606);
            this.btnYnTklf.Name = "btnYnTklf";
            this.btnYnTklf.Size = new System.Drawing.Size(75, 23);
            this.btnYnTklf.TabIndex = 18;
            this.btnYnTklf.Text = "Yeni Teklif";
            this.btnYnTklf.UseVisualStyleBackColor = true;
            this.btnYnTklf.Click += new System.EventHandler(this.btnYnTklf_Click);
            // 
            // btnKaydet
            // 
            this.btnKaydet.Location = new System.Drawing.Point(200, 603);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(75, 23);
            this.btnKaydet.TabIndex = 19;
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.UseVisualStyleBackColor = true;
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 206);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Teklif Adı / Konu:";
            // 
            // txtTeklifAdi
            // 
            this.txtTeklifAdi.Location = new System.Drawing.Point(108, 203);
            this.txtTeklifAdi.Name = "txtTeklifAdi";
            this.txtTeklifAdi.Size = new System.Drawing.Size(100, 20);
            this.txtTeklifAdi.TabIndex = 21;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(360, 36);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(441, 167);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Enes’ten Notlar";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.label8.Location = new System.Drawing.Point(7, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(405, 19);
            this.label8.TabIndex = 0;
            this.label8.Text = "Teklif yazması bittikten sonra ‘Topla’ butonuna basın.";
            // 
            // FrmTeklifOlustur
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 644);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.txtTeklifAdi);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnKaydet);
            this.Controls.Add(this.btnYnTklf);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtNotlar);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnTpl);
            this.Controls.Add(this.btnPdfOlustur);
            this.Controls.Add(this.lblGenelToplam);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblKdv);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblAraToplam);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSatirSil);
            this.Controls.Add(this.btnSatirEkle);
            this.Controls.Add(this.dgvTeklifKalemleri);
            this.Controls.Add(this.dtpTarih);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbMusteriler);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmTeklifOlustur";
            this.Text = "Teklif";
            this.Load += new System.EventHandler(this.FrmTeklifOlustur_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTeklifKalemleri)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbMusteriler;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpTarih;
        private System.Windows.Forms.DataGridView dgvTeklifKalemleri;
        private System.Windows.Forms.Button btnSatirEkle;
        private System.Windows.Forms.Button btnSatirSil;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblAraToplam;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblKdv;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblGenelToplam;
        private System.Windows.Forms.Button btnPdfOlustur;
        private System.Windows.Forms.Button btnTpl;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNotlar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnTeklifiYukle;
        private System.Windows.Forms.ListBox lstKayitliTeklifler;
        private System.Windows.Forms.Button btnYnTklf;
        private System.Windows.Forms.Button btnKaydet;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTeklifAdi;
        private System.Windows.Forms.Button btnTeklifleriListele;
        private System.Windows.Forms.Button btnTeklifiSil;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label8;
    }
}