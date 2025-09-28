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
        private int? aktifTeklifID = null; // ? işareti, bu değişkenin null (boş) olabileceği anlamına gelir.
        public FrmTeklifOlustur()
        {
            InitializeComponent();
        }

        private void FrmTeklifOlustur_Load(object sender, EventArgs e)
        {
            MusterileriYukle();
            TeklifTablosunuHazirla();

        }
        private void MusterileriYukle()
        {
            // Olayı geçici olarak devre dışı bırakıyoruz ki kodumuz otomatik çalışmasın.
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

            // Seçimi temizliyoruz.
            cmbMusteriler.SelectedIndex = -1;

            // İşimiz bitti, olayı tekrar aktif hale getiriyoruz.
            // Artık sadece kullanıcı bir seçim yaptığında çalışacak.
            cmbMusteriler.SelectedIndexChanged += cmbMusteriler_SelectedIndexChanged;
        }

        private void TeklifTablosunuHazirla()
        {
            dtTeklif = new DataTable();
            dtTeklif.Columns.Add("Aciklama", typeof(string));
            dtTeklif.Columns.Add("Miktar", typeof(decimal));
            dtTeklif.Columns.Add("Birim", typeof(string));
            dtTeklif.Columns.Add("BirimFiyat", typeof(decimal));
            dtTeklif.Columns.Add("ToplamFiyat", typeof(decimal), "Miktar * BirimFiyat"); // Otomatik hesaplama için DataTable'a formül ekledik!

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

        // YENİ EKLENDİ: "Satır Ekle (+)" butonuna tıklandığında çalışır
        private void btnSatirEkle_Click(object sender, EventArgs e)
        {
            // Hafızadaki dtTeklif tablomuza boş bir satır ekliyoruz.
            // Bu satır otomatik olarak ekrandaki DataGridView'de de görünür.
            dtTeklif.Rows.Add(dtTeklif.NewRow());
        }

        // YENİ EKLENDİ: "Satır Sil (-)" butonuna tıklandığında çalışır
        private void btnSatirSil_Click(object sender, EventArgs e)
        {
            // Eğer seçili bir satır varsa
            if (dgvTeklifKalemleri.CurrentRow != null)
            {
                // O satırı DataGridView'den sil
                dgvTeklifKalemleri.Rows.Remove(dgvTeklifKalemleri.CurrentRow);
            }
            else
            {
                MessageBox.Show("Lütfen silmek için bir satır seçin.", "Uyarı");
            }
        }

        private void btnSatirEkle_Click_1(object sender, EventArgs e)
        {
            // Hafızadaki dtTeklif tablomuza boş bir satır ekliyoruz.
            // Bu satır otomatik olarak ekrandaki DataGridView'de de görünür.
            dtTeklif.Rows.Add(dtTeklif.NewRow());
            HesaplaGenelToplamlari();
        }

        private void btnSatirSil_Click_1(object sender, EventArgs e)
        {
            // Eğer seçili bir satır varsa
            if (dgvTeklifKalemleri.CurrentRow != null)
            {
                // O satırı DataGridView'den sil
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
            // Hücredeki değişiklik bittiğinde genel toplamları yeniden hesapla.
            // Bu, Miktar veya BirimFiyat değiştiğinde anında güncelleme sağlar.
            HesaplaGenelToplamlari();
        }

        private void dgvTeklifKalemleri_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            // Satır silme işlemi tamamlandığında genel toplamları yeniden hesapla
            HesaplaGenelToplamlari();
        }
        private void HesaplaGenelToplamlari()
        {
            // Düzgün bir hesaplama için anlık olarak DataGridView'in kendisini kullanmak daha sağlıklıdır.
            decimal araToplam = 0;
            foreach (DataGridViewRow row in dgvTeklifKalemleri.Rows)
            {
                // Hücrelerin null veya geçersiz değer içerme ihtimaline karşı kontrol
                if (row.Cells["ToplamFiyat"].Value != null && row.Cells["ToplamFiyat"].Value != DBNull.Value)
                {
                    araToplam += Convert.ToDecimal(row.Cells["ToplamFiyat"].Value);
                }
            }

            decimal kdv = araToplam * 0.20m; // %20 KDV
            decimal genelToplam = araToplam + kdv;

            lblAraToplam.Text = araToplam.ToString("c2");
            lblKdv.Text = kdv.ToString("c2");
            lblGenelToplam.Text = genelToplam.ToString("c2");
        }

        private void btnPdfOlustur_Click(object sender, EventArgs e)
        {
            // 1. Kontroller: Müşteri seçili mi ve en az bir teklif satırı var mı?
            if (cmbMusteriler.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen bir müşteri seçin.", "Uyarı");
                return;
            }
            if (dgvTeklifKalemleri.Rows.Count == 0)
            {
                MessageBox.Show("Lütfen en az bir teklif kalemi ekleyin.", "Uyarı");
                return;
            }

            // 2. Kullanıcıya dosyayı nereye kaydedeceğini sor
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PDF Dosyası (*.pdf)|*.pdf";
            sfd.FileName = $"{cmbMusteriler.Text} - Teklif.pdf";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    TeklifPdfOlustur(sfd.FileName); // PDF çizim metodunu çağır

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

        private void TeklifPdfOlustur(string dosyaYolu)
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
            gfx.DrawString(cmbMusteriler.Text, fontBaslik, XBrushes.Black, 40, y);
            y += 35;

            // --- YENİ KOORDİNATLAR ---
            double aciklamaX = 50;
            double miktarX = 290;
            double birimFiyatX = 370;
            double toplamFiyatX = 460;
            double toplamAlanGenislik = 95; // Toplamlar için sağda bırakılacak boşluk

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

            // Toplamlar Alanı
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

            // ... Önceki kodlar (Genel Toplamı çizdikten sonra) ...
            y += 40; // Toplamlar ile notlar arasına boşluk bırak

            // YENİ EKLENEN NOT BÖLÜMÜ
            // Eğer txtNotlar kutusu boş değilse, notları PDF'e ekle
            if (!string.IsNullOrWhiteSpace(txtNotlar.Text))
            {
                gfx.DrawString("Teklif Notları:", fontBaslik, XBrushes.Black, 40, y);
                y += 20;

                // Çok satırlı metni PDF'e düzgün bir şekilde çizmek için TextFormatter kullanıyoruz.
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

        private void cmbMusteriler_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kontrolü daha esnek ve güvenli hale getirdik
            if (cmbMusteriler.SelectedValue != null && cmbMusteriler.SelectedValue != DBNull.Value)
            {
                int seciliMusteriID = Convert.ToInt32(cmbMusteriler.SelectedValue);
                KayitliTeklifleriYukle(seciliMusteriID);
            }
            else
            {
                // Geçerli bir seçim yoksa teklifler listesini temizle.
                lstKayitliTeklifler.DataSource = null;
            }
        }
        private void KayitliTeklifleriYukle(int musteriID)
        {
            using (var baglanti = new SQLiteConnection(Veritabani.BaglantiCumlesi))
            {
                // Müşteriye ait teklifleri, tarihi ve toplam tutarıyla birlikte çek.
                string sorgu = "SELECT TeklifID, TeklifAdi, TeklifTarihi, GenelToplam FROM Teklifler WHERE MusteriID = @musteriID ORDER BY TeklifTarihi DESC";
                using (var da = new SQLiteDataAdapter(sorgu, baglanti))
                {
                    da.SelectCommand.Parameters.AddWithValue("@musteriID", musteriID);
                    var dt = new DataTable();
                    da.Fill(dt);

                    // DataTable'a, ListBox'ta güzel görünmesi için yeni bir kolon ekliyoruz.
                    dt.Columns.Add("GorunenMetin", typeof(string));
                    foreach (DataRow row in dt.Rows)
                    {
                        DateTime tarih = DateTime.Parse(row["TeklifTarihi"].ToString());
                        decimal toplam = Convert.ToDecimal(row["GenelToplam"]);
                        row["GorunenMetin"] = $"{tarih:dd.MM.yyyy} - {toplam:c2} Tutarında Teklif";
                        string teklifAdi = row["TeklifAdi"].ToString();
                        row["GorunenMetin"] = $"{tarih:dd.MM.yyyy} - {teklifAdi} - {toplam:c2}";
                    }

                    // ListBox'a verileri atama
                    lstKayitliTeklifler.DataSource = dt;
                    lstKayitliTeklifler.DisplayMember = "GorunenMetin"; // Kullanıcının göreceği alan
                    lstKayitliTeklifler.ValueMember = "TeklifID";     // Arka planda tutulacak değer
                }
            }
        }

        private void btnTeklifiYukle_Click(object sender, EventArgs e)
        {
            // Listeden bir teklif seçili mi diye kontrol et.
            if (lstKayitliTeklifler.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen yüklemek için bir teklif seçin.", "Uyarı");
                return;
            }

            // Seçili teklifin ID'sini al.
            int seciliTeklifID = Convert.ToInt32(lstKayitliTeklifler.SelectedValue);

            // Ana yükleme metodunu çağır.
            TeklifiFormaYukle(seciliTeklifID);
        }
        private void TeklifiFormaYukle(int teklifID)
        {
            // Formu "düzenleme moduna" al.
            this.aktifTeklifID = teklifID;

            try
            {
                using (var baglanti = new SQLiteConnection(Veritabani.BaglantiCumlesi))
                {
                    baglanti.Open();

                    // 1. Adım: Ana Teklif Bilgilerini Çek (Tarih, Notlar vs.)
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

                    // 2. Adım: Teklif Kalemlerini Çek ve Tabloya Doldur
                    dtTeklif.Rows.Clear(); // Mevcut tabloyu temizle
                    string kalemSorgu = "SELECT Aciklama, Miktar, Birim, BirimFiyat FROM TeklifKalemleri WHERE TeklifID = @id";
                    using (var da = new SQLiteDataAdapter(kalemSorgu, baglanti))
                    {
                        da.SelectCommand.Parameters.AddWithValue("@id", teklifID);
                        da.Fill(dtTeklif); // dtTeklif DataTable'ını veritabanından gelenlerle doldur.
                    }
                }
                // Toplamları yeniden hesapla (ekranda görünsün diye).
                HesaplaGenelToplamlari();
                MessageBox.Show("Teklif başarıyla yüklendi. Şimdi üzerinde değişiklik yapabilirsiniz.", "Bilgi");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Teklif yüklenirken bir hata oluştu: " + ex.Message, "Hata");
            }
        }
        private void Temizle()
        {
            aktifTeklifID = null; // Formu "yeni kayıt" moduna al
            dtTeklif.Rows.Clear(); // Tabloyu temizle
            cmbMusteriler.SelectedIndex = -1;
            lstKayitliTeklifler.DataSource = null;
            txtNotlar.Clear();
            dtpTarih.Value = DateTime.Now;
            HesaplaGenelToplamlari(); // Toplamları sıfırla
        }

        private void btnYnTklf_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            // --- 1. Kontroller ---
            if (cmbMusteriler.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen bir müşteri seçin.", "Uyarı");
                return;
            }
            if (dgvTeklifKalemleri.Rows.Count == 0 || dgvTeklifKalemleri.Rows[0].IsNewRow)
            {
                MessageBox.Show("Lütfen en az bir teklif kalemi ekleyin.", "Uyarı");
                return;
            }

            // --- 2. Verileri Toplama ---
            int musteriID = Convert.ToInt32(cmbMusteriler.SelectedValue);
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

                    // --- 3. Akıllı Mantık: Yeni Kayıt mı? Güncelleme mi? ---
                    if (aktifTeklifID == null) // YENİ KAYIT
                    {
                        // A. Ana teklif kaydını "Teklifler" tablosuna ekle
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

                        // B. Teklif kalemlerini "TeklifKalemleri" tablosuna ekle
                        foreach (DataRow row in dtTeklif.Rows)
                        {
                            if (row.RowState == DataRowState.Deleted) continue; // Silinmiş satırları atla
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
                        // A. Ana teklif kaydını "Teklifler" tablosunda güncelle
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

                        // B. Eski teklif kalemlerini sil
                        string silmeSorgusu = "DELETE FROM TeklifKalemleri WHERE TeklifID = @id";
                        using (var komut = new SQLiteCommand(silmeSorgusu, baglanti))
                        {
                            komut.Parameters.AddWithValue("@id", aktifTeklifID.Value);
                            komut.ExecuteNonQuery();
                        }

                        // C. Güncel teklif kalemlerini yeniden ekle
                        foreach (DataRow row in dtTeklif.Rows)
                        {
                            if (row.RowState == DataRowState.Deleted) continue;
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
                } // using baglanti
                MessageBox.Show("Teklif başarıyla kaydedildi!", "Başarılı");
                Temizle();
                KayitliTeklifleriYukle(musteriID);
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
            // Kontrolü daha esnek ve güvenli hale getirdik
            if (cmbMusteriler.SelectedValue != null && cmbMusteriler.SelectedValue != DBNull.Value)
            {
                // Seçili müşterinin ID'sini al (Convert.ToInt32 long'u çevirebilir)
                int seciliMusteriID = Convert.ToInt32(cmbMusteriler.SelectedValue);

                // Bu müşteri için teklifleri yükleyen metodu çağır.
                KayitliTeklifleriYukle(seciliMusteriID);
            }
            else
            {
                // Eğer bir müşteri seçili değilse, kullanıcıyı uyar.
                MessageBox.Show("Lütfen önce bir müşteri seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnTeklifiSil_Click(object sender, EventArgs e)
        {
            // 1. Kontrol: Listeden bir teklif seçili mi?
            if (lstKayitliTeklifler.SelectedValue == null || lstKayitliTeklifler.SelectedValue is DBNull)
            {
                MessageBox.Show("Lütfen silmek için bir teklif seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Seçili teklifin ID'sini ve görünen metnini al
            int seciliTeklifID = Convert.ToInt32(lstKayitliTeklifler.SelectedValue);
            string seciliTeklifMetni = lstKayitliTeklifler.Text;

            // 3. Kullanıcıdan silme onayı al (Bu çok önemlidir!)
            DialogResult cevap = MessageBox.Show($"Aşağıdaki teklifi kalıcı olarak silmek istediğinizden emin misiniz?\n\n{seciliTeklifMetni}\n\nBu işlem geri alınamaz!", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (cevap == DialogResult.Yes)
            {
                try
                {
                    using (var baglanti = new SQLiteConnection(Veritabani.BaglantiCumlesi))
                    {
                        baglanti.Open();

                        // Önce teklife ait kalemleri (detayları) TeklifKalemleri tablosundan sil
                        string kalemSilSorgu = "DELETE FROM TeklifKalemleri WHERE TeklifID = @teklifID";
                        using (var komut = new SQLiteCommand(kalemSilSorgu, baglanti))
                        {
                            komut.Parameters.AddWithValue("@teklifID", seciliTeklifID);
                            komut.ExecuteNonQuery();
                        }

                        // Sonra teklifin ana kaydını Teklifler tablosundan sil
                        string teklifSilSorgu = "DELETE FROM Teklifler WHERE TeklifID = @teklifID";
                        using (var komut = new SQLiteCommand(teklifSilSorgu, baglanti))
                        {
                            komut.Parameters.AddWithValue("@teklifID", seciliTeklifID);
                            komut.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Teklif başarıyla silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Silme sonrası formu ve listeleri güncelle
                    int seciliMusteriID = Convert.ToInt32(cmbMusteriler.SelectedValue);
                    Temizle(); // Formu temizle
                    KayitliTeklifleriYukle(seciliMusteriID); // Müşterinin teklif listesini yenile
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Teklif silinirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
    

