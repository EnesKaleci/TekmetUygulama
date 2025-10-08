using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TEKMET
{
    public partial class FrmMusteriHesap : Form
    {
        private DataTable dtMusteriler;
        // ====================================================================
        // YENİ EKLENEN YARDIMCI METOTLAR (DBNull Hatasını Çözmek İçin)
        // ====================================================================

        // Bir hücrenin değerini güvenli bir şekilde string'e (metne) çevirir.
        private string GetText(object value)
        {
            // Eğer değer DBNull ise boş metin (""), değilse kendi değerini döndür.
            return value == DBNull.Value ? "" : value.ToString();
        }

        // Bir hücrenin değerini güvenli bir şekilde decimal'a (parasal değere) çevirir.
        private decimal GetDecimal(object value)
        {
            // Eğer değer DBNull ise 0, değilse kendi ondalık değerini döndür.
            return value == DBNull.Value ? 0 : Convert.ToDecimal(value);
        }

        // ====================================================================
        public FrmMusteriHesap()
        {
            InitializeComponent();
        }

        private void FrmMusteriHesap_Load(object sender, EventArgs e)
        {
            TumMusterileriYukle();
            Temizle(); // <-- BU SATIRI EKLEYİN
        }
        private void Temizle()
        {
            // Bu metot, tüm seçimleri ve veri alanlarını temizleyerek
            // formu başlangıç durumuna getirir.
            dgvMusterilerListesi.ClearSelection();
            lblAdSoyad.Text = "...";
            lblTelefon.Text = "...";
            lblGuncelBakiye.Text = "0,00 ₺";
            dgvIsler.DataSource = null;
            dgvOdemeler.DataSource = null;
            txtArama.Focus(); // İmleci arama kutusuna odaklar.
        }
        private void TumMusterileriYukle()
        {
            using (var baglanti = new SQLiteConnection(Veritabani.BaglantiCumlesi))
            {
                string sorgu = "SELECT MusteriID, AdSoyad, Telefon FROM Musteriler ORDER BY AdSoyad";
                using (var da = new SQLiteDataAdapter(sorgu, baglanti))
                {
                    dtMusteriler = new DataTable(); // Hafızadaki listeyi doldur
                    da.Fill(dtMusteriler);
                    dgvMusterilerListesi.DataSource = dtMusteriler; // Ekrana yansıt
                }
            }

            // Müşteri listesi tablosunun görünüm ayarları
            if (dgvMusterilerListesi.Columns.Count > 0)
            {
                dgvMusterilerListesi.Columns["MusteriID"].Visible = false; // ID'yi gizle
                dgvMusterilerListesi.Columns["AdSoyad"].HeaderText = "Ad Soyad";
                dgvMusterilerListesi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void txtArama_TextChanged(object sender, EventArgs e)
        {
            // DataView, hafızadaki DataTable'ı filtrelemek için kullanılır.
            DataView dv = dtMusteriler.DefaultView;
            // AdSoyad sütununda, arama kutusundaki metni içeren satırları bul.
            // '%' joker karakterdir, "ah" yazınca içinde "ah" geçen her şeyi bulur.
            dv.RowFilter = $"AdSoyad LIKE '%{txtArama.Text}%'";
            dgvMusterilerListesi.DataSource = dv.ToTable();
        }

        private void dgvMusterilerListesi_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMusterilerListesi.CurrentRow == null)
            {
                // Seçim yoksa sağ tarafı temizle
                lblAdSoyad.Text = "...";
                lblTelefon.Text = "...";
                lblGuncelBakiye.Text = "0.00 TL";
                dgvIsler.DataSource = null;
                dgvOdemeler.DataSource = null;
                return;
            }

            // Seçilen müşterinin ID'sini al
            int seciliMusteriID = Convert.ToInt32(dgvMusterilerListesi.CurrentRow.Cells["MusteriID"].Value);

            // Müşteri bilgilerini etiketlere yazdır
            lblAdSoyad.Text = dgvMusterilerListesi.CurrentRow.Cells["AdSoyad"].Value.ToString();
            lblTelefon.Text = dgvMusterilerListesi.CurrentRow.Cells["Telefon"].Value.ToString();

            // O müşteriye ait işleri yükle
            IsleriYukle(seciliMusteriID);
        }

        private void IsleriYukle(int musteriID)
        {
            using (var baglanti = new SQLiteConnection(Veritabani.BaglantiCumlesi))
            {
                // Bu sorgu biraz karmaşık. Her işin fiyatını ve o işe yapılan toplam ödemeyi
                // hesaplayıp, aradaki farkı "Kalan" olarak bize verir.
                string sorgu = @"
                    SELECT
                        I.IsID,
                        I.IsTanimi,
                        I.IsTarihi,
                        I.Fiyat,
                        (I.Fiyat - IFNULL((SELECT SUM(OdemeMiktari) FROM Odemeler WHERE IsID = I.IsID), 0)) AS Kalan
                    FROM Isler I
                    WHERE I.MusteriID = @musteriID";

                using (var da = new SQLiteDataAdapter(sorgu, baglanti))
                {
                    da.SelectCommand.Parameters.AddWithValue("@musteriID", musteriID);
                    var dtIsler = new DataTable();
                    da.Fill(dtIsler);
                    dgvIsler.DataSource = dtIsler;
                }
            }

            // İşler listesi tablosunun görünüm ayarları
            if (dgvIsler.Columns.Count > 0)
            {
                dgvIsler.Columns["IsID"].Visible = false; // ID'yi gizle
                dgvIsler.Columns["IsTanimi"].HeaderText = "İş Tanımı";
                dgvIsler.Columns["IsTarihi"].HeaderText = "Tarih";
                dgvIsler.Columns["Kalan"].DefaultCellStyle.Format = "c2"; // Para formatı
                dgvIsler.Columns["Fiyat"].DefaultCellStyle.Format = "c2"; // Para formatı
                dgvIsler.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

            // Ödemeler listesini temizle, çünkü henüz bir iş seçilmedi
            dgvOdemeler.DataSource = null;

            // Müşterinin toplam bakiyesini güncelle
            GuncelBakiyeHesapla();
        }
        private void OdemeleriYukle(int isID)
        {
            using (var baglanti = new SQLiteConnection(Veritabani.BaglantiCumlesi))
            {
                string sorgu = "SELECT OdemeTarihi, Aciklama, OdemeMiktari FROM Odemeler WHERE IsID = @isID";
                using (var da = new SQLiteDataAdapter(sorgu, baglanti))
                {
                    da.SelectCommand.Parameters.AddWithValue("@isID", isID);
                    var dtOdemeler = new DataTable();
                    da.Fill(dtOdemeler);
                    dgvOdemeler.DataSource = dtOdemeler;
                }
            }

            // Ödemeler listesi tablosunun görünüm ayarları
            if (dgvOdemeler.Columns.Count > 0)
            {
                dgvOdemeler.Columns["OdemeTarihi"].HeaderText = "Ödeme Tarihi";
                dgvOdemeler.Columns["OdemeMiktari"].HeaderText = "Tutar";
                dgvOdemeler.Columns["OdemeMiktari"].DefaultCellStyle.Format = "c2"; // Para formatı
                dgvOdemeler.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void GuncelBakiyeHesapla()
        {
            decimal toplamBakiye = 0;
            foreach (DataGridViewRow row in dgvIsler.Rows)
            {
                // 'Kalan' sütunundaki değeri topla
                toplamBakiye += Convert.ToDecimal(row.Cells["Kalan"].Value);
            }
            lblGuncelBakiye.Text = toplamBakiye.ToString("c2"); // Para formatında göster
        }

        private void dgvIsler_SelectionChanged(object sender, EventArgs e)
        {
            // 1. Kontrol: Geçerli bir satır seçili mi?
            if (dgvIsler.CurrentRow == null)
            {
                dgvOdemeler.DataSource = null; // Ödemeler listesini temizle
                return;
            }

            // 2. Kontrol (YENİ EKLENDİ): Hücrenin değeri DBNull mu?
            // Bu, özellikle liste yenilendiğinde hatayı önler.
            if (dgvIsler.CurrentRow.Cells["IsID"].Value is DBNull)
            {
                dgvOdemeler.DataSource = null; // Ödemeler listesini temizle
                return;
            }

            // Tüm kontrollerden geçtiyse, ID'yi al ve ödemeleri yükle.
            int seciliIsID = Convert.ToInt32(dgvIsler.CurrentRow.Cells["IsID"].Value);
            OdemeleriYukle(seciliIsID);
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnYeniOdemeEkle_Click(object sender, EventArgs e)
        {

            // Kontrol 1: Bir iş seçili mi? (İş seçiliyse müşteri de seçilidir)
            if (dgvIsler.CurrentRow == null)
            {
                MessageBox.Show("Lütfen ödeme eklemek için listeden bir iş seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Seçili işin ve müşterinin ID'lerini al.
            int seciliIsID = Convert.ToInt32(dgvIsler.CurrentRow.Cells["IsID"].Value);
            int seciliMusteriID = Convert.ToInt32(dgvMusterilerListesi.CurrentRow.Cells["MusteriID"].Value);

            // FrmYeniOdeme formunu, bu iş ID'si ile oluştur ve aç.
            FrmYeniOdeme frm = new FrmYeniOdeme(seciliIsID);
            DialogResult sonuc = frm.ShowDialog();

            // Eğer FrmYeniOdeme formu "OK" sonucuyla (yani Kaydet'e basılarak) kapatıldıysa,
            // her iki listeyi de yenile:
            if (sonuc == DialogResult.OK)
            {
                // 1. İşler listesini yenile (çünkü o işin "Kalan" borcu değişti).
                IsleriYukle(seciliMusteriID);
                // 2. Ödemeler listesini yenile (çünkü yeni bir ödeme eklendi).
                OdemeleriYukle(seciliIsID);
            }

        }

        private void btnYeniIsEkle_Click(object sender, EventArgs e)
        {
            // Kontrol: Bir müşteri seçili mi?
            if (dgvMusterilerListesi.CurrentRow == null)
            {
                MessageBox.Show("Lütfen önce bir müşteri seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Seçili müşterinin ID'sini al.
            int seciliMusteriID = Convert.ToInt32(dgvMusterilerListesi.CurrentRow.Cells["MusteriID"].Value);

            // FrmYeniIs formunu, bu müşteri ID'si ile oluştur ve aç.
            FrmYeniIs frm = new FrmYeniIs(seciliMusteriID);
            DialogResult sonuc = frm.ShowDialog();

            // Eğer FrmYeniIs formu "OK" sonucuyla (yani Kaydet'e basılarak) kapatıldıysa,
            // İşler listesini yenile ki yeni eklenen iş anında görünsün.
            if (sonuc == DialogResult.OK)
            {
                IsleriYukle(seciliMusteriID);
            }
        }

        private void dgvIsler_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Geçerli bir satıra çift tıklandı mı diye kontrol et
            if (e.RowIndex < 0) return;

            // Seçili müşterinin ve işin ID'lerini al
            int seciliMusteriID = Convert.ToInt32(dgvMusterilerListesi.CurrentRow.Cells["MusteriID"].Value);
            int seciliIsID = Convert.ToInt32(dgvIsler.CurrentRow.Cells["IsID"].Value);

            // FrmYeniIs formunu DÜZENLEME MODUNDA aç (hem musteriID hem isID gönderiyoruz)
            FrmYeniIs frm = new FrmYeniIs(seciliMusteriID, seciliIsID);
            DialogResult sonuc = frm.ShowDialog();

            // Formdan OK sonucu döndüyse (Kaydet veya Sil'e basıldıysa) listeyi yenile
            if (sonuc == DialogResult.OK)
            {
                IsleriYukle(seciliMusteriID);
            }
        }
    }

}
