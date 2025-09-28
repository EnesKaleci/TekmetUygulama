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
    public partial class FrmMusteriEkle : Form
    {
        // Seçili olan müşterinin ID'sini saklamak için bir değişken.
        // null ise yeni kayıt, bir değeri varsa düzenleme/silme modundayız demektir.
        private int? seciliMusteriID = null;
        public FrmMusteriEkle()
        {
            InitializeComponent();
        }

        private void FrmMusteriEkle_Load(object sender, EventArgs e)
        {
            MusterileriYukle();
            Temizle(); // Formu temiz bir başlangıç için hazırla
        }
        private void MusterileriYukle()
        {
            using (var baglanti = new SQLiteConnection(Veritabani.BaglantiCumlesi))
            {
                string sorgu = "SELECT MusteriID, AdSoyad, Telefon, Adres, KayitTarihi FROM Musteriler ORDER BY AdSoyad";
                using (var da = new SQLiteDataAdapter(sorgu, baglanti))
                {
                    var dt = new DataTable();
                    da.Fill(dt);
                    dgvMusteriler.DataSource = dt;
                }
            }

            if (dgvMusteriler.Columns.Count > 0)
            {
                dgvMusteriler.Columns["MusteriID"].HeaderText = "No";
                dgvMusteriler.Columns["AdSoyad"].HeaderText = "Ad Soyad";
                dgvMusteriler.Columns["KayitTarihi"].HeaderText = "Kayıt Tarihi";
                dgvMusteriler.Columns["MusteriID"].Width = 40;
                dgvMusteriler.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void dgvMusteriler_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMusteriler.CurrentRow != null)
            {
                DataGridViewRow seciliSatir = dgvMusteriler.CurrentRow;
                seciliMusteriID = Convert.ToInt32(seciliSatir.Cells["MusteriID"].Value);

                // Satırdaki verileri TextBox'lara aktar.
                txtAdSoyad.Text = seciliSatir.Cells["AdSoyad"].Value.ToString();
                txtTelefon.Text = seciliSatir.Cells["Telefon"].Value.ToString();
                txtAdres.Text = seciliSatir.Cells["Adres"].Value.ToString();

                btnSil.Enabled = true; // Bir müşteri seçili olduğu için Sil butonu aktif olsun.
            }
        }
        private void Temizle()
        {
            seciliMusteriID = null;
            txtAdSoyad.Clear();
            txtTelefon.Clear();
            txtAdres.Clear();
            dgvMusteriler.ClearSelection();
            btnSil.Enabled = false; // Seçim olmadığı için Sil butonu pasif olsun.
            txtAdSoyad.Focus();
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAdSoyad.Text))
            {
                MessageBox.Show("Ad Soyad alanı boş bırakılamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var baglanti = new SQLiteConnection(Veritabani.BaglantiCumlesi))
                {
                    baglanti.Open();
                    string sorgu;
                    SQLiteCommand komut;

                    if (seciliMusteriID == null) // YENİ KAYIT (INSERT)
                    {
                        sorgu = "INSERT INTO Musteriler (AdSoyad, Telefon, Adres, KayitTarihi) VALUES (@adSoyad, @telefon, @adres, @kayitTarihi)";
                        komut = new SQLiteCommand(sorgu, baglanti);
                    }
                    else // GÜNCELLEME (UPDATE)
                    {
                        sorgu = "UPDATE Musteriler SET AdSoyad = @adSoyad, Telefon = @telefon, Adres = @adres WHERE MusteriID = @id";
                        komut = new SQLiteCommand(sorgu, baglanti);
                        komut.Parameters.AddWithValue("@id", seciliMusteriID.Value);
                    }

                    // Ortak parametreleri ata
                    komut.Parameters.AddWithValue("@adSoyad", txtAdSoyad.Text);
                    komut.Parameters.AddWithValue("@telefon", txtTelefon.Text);
                    komut.Parameters.AddWithValue("@adres", txtAdres.Text);
                    if (seciliMusteriID == null) // Sadece yeni kayıtta tarih ekle
                    {
                        komut.Parameters.AddWithValue("@kayitTarihi", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    }

                    komut.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("İşlem sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Hata olursa işlemi bitir.
            }

            // İşlem başarılıysa listeyi yenile ve formu temizle.
            MusterileriYukle();
            Temizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (seciliMusteriID == null)
            {
                MessageBox.Show("Lütfen silmek için listeden bir müşteri seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult cevap = MessageBox.Show($"'{txtAdSoyad.Text}' isimli müşteriyi kalıcı olarak silmek istediğinizden emin misiniz?",
                                                 "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap == DialogResult.Yes)
            {
                try
                {
                    using (var baglanti = new SQLiteConnection(Veritabani.BaglantiCumlesi))
                    {
                        baglanti.Open();
                        string sorgu = "DELETE FROM Musteriler WHERE MusteriID = @id";
                        using (var komut = new SQLiteCommand(sorgu, baglanti))
                        {
                            komut.Parameters.AddWithValue("@id", seciliMusteriID.Value);
                            komut.ExecuteNonQuery();
                        }
                    }
                    MusterileriYukle();
                    Temizle();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Silme işlemi sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtTelefon_KeyPress(object sender, KeyPressEventArgs e)
        {
            
             // Backspace gibi bir kontrol karakteri DEĞİLSE...
              if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                 {
                // O tuşun TextBox'a yazılmasını engelle.
                e.Handled = true;
                     }
        }
    }
 }

