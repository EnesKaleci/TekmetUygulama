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
    public partial class FrmCalisanlar : Form
    {
        private int? seciliCalisanID = null;
        public FrmCalisanlar()
        {
            InitializeComponent();
            this.dgvCalisanlar.RowHeadersVisible = false;
            Yükle();


        }
        private void Yükle()
        {
            // 'using' blokları, veritabanı bağlantısının işi bittiğinde
            // otomatik ve güvenli bir şekilde kapatılmasını sağlar.
            using (var baglanti = new SQLiteConnection(Veritabani.BaglantiCumlesi))
            {
                // Calisanlar tablosundaki tüm verileri, AdSoyad'a göre alfabetik sıralayarak seçen sorgu.
                string sorgu = "SELECT CalisanID, AdSoyad, Telefon FROM Calisanlar ORDER BY AdSoyad";

                // SQLiteDataAdapter, veritabanı ile C# arasında bir köprü görevi görür.
                using (var da = new SQLiteDataAdapter(sorgu, baglanti))
                {
                    // Verileri içinde tutacağımız sanal bir tablo oluşturuyoruz.
                    var dt = new DataTable();

                    // Sorgudan gelen sonuçları sanal tablomuzun içine dolduruyoruz.
                    da.Fill(dt);

                    // Sanal tabloyu, ekrandaki DataGridView'in veri kaynağı olarak atıyoruz.
                    // Bu satır sayesinde veriler ekranda görünür hale gelir.
                    dgvCalisanlar.DataSource = dt;
                }
            }

            // Tablonun daha profesyonel görünmesi için bazı sütun ayarları yapıyoruz.
            if (dgvCalisanlar.Columns.Count > 0)
            {
                // Kullanıcının görmesine gerek olmayan ID kolonunu gizliyoruz.
                dgvCalisanlar.Columns["CalisanID"].Visible = false;

                // Sütun başlığını daha okunaklı hale getiriyoruz.
                dgvCalisanlar.Columns["AdSoyad"].HeaderText = "Ad Soyad";

                // Kolonların genişliğini otomatik olarak ayarla.
                dgvCalisanlar.AutoSizeColumnsMode = (DataGridViewAutoSizeColumnsMode)DataGridViewAutoSizeColumnMode.Fill;
            }
        }
        private void CalisanlariYukle()
        {
            using (var baglanti = new SQLiteConnection(Veritabani.BaglantiCumlesi))
            {
                string sorgu = "SELECT CalisanID, AdSoyad, Telefon FROM Calisanlar ORDER BY AdSoyad";
                using (var da = new SQLiteDataAdapter(sorgu, baglanti))
                {
                    var dt = new DataTable();
                    da.Fill(dt);
                    dgvCalisanlar.DataSource = dt;
                }
            }
            if (dgvCalisanlar.Columns.Count > 0)
            {
                dgvCalisanlar.Columns["CalisanID"].Visible = false;
                dgvCalisanlar.Columns["AdSoyad"].HeaderText = "Ad Soyad";
                dgvCalisanlar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void txtAdSoyad_TextChanged(object sender, EventArgs e)
        {

        }

        private void FrmCalisanlar_Load(object sender, EventArgs e)
        {
           
        }

        private void dgvCalisanlar_SelectionChanged(object sender, EventArgs e)
        {
            // 1. Kontrol: Geçerli bir satır seçili mi?
            if (dgvCalisanlar.CurrentRow == null)
            {
                Temizle(); // Eğer seçili satır yoksa, formu temizle ve çık.
                return;
            }

            // 2. Kontrol: Satırın ID hücresi boş (DBNull) mu?
            // Bu, özellikle boş alana tıklandığında hatayı önler.
            if (dgvCalisanlar.CurrentRow.Cells["CalisanID"].Value is DBNull)
            {
                Temizle(); // Hatalı bir seçimse, formu temizle ve çık.
                return;
            }

            // Tüm kontrollerden geçtiyse, verileri güvenle oku.
            try
            {
                seciliCalisanID = Convert.ToInt32(dgvCalisanlar.CurrentRow.Cells["CalisanID"].Value);

                // Güvenli GetText() metodumuzu kullanarak verileri okuyoruz.
                txtAdSoyad.Text = GetText(dgvCalisanlar.CurrentRow.Cells["AdSoyad"].Value);
                txtTelefon.Text = GetText(dgvCalisanlar.CurrentRow.Cells["Telefon"].Value);

                btnSil.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri okunurken beklenmedik bir hata oluştu: " + ex.Message);
                Temizle();
            }
        }
        private string GetText(object value)
        {
            return value == DBNull.Value ? "" : value.ToString();
        }
        private void Temizle()
        {
            // Olayı geçici olarak devre dışı bırak ki ClearSelection() onu tetiklemesin.
            dgvCalisanlar.SelectionChanged -= dgvCalisanlar_SelectionChanged;

            // --- Temizleme işlemleri ---
            seciliCalisanID = null;         // En önemlisi: ID'yi sıfırla.
            txtAdSoyad.Clear();
            txtTelefon.Clear();
            dgvCalisanlar.ClearSelection(); // Tablodaki seçimi kaldır.
            btnSil.Enabled = false;
            txtAdSoyad.Focus();
            // -------------------------

            // Olayı tekrar aktif hale getir.
            dgvCalisanlar.SelectionChanged += dgvCalisanlar_SelectionChanged;
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAdSoyad.Text))
            {
                MessageBox.Show("Ad Soyad alanı boş bırakılamaz!", "Uyarı");
                return;
            }
            if (!this.txtTelefon.MaskCompleted && !string.IsNullOrEmpty(this.txtTelefon.Text.Replace("(", "").Replace(")", "").Replace(" ", "")))
            {
                MessageBox.Show("Lütfen telefon numarasını 11 hane olarak eksiksiz girin.", "Uyarı");
                return;
            }

            try
            {
                using (var baglanti = new SQLiteConnection(Veritabani.BaglantiCumlesi))
                {
                    baglanti.Open();
                    string sorgu;
                    SQLiteCommand komut;

                    if (seciliCalisanID == null) // Yeni Kayıt
                    {
                        sorgu = "INSERT INTO Calisanlar (AdSoyad, Telefon) VALUES (@adSoyad, @telefon)";
                        komut = new SQLiteCommand(sorgu, baglanti);
                    }
                    else // Güncelleme
                    {
                        sorgu = "UPDATE Calisanlar SET AdSoyad = @adSoyad, Telefon = @telefon WHERE CalisanID = @id";
                        komut = new SQLiteCommand(sorgu, baglanti);
                        komut.Parameters.AddWithValue("@id", seciliCalisanID.Value);
                    }
                    komut.Parameters.AddWithValue("@adSoyad", txtAdSoyad.Text);
                    komut.Parameters.AddWithValue("@telefon", txtTelefon.Text);
                    komut.ExecuteNonQuery();
                }
                CalisanlariYukle();
                Temizle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("İşlem sırasında bir hata oluştu: " + ex.Message, "Hata");
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (seciliCalisanID == null) return;
            DialogResult cevap = MessageBox.Show($"'{txtAdSoyad.Text}' isimli çalışanı silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap == DialogResult.Yes)
            {
                try
                {
                    using (var baglanti = new SQLiteConnection(Veritabani.BaglantiCumlesi))
                    {
                        baglanti.Open();
                        string sorgu = "DELETE FROM Calisanlar WHERE CalisanID = @id";
                        using (var komut = new SQLiteCommand(sorgu, baglanti))
                        {
                            komut.Parameters.AddWithValue("@id", seciliCalisanID.Value);
                            komut.ExecuteNonQuery();
                        }
                    }
                    CalisanlariYukle();
                    Temizle();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Silme işlemi sırasında bir hata oluştu: " + ex.Message, "Hata");
                }
            }
        }
    }
}
