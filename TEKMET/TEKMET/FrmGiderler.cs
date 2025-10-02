using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TEKMET
{
    public partial class FrmGiderler : Form
    {
        public FrmGiderler()
        {
            InitializeComponent();
        }

        private void FrmGiderler_Load(object sender, EventArgs e)
        {

            CalisanlariDoldur();
            AylariDoldur();
            YillariDoldur();
            GiderlerTablosunuHazirla();
        }
        private void CalisanlariDoldur()
        {
            using (var baglanti = new SQLiteConnection(Veritabani.BaglantiCumlesi))
            {
                string sorgu = "SELECT CalisanID, AdSoyad FROM Calisanlar ORDER BY AdSoyad";
                using (var da = new SQLiteDataAdapter(sorgu, baglanti))
                {
                    var dt = new DataTable();
                    da.Fill(dt);

                    // Listeye, çalışana özel olmayan giderler için "Genel Gider" seçeneği ekliyoruz.
                    DataRow genelGiderRow = dt.NewRow();
                    genelGiderRow["CalisanID"] = 0; // ID'si 0 olanlar genel gider olsun.
                    genelGiderRow["AdSoyad"] = "GENEL GİDER (Şirket)";
                    dt.Rows.InsertAt(genelGiderRow, 0);

                    cmbCalisanlar.DataSource = dt;
                    cmbCalisanlar.DisplayMember = "AdSoyad";
                    cmbCalisanlar.ValueMember = "CalisanID";
                }
            }
        }

        private void AylariDoldur()
        {
            // Aylar listesini Türkçe olarak oluştur ve ComboBox'a ekle.
            var aylar = CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat.MonthNames
                .Select((ay, index) => new { Ad = ay, Deger = index + 1 })
                .Where(a => !string.IsNullOrEmpty(a.Ad))
                .ToList();

            cmbAy.DataSource = aylar;
            cmbAy.DisplayMember = "Ad";
            cmbAy.ValueMember = "Deger";

            // Form açıldığında mevcut ayı seçili getir.
            cmbAy.SelectedValue = DateTime.Now.Month;
        }

        private void YillariDoldur()
        {
            // Mevcut yıldan 5 yıl öncesi ve 1 yıl sonrasına kadar bir liste oluştur.
            int mevcutYil = DateTime.Now.Year;
            for (int i = mevcutYil + 1; i >= mevcutYil - 5; i--)
            {
                cmbYil.Items.Add(i);
            }

            // Form açıldığında mevcut yılı seçili getir.
            cmbYil.SelectedItem = mevcutYil;
        }

        private void GiderlerTablosunuHazirla()
        {
            dgvGiderler.AutoGenerateColumns = false;
            dgvGiderler.Columns.Clear();
            dgvGiderler.RowHeadersVisible = false;

            // YENİ EKLENDİ: Satır yüksekliklerinin içeriğe göre otomatik ayarlanmasını sağlar.
            dgvGiderler.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dgvGiderler.Columns.Add(new DataGridViewTextBoxColumn { Name = "Tarih", HeaderText = "Tarih", DataPropertyName = "Tarih", Width = 100 });
            dgvGiderler.Columns.Add(new DataGridViewTextBoxColumn { Name = "CalisanAdi", HeaderText = "Personel / Gider Türü", DataPropertyName = "AdSoyad", Width = 150 });

            // "Açıklama" kolonunu ayrı bir değişken olarak oluşturuyoruz ki stilini ayarlayabilelim.
            var aciklamaKolonu = new DataGridViewTextBoxColumn
            {
                Name = "Aciklama",
                HeaderText = "Açıklama",
                DataPropertyName = "Aciklama",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };
            // YENİ EKLENDİ: Bu kolondaki metinlerin, hücreye sığmadığında alt satıra kaymasını sağlar.
            aciklamaKolonu.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvGiderler.Columns.Add(aciklamaKolonu); // Ayarlanmış kolonu tabloya ekliyoruz.

            dgvGiderler.Columns.Add(new DataGridViewTextBoxColumn { Name = "Tutar", HeaderText = "Tutar", DataPropertyName = "Tutar", Width = 100, DefaultCellStyle = { Format = "c2" } });
        }
        

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            // Kontroller
            if (nudTutar.Value <= 0)
            {
                MessageBox.Show("Tutar 0'dan büyük olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtAciklama.Text))
            {
                MessageBox.Show("Açıklama alanı boş bırakılamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var baglanti = new SQLiteConnection(Veritabani.BaglantiCumlesi))
                {
                    baglanti.Open();
                    string sorgu = "INSERT INTO Giderler (CalisanID, Aciklama, Tutar, Tarih) VALUES (@calisanID, @aciklama, @tutar, @tarih)";
                    using (var komut = new SQLiteCommand(sorgu, baglanti))
                    {
                        // Eğer "GENEL GİDER" seçildiyse CalisanID'yi DBNull olarak kaydet.
                        if (Convert.ToInt32(cmbCalisanlar.SelectedValue) == 0) // İlk kodda 0 olarak belirlemiştik, şimdi DBNull'a çevirdik.
                        {
                            komut.Parameters.AddWithValue("@calisanID", DBNull.Value);
                        }
                        else
                        {
                            komut.Parameters.AddWithValue("@calisanID", cmbCalisanlar.SelectedValue);
                        }

                        komut.Parameters.AddWithValue("@aciklama", txtAciklama.Text);
                        komut.Parameters.AddWithValue("@tutar", nudTutar.Value);
                        komut.Parameters.AddWithValue("@tarih", dtpTarih.Value.ToString("yyyy-MM-dd"));
                        komut.ExecuteNonQuery();
                    }
                }

                // Kayıt sonrası giriş alanlarını temizle
                txtAciklama.Clear();
                nudTutar.Value = 0;

                // Listeyi otomatik olarak yenile
                btnListele.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gider kaydedilirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            string seciliAy = ((int)cmbAy.SelectedValue).ToString("D2");
            string seciliYil = cmbYil.SelectedItem.ToString();
            int seciliIndex = cmbCalisanlar.SelectedIndex; // Seçimin sırasını alıyoruz

            DataTable dt = new DataTable();

            using (var baglanti = new SQLiteConnection(Veritabani.BaglantiCumlesi))
            {
                string sorgu;
                SQLiteDataAdapter da;

                // --- YENİ VE BASİTLEŞTİRİLMİŞ MANTIK ---
                // Eğer ComboBox'tan "GENEL GİDER" seçiliyse (yani ilk sıradaki öğe)
                if (seciliIndex == 0)
                {
                    // Çalışan filtresi OLMAYAN sorguyu kullan
                    sorgu = @"
                SELECT G.Tarih, IFNULL(C.AdSoyad, 'GENEL GİDER') AS AdSoyad, G.Aciklama, G.Tutar
                FROM Giderler G
                LEFT JOIN Calisanlar C ON G.CalisanID = C.CalisanID
                WHERE strftime('%Y', G.Tarih) = @yil AND strftime('%m', G.Tarih) = @ay
                ORDER BY G.Tarih";
                    da = new SQLiteDataAdapter(sorgu, baglanti);
                }
                // Eğer belirli bir çalışan seçiliyse
                else
                {
                    // Çalışan filtresi OLAN sorguyu kullan
                    sorgu = @"
                SELECT G.Tarih, C.AdSoyad, G.Aciklama, G.Tutar
                FROM Giderler G
                JOIN Calisanlar C ON G.CalisanID = C.CalisanID
                WHERE strftime('%Y', G.Tarih) = @yil AND strftime('%m', G.Tarih) = @ay AND G.CalisanID = @calisanID
                ORDER BY G.Tarih";
                    da = new SQLiteDataAdapter(sorgu, baglanti);
                    da.SelectCommand.Parameters.AddWithValue("@calisanID", cmbCalisanlar.SelectedValue);
                }

                // Ortak parametreleri her iki sorgu için de ekle
                da.SelectCommand.Parameters.AddWithValue("@yil", seciliYil);
                da.SelectCommand.Parameters.AddWithValue("@ay", seciliAy);

                da.Fill(dt);
            }

            dgvGiderler.DataSource = dt;

            // Toplam hesaplama kısmı (güvenlik için küçük bir iyileştirme ile)
            decimal aylikToplam = 0;
            foreach (DataGridViewRow row in dgvGiderler.Rows)
            {
                decimal tutar;
                if (row.Cells["Tutar"].Value != null && decimal.TryParse(row.Cells["Tutar"].Value.ToString(), out tutar))
                {
                    aylikToplam += tutar;
                }
            }
            lblAylikToplam.Text = $" {aylikToplam:c2}";
        }
    }
    
}
