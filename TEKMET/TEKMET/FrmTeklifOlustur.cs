using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TEKMET
{
    public partial class FrmTeklifOlustur : Form
    {
        private DataTable dtTeklif;
        private int? aktifTeklifID = null;

        public FrmTeklifOlustur()
        {
            InitializeComponent();
        }

        private void FrmTeklifOlustur_Load(object sender, EventArgs e)
        {
            MusterileriYukle();
            TeklifTablosunuHazirla();
            // YENİ EKLENDİ: Form ilk yüklendiğinde metin kutusunu göster
            txtMusteriAdi.Visible = true;
        }

        private void MusterileriYukle()
        {
            cmbMusteriler.SelectedIndexChanged -= cmbMusteriler_SelectedIndexChanged;

            using (var baglanti = new SQLiteConnection(Veritabani.BaglantiCumlesi))
            {
                string sorgu = "SELECT MusteriID, AdSoyad FROM Musteriler ORDER BY AdSoyad";
                using (var da = new SQLiteDataAdapter(sorgu, baglanti))
                {
                    var dt = new DataTable();
                    da.Fill(dt);

                    cmbMusteriler.DataSource = dt;
                    cmbMusteriler.DisplayMember = "AdSoyad";
                    cmbMusteriler.ValueMember = "MusteriID";
                }
            }

            cmbMusteriler.SelectedIndex = -1;
            cmbMusteriler.SelectedIndexChanged += cmbMusteriler_SelectedIndexChanged;
        }

        // YENİ EKLENDİ: Müşteri adını doğru kontrolden almak için yardımcı bir metod.
        private string GetGecerliMusteriAdi()
        {
            if (cmbMusteriler.SelectedIndex != -1)
            {
                // ComboBox'tan bir müşteri seçiliyse, onun adını döndür.
                return cmbMusteriler.Text;
            }
            else
            {
                // Seçim yoksa, TextBox'taki adı döndür (başındaki/sonundaki boşlukları temizle).
                return txtMusteriAdi.Text.Trim();
            }
        }

        private void TeklifTablosunuHazirla()
        {
            dtTeklif = new DataTable();
            dtTeklif.Columns.Add("Aciklama", typeof(string));
            dtTeklif.Columns.Add("Miktar", typeof(decimal));
            dtTeklif.Columns.Add("Birim", typeof(string));
            dtTeklif.Columns.Add("BirimFiyat", typeof(decimal));
            dtTeklif.Columns.Add("ToplamFiyat", typeof(decimal), "Miktar * BirimFiyat");

            dgvTeklifKalemleri.DataSource = dtTeklif;
            dgvTeklifKalemleri.AutoGenerateColumns = false;
            dgvTeklifKalemleri.Columns.Clear();

            dgvTeklifKalemleri.Columns.Add(new DataGridViewTextBoxColumn { Name = "Aciklama", HeaderText = "AÇIKLAMA", DataPropertyName = "Aciklama", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvTeklifKalemleri.Columns.Add(new DataGridViewTextBoxColumn { Name = "Miktar", HeaderText = "MİKTAR", DataPropertyName = "Miktar", Width = 75 });
            var birimKolonu = new DataGridViewComboBoxColumn { Name = "Birim", HeaderText = "BİRİM", DataPropertyName = "Birim", Width = 75 };
            birimKolonu.Items.AddRange("Adet", "m²", "mt");
            dgvTeklifKalemleri.Columns.Add(birimKolonu);
            dgvTeklifKalemleri.Columns.Add(new DataGridViewTextBoxColumn { Name = "BirimFiyat", HeaderText = "BİRİM FİYAT", DataPropertyName = "BirimFiyat", Width = 100, DefaultCellStyle = { Format = "c2" } });
            dgvTeklifKalemleri.Columns.Add(new DataGridViewTextBoxColumn { Name = "ToplamFiyat", HeaderText = "TOPLAM FİYAT", DataPropertyName = "ToplamFiyat", Width = 120, ReadOnly = true, DefaultCellStyle = { Format = "c2" } });
        }

        private void btnSatirEkle_Click(object sender, EventArgs e)
        {
            dtTeklif.Rows.Add(dtTeklif.NewRow());
        }

        private void btnSatirSil_Click(object sender, EventArgs e)
        {
            if (dgvTeklifKalemleri.CurrentRow != null)
            {
                dgvTeklifKalemleri.Rows.Remove(dgvTeklifKalemleri.CurrentRow);
            }
            else
            {
                MessageBox.Show("Lütfen silmek için bir satır seçin.", "Uyarı");
            }
        }

        private void btnSatirEkle_Click_1(object sender, EventArgs e)
        {
            dtTeklif.Rows.Add(dtTeklif.NewRow());
            HesaplaGenelToplamlari();
        }

        private void btnSatirSil_Click_1(object sender, EventArgs e)
        {
            if (dgvTeklifKalemleri.CurrentRow != null)
            {
                dgvTeklifKalemleri.Rows.Remove(dgvTeklifKalemleri.CurrentRow);
                HesaplaGenelToplamlari();
            }
            else
            {
                MessageBox.Show("Lütfen silmek için bir satır seçin.", "Uyarı");
            }
        }

        private void dgvTeklifKalemleri_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            HesaplaGenelToplamlari();
        }

        private void dgvTeklifKalemleri_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            HesaplaGenelToplamlari();
        }
        private void HesaplaGenelToplamlari()
        {
            decimal araToplam = 0;
            foreach (DataGridViewRow row in dgvTeklifKalemleri.Rows)
            {
                if (row.Cells["ToplamFiyat"].Value != null && row.Cells["ToplamFiyat"].Value != DBNull.Value)
                {
                    araToplam += Convert.ToDecimal(row.Cells["ToplamFiyat"].Value);
                }
            }

            decimal kdv = araToplam * 0.20m;
            decimal genelToplam = araToplam + kdv;

            lblAraToplam.Text = araToplam.ToString("c2");
            lblKdv.Text = kdv.ToString("c2");
            lblGenelToplam.Text = genelToplam.ToString("c2");
        }

        private void btnPdfOlustur_Click(object sender, EventArgs e)
        {
            // DEĞİŞTİRİLDİ: Kontrol artık yeni yardımcı metot üzerinden yapılıyor.
            string musteriAdi = GetGecerliMusteriAdi();
            if (string.IsNullOrWhiteSpace(musteriAdi))
            {
                MessageBox.Show("Lütfen bir müşteri seçin veya yeni bir müşteri adı girin.", "Uyarı");
                return;
            }

            if (dgvTeklifKalemleri.Rows.Count == 0)
            {
                MessageBox.Show("Lütfen en az bir teklif kalemi ekleyin.", "Uyarı");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PDF Dosyası (*.pdf)|*.pdf";
            // DEĞİŞTİRİLDİ: Dosya adı da yeni metottan gelen isme göre oluşturuluyor.
            sfd.FileName = $"{musteriAdi} - Teklif.pdf";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // DEĞİŞTİRİLDİ: Müşteri adı PDF oluşturma metoduna parametre olarak gönderiliyor.
                    TeklifPdfOlustur(sfd.FileName, musteriAdi);

                    DialogResult cevap = MessageBox.Show("PDF teklifi başarıyla oluşturuldu. Dosyayı şimdi açmak ister misiniz?", "Başarılı", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (cevap == DialogResult.Yes)
                    {
                        Process.Start(sfd.FileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("PDF oluşturulurken bir hata oluştu: " + ex.Message, "Hata");
                }
            }
        }

        // DEĞİŞTİRİLDİ: Metodun imzası, müşteri adını parametre olarak alacak şekilde güncellendi.
        private void TeklifPdfOlustur(string dosyaYolu, string musteriAdi)
        {
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont fontSirket = new XFont("Arial", 22, XFontStyle.Bold);
            XFont fontBaslik = new XFont("Arial", 12, XFontStyle.Bold);
            XFont fontNormal = new XFont("Arial", 10, XFontStyle.Regular);
            XFont fontGenelToplam = new XFont("Arial", 12, XFontStyle.Bold);

            int y = 40;

            try
            {
                string logoYolu = "logo.png";
                XImage logo = XImage.FromFile(logoYolu);
                double logoGenislik = 150;
                double logoYukseklik = (logoGenislik / logo.PixelWidth) * logo.PixelHeight;
                double logoX = (page.Width - logoGenislik) / 2;
                gfx.DrawImage(logo, logoX, y, logoGenislik, logoYukseklik);
                y += (int)logoYukseklik + 10;
            }
            catch (Exception)
            {
                gfx.DrawString("TEKMET YAPI", fontSirket, XBrushes.Black, new XRect(0, y, page.Width, 0), XStringFormats.TopCenter);
                y += 30;
            }

            gfx.DrawString(dtpTarih.Value.ToString("dd.MM.yyyy"), fontNormal, XBrushes.Black, new XRect(0, 50, page.Width - 40, 0), XStringFormats.TopRight);
            y += 20;
            gfx.DrawLine(XPens.DarkGray, 40, y, page.Width - 40, y);
            y += 20;
            gfx.DrawString("Sayın,", fontNormal, XBrushes.Black, 40, y);
            y += 15;
            // DEĞİŞTİRİLDİ: Müşteri adı artık parametreden geliyor.
            gfx.DrawString(musteriAdi, fontBaslik, XBrushes.Black, 40, y);
            y += 35;

            double aciklamaX = 50;
            double miktarX = 290;
            double birimFiyatX = 370;
            double toplamFiyatX = 460;
            double toplamAlanGenislik = 95;

            gfx.DrawRectangle(XBrushes.LightGray, 40, y, page.Width - 80, 20);
            gfx.DrawString("AÇIKLAMA", fontBaslik, XBrushes.Black, aciklamaX, y + 15);
            gfx.DrawString("MİKTAR", fontBaslik, XBrushes.Black, miktarX, y + 15);
            gfx.DrawString("BİRİM FİYAT", fontBaslik, XBrushes.Black, birimFiyatX, y + 15);
            gfx.DrawString("TOPLAM FİYAT", fontBaslik, XBrushes.Black, toplamFiyatX, y + 15);
            y += 20;

            XStringFormat formatSag = new XStringFormat { Alignment = XStringAlignment.Far };

            foreach (DataGridViewRow row in dgvTeklifKalemleri.Rows)
            {
                if (row.IsNewRow || row.Cells["Aciklama"].Value == null && row.Cells["Miktar"].Value == null) continue;

                gfx.DrawLine(XPens.Gray, 40, y, page.Width - 40, y);
                y += 5;

                gfx.DrawString(row.Cells["Aciklama"].Value?.ToString() ?? "", fontNormal, XBrushes.Black, aciklamaX, y + 10);

                string miktar = row.Cells["Miktar"].Value?.ToString() ?? "0";
                string birim = row.Cells["Birim"].Value?.ToString() ?? "";
                gfx.DrawString($"{miktar} {birim}", fontNormal, XBrushes.Black, miktarX, y + 10);

                decimal birimFiyat = row.Cells["BirimFiyat"].Value != DBNull.Value ? Convert.ToDecimal(row.Cells["BirimFiyat"].Value) : 0;
                gfx.DrawString(birimFiyat.ToString("c2"), fontNormal, XBrushes.Black, new XRect(birimFiyatX - 20, y + 10, 80, 0), formatSag);

                decimal toplamFiyat = row.Cells["ToplamFiyat"].Value != DBNull.Value ? Convert.ToDecimal(row.Cells["ToplamFiyat"].Value) : 0;
                gfx.DrawString(toplamFiyat.ToString("c2"), fontNormal, XBrushes.Black, new XRect(toplamFiyatX - 20, y + 10, toplamAlanGenislik, 0), formatSag);

                y += 20;
            }
            gfx.DrawLine(XPens.Gray, 40, y, page.Width - 40, y);

            y += 20;
            double toplamEtiketX = 340;
            double toplamDegerX = 460;

            gfx.DrawString("ARA TOPLAM:", fontBaslik, XBrushes.Black, new XRect(toplamEtiketX, y, 110, 0), formatSag);
            gfx.DrawString(lblAraToplam.Text, fontNormal, XBrushes.Black, new XRect(toplamDegerX, y, 95, 0), formatSag);
            y += 20;
            gfx.DrawString("KDV (%20):", fontBaslik, XBrushes.Black, new XRect(toplamEtiketX, y, 110, 0), formatSag);
            gfx.DrawString(lblKdv.Text, fontNormal, XBrushes.Black, new XRect(toplamDegerX, y, 95, 0), formatSag);
            y += 25;

            gfx.DrawRectangle(XBrushes.LightGray, toplamEtiketX, y, page.Width - 40 - toplamEtiketX, 25);

            gfx.DrawString("GENEL TOPLAM:", fontGenelToplam, XBrushes.Black, new XRect(toplamEtiketX, y + 5, 110, 0), formatSag);
            gfx.DrawString(lblGenelToplam.Text, fontGenelToplam, XBrushes.Black, new XRect(toplamDegerX, y + 5, 95, 0), formatSag);

            y += 40;

            if (!string.IsNullOrWhiteSpace(txtNotlar.Text))
            {
                gfx.DrawString("Teklif Notları:", fontBaslik, XBrushes.Black, 40, y);
                y += 20;

                XTextFormatter tf = new XTextFormatter(gfx);
                XRect rect = new XRect(40, y, page.Width - 80, page.Height - y - 40);
                tf.DrawString(txtNotlar.Text, fontNormal, XBrushes.Black, rect, XStringFormats.TopLeft);
            }

            document.Save(dosyaYolu);
        }

        private void btnTpl_Click(object sender, EventArgs e)
        {
            HesaplaGenelToplamlari();
        }

        // DEĞİŞTİRİLDİ: ComboBox seçimi değiştiğinde TextBox'ın görünürlüğünü ayarlar.
        private void cmbMusteriler_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMusteriler.SelectedValue != null && cmbMusteriler.SelectedValue != DBNull.Value)
            {
                // Bir müşteri seçildi:
                txtMusteriAdi.Visible = false; // Metin kutusunu gizle
                txtMusteriAdi.Clear(); // İçini temizle
                int seciliMusteriID = Convert.ToInt32(cmbMusteriler.SelectedValue);
                KayitliTeklifleriYukle(seciliMusteriID);
            }
            else
            {
                // Seçim kaldırıldı veya yok:
                txtMusteriAdi.Visible = true; // Metin kutusunu göster
                lstKayitliTeklifler.DataSource = null; // Teklif listesini temizle
            }
        }
        private void KayitliTeklifleriYukle(int musteriID)
        {
            using (var baglanti = new SQLiteConnection(Veritabani.BaglantiCumlesi))
            {
                string sorgu = "SELECT TeklifID, TeklifAdi, TeklifTarihi, GenelToplam FROM Teklifler WHERE MusteriID = @musteriID ORDER BY TeklifTarihi DESC";
                using (var da = new SQLiteDataAdapter(sorgu, baglanti))
                {
                    da.SelectCommand.Parameters.AddWithValue("@musteriID", musteriID);
                    var dt = new DataTable();
                    da.Fill(dt);

                    dt.Columns.Add("GorunenMetin", typeof(string));
                    foreach (DataRow row in dt.Rows)
                    {
                        DateTime tarih = DateTime.Parse(row["TeklifTarihi"].ToString());
                        decimal toplam = Convert.ToDecimal(row["GenelToplam"]);
                        string teklifAdi = row["TeklifAdi"].ToString();
                        row["GorunenMetin"] = $"{tarih:dd.MM.yyyy} - {teklifAdi} - {toplam:c2}";
                    }

                    lstKayitliTeklifler.DataSource = dt;
                    lstKayitliTeklifler.DisplayMember = "GorunenMetin";
                    lstKayitliTeklifler.ValueMember = "TeklifID";
                }
            }
        }

        private void btnTeklifiYukle_Click(object sender, EventArgs e)
        {
            if (lstKayitliTeklifler.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen yüklemek için bir teklif seçin.", "Uyarı");
                return;
            }

            int seciliTeklifID = Convert.ToInt32(lstKayitliTeklifler.SelectedValue);
            TeklifiFormaYukle(seciliTeklifID);
        }
        private void TeklifiFormaYukle(int teklifID)
        {
            this.aktifTeklifID = teklifID;

            try
            {
                using (var baglanti = new SQLiteConnection(Veritabani.BaglantiCumlesi))
                {
                    baglanti.Open();

                    string teklifSorgu = "SELECT * FROM Teklifler WHERE TeklifID = @id";
                    using (var komut = new SQLiteCommand(teklifSorgu, baglanti))
                    {
                        komut.Parameters.AddWithValue("@id", teklifID);
                        using (var okuyucu = komut.ExecuteReader())
                        {
                            if (okuyucu.Read())
                            {
                                dtpTarih.Value = DateTime.Parse(okuyucu["TeklifTarihi"].ToString());
                                txtNotlar.Text = okuyucu["Notlar"].ToString();
                                txtTeklifAdi.Text = okuyucu["TeklifAdi"].ToString();
                            }
                        }
                    }

                    dtTeklif.Rows.Clear();
                    string kalemSorgu = "SELECT Aciklama, Miktar, Birim, BirimFiyat FROM TeklifKalemleri WHERE TeklifID = @id";
                    using (var da = new SQLiteDataAdapter(kalemSorgu, baglanti))
                    {
                        da.SelectCommand.Parameters.AddWithValue("@id", teklifID);
                        da.Fill(dtTeklif);
                    }
                }
                HesaplaGenelToplamlari();
                MessageBox.Show("Teklif başarıyla yüklendi. Şimdi üzerinde değişiklik yapabilirsiniz.", "Bilgi");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Teklif yüklenirken bir hata oluştu: " + ex.Message, "Hata");
            }
        }

        // DEĞİŞTİRİLDİ: Temizleme işlemine txtMusteriAdi de eklendi.
        private void Temizle()
        {
            aktifTeklifID = null;
            dtTeklif.Rows.Clear();
            cmbMusteriler.SelectedIndex = -1; // Bu, SelectedIndexChanged olayını tetikleyerek txtMusteriAdi'yi zaten gösterir.
            txtMusteriAdi.Clear(); // Yine de içini temizlemek iyidir.
            lstKayitliTeklifler.DataSource = null;
            txtNotlar.Clear();
            txtTeklifAdi.Clear();
            dtpTarih.Value = DateTime.Now;
            HesaplaGenelToplamlari();
        }

        private void btnYnTklf_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        // YENİ EKLENDİ: Veritabanında olmayan bir müşteri için kayıt oluşturan ve ID'sini döndüren metot.
        private int MusteriOlustur(string adSoyad, SQLiteConnection baglanti)
        {
            string sorgu = "INSERT INTO Musteriler (AdSoyad) VALUES (@adSoyad)";
            using (var komut = new SQLiteCommand(sorgu, baglanti))
            {
                komut.Parameters.AddWithValue("@adSoyad", adSoyad);
                komut.ExecuteNonQuery();
                return (int)baglanti.LastInsertRowId;
            }
        }


        private void btnKaydet_Click(object sender, EventArgs e)
        {
            // --- 1. Kontroller ---
            // DEĞİŞTİRİLDİ: Müşteri adı kontrolü yeni metotla yapılıyor.
            string musteriAdi = GetGecerliMusteriAdi();
            if (string.IsNullOrWhiteSpace(musteriAdi))
            {
                MessageBox.Show("Lütfen bir müşteri seçin veya yeni bir müşteri adı girin.", "Uyarı");
                return;
            }
            if (dgvTeklifKalemleri.Rows.Count == 0 || (dgvTeklifKalemleri.Rows.Count == 1 && dgvTeklifKalemleri.Rows[0].IsNewRow))
            {
                MessageBox.Show("Lütfen en az bir teklif kalemi ekleyin.", "Uyarı");
                return;
            }

            // --- 2. Verileri Toplama ---
            string teklifAdi = txtTeklifAdi.Text;
            string teklifTarihi = dtpTarih.Value.ToString("yyyy-MM-dd");
            decimal araToplam = decimal.Parse(lblAraToplam.Text, System.Globalization.NumberStyles.Currency);
            decimal kdv = decimal.Parse(lblKdv.Text, System.Globalization.NumberStyles.Currency);
            decimal genelToplam = decimal.Parse(lblGenelToplam.Text, System.Globalization.NumberStyles.Currency);
            string notlar = txtNotlar.Text;

            try
            {
                using (var baglanti = new SQLiteConnection(Veritabani.BaglantiCumlesi))
                {
                    baglanti.Open();

                    // DEĞİŞTİRİLDİ: Müşteri ID'sini belirleme mantığı eklendi.
                    int musteriID;
                    if (cmbMusteriler.SelectedIndex != -1)
                    {
                        // Mevcut müşteri seçili.
                        musteriID = Convert.ToInt32(cmbMusteriler.SelectedValue);
                    }
                    else
                    {
                        // Yeni müşteri, veritabanına ekle ve ID'sini al.
                        musteriID = MusteriOlustur(musteriAdi, baglanti);
                    }

                    // --- 3. Akıllı Mantık: Yeni Kayıt mı? Güncelleme mi? ---
                    if (aktifTeklifID == null) // YENİ KAYIT
                    {
                        string teklifSorgu = "INSERT INTO Teklifler (MusteriID, TeklifAdi, TeklifTarihi, AraToplam, KDV, GenelToplam, Notlar) VALUES (@musteriID, @teklifAdi, @tarih, @araToplam, @kdv, @genelToplam, @notlar)";
                        long yeniTeklifID;
                        using (var komut = new SQLiteCommand(teklifSorgu, baglanti))
                        {
                            komut.Parameters.AddWithValue("@musteriID", musteriID);
                            komut.Parameters.AddWithValue("@teklifAdi", teklifAdi);
                            komut.Parameters.AddWithValue("@tarih", teklifTarihi);
                            komut.Parameters.AddWithValue("@araToplam", araToplam);
                            komut.Parameters.AddWithValue("@kdv", kdv);
                            komut.Parameters.AddWithValue("@genelToplam", genelToplam);
                            komut.Parameters.AddWithValue("@notlar", notlar);
                            komut.ExecuteNonQuery();
                            yeniTeklifID = baglanti.LastInsertRowId;
                        }

                        foreach (DataRow row in dtTeklif.Rows)
                        {
                            if (row.RowState == DataRowState.Deleted || row["Aciklama"] == DBNull.Value) continue;
                            string kalemSorgu = "INSERT INTO TeklifKalemleri (TeklifID, Aciklama, Miktar, Birim, BirimFiyat, ToplamFiyat) VALUES (@teklifID, @aciklama, @miktar, @birim, @birimFiyat, @toplamFiyat)";
                            using (var komut = new SQLiteCommand(kalemSorgu, baglanti))
                            {
                                komut.Parameters.AddWithValue("@teklifID", yeniTeklifID);
                                komut.Parameters.AddWithValue("@aciklama", row["Aciklama"]);
                                komut.Parameters.AddWithValue("@miktar", row["Miktar"]);
                                komut.Parameters.AddWithValue("@birim", row["Birim"]);
                                komut.Parameters.AddWithValue("@birimFiyat", row["BirimFiyat"]);
                                komut.Parameters.AddWithValue("@toplamFiyat", row["ToplamFiyat"]);
                                komut.ExecuteNonQuery();
                            }
                        }
                    }
                    else // GÜNCELLEME
                    {
                        string teklifSorgu = "UPDATE Teklifler SET MusteriID = @musteriID, TeklifAdi = @teklifAdi, TeklifTarihi = @tarih, AraToplam = @araToplam, KDV = @kdv, GenelToplam = @genelToplam, Notlar = @notlar WHERE TeklifID = @id";
                        using (var komut = new SQLiteCommand(teklifSorgu, baglanti))
                        {
                            komut.Parameters.AddWithValue("@id", aktifTeklifID.Value);
                            komut.Parameters.AddWithValue("@musteriID", musteriID);
                            komut.Parameters.AddWithValue("@teklifAdi", teklifAdi);
                            komut.Parameters.AddWithValue("@tarih", teklifTarihi);
                            komut.Parameters.AddWithValue("@araToplam", araToplam);
                            komut.Parameters.AddWithValue("@kdv", kdv);
                            komut.Parameters.AddWithValue("@genelToplam", genelToplam);
                            komut.Parameters.AddWithValue("@notlar", notlar);
                            komut.ExecuteNonQuery();
                        }

                        string silmeSorgusu = "DELETE FROM TeklifKalemleri WHERE TeklifID = @id";
                        using (var komut = new SQLiteCommand(silmeSorgusu, baglanti))
                        {
                            komut.Parameters.AddWithValue("@id", aktifTeklifID.Value);
                            komut.ExecuteNonQuery();
                        }

                        foreach (DataRow row in dtTeklif.Rows)
                        {
                            if (row.RowState == DataRowState.Deleted || row["Aciklama"] == DBNull.Value) continue;
                            string kalemSorgu = "INSERT INTO TeklifKalemleri (TeklifID, Aciklama, Miktar, Birim, BirimFiyat, ToplamFiyat) VALUES (@teklifID, @aciklama, @miktar, @birim, @birimFiyat, @toplamFiyat)";
                            using (var komut = new SQLiteCommand(kalemSorgu, baglanti))
                            {
                                komut.Parameters.AddWithValue("@teklifID", aktifTeklifID.Value);
                                komut.Parameters.AddWithValue("@aciklama", row["Aciklama"]);
                                komut.Parameters.AddWithValue("@miktar", row["Miktar"]);
                                komut.Parameters.AddWithValue("@birim", row["Birim"]);
                                komut.Parameters.AddWithValue("@birimFiyat", row["BirimFiyat"]);
                                komut.Parameters.AddWithValue("@toplamFiyat", row["ToplamFiyat"]);
                                komut.ExecuteNonQuery();
                            }
                        }
                    }
                }
                MessageBox.Show("Teklif başarıyla kaydedildi!", "Başarılı");

                int sonKullanilanMusteriID = (cmbMusteriler.SelectedIndex != -1) ? Convert.ToInt32(cmbMusteriler.SelectedValue) : 0;

                Temizle();
                MusterileriYukle(); // Yeni müşteri eklendiyse listeyi yenilemek için.

                // Kaydedilen müşterinin tekliflerini tekrar yükle
                if (sonKullanilanMusteriID > 0)
                {
                    cmbMusteriler.SelectedValue = sonKullanilanMusteriID;
                    KayitliTeklifleriYukle(sonKullanilanMusteriID);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Teklif kaydedilirken bir hata oluştu: " + ex.Message, "Hata");
            }
        }

        private void dgvTeklifKalemleri_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnTeklifleriListele_Click(object sender, EventArgs e)
        {
            if (cmbMusteriler.SelectedValue != null && cmbMusteriler.SelectedValue != DBNull.Value)
            {
                int seciliMusteriID = Convert.ToInt32(cmbMusteriler.SelectedValue);
                KayitliTeklifleriYukle(seciliMusteriID);
            }
            else
            {
                MessageBox.Show("Lütfen önce bir müşteri seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnTeklifiSil_Click(object sender, EventArgs e)
        {
            if (lstKayitliTeklifler.SelectedValue == null || lstKayitliTeklifler.SelectedValue is DBNull)
            {
                MessageBox.Show("Lütfen silmek için bir teklif seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int seciliTeklifID = Convert.ToInt32(lstKayitliTeklifler.SelectedValue);
            string seciliTeklifMetni = lstKayitliTeklifler.Text;

            DialogResult cevap = MessageBox.Show($"Aşağıdaki teklifi kalıcı olarak silmek istediğinizden emin misiniz?\n\n{seciliTeklifMetni}\n\nBu işlem geri alınamaz!", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (cevap == DialogResult.Yes)
            {
                try
                {
                    using (var baglanti = new SQLiteConnection(Veritabani.BaglantiCumlesi))
                    {
                        baglanti.Open();

                        string kalemSilSorgu = "DELETE FROM TeklifKalemleri WHERE TeklifID = @teklifID";
                        using (var komut = new SQLiteCommand(kalemSilSorgu, baglanti))
                        {
                            komut.Parameters.AddWithValue("@teklifID", seciliTeklifID);
                            komut.ExecuteNonQuery();
                        }

                        string teklifSilSorgu = "DELETE FROM Teklifler WHERE TeklifID = @teklifID";
                        using (var komut = new SQLiteCommand(teklifSilSorgu, baglanti))
                        {
                            komut.Parameters.AddWithValue("@teklifID", seciliTeklifID);
                            komut.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Teklif başarıyla silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    int seciliMusteriID = Convert.ToInt32(cmbMusteriler.SelectedValue);
                    Temizle();
                    KayitliTeklifleriYukle(seciliMusteriID);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Teklif silinirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnResimEkle_Click(object sender, EventArgs e)
        {

        }

        private void btnResimSil_Click(object sender, EventArgs e)
        {

        }
    }
}