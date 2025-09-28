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
    public partial class FrmYeniIs : Form
    {
        // Bu işin ekleneceği müşterinin ID'sini tutmak için.
        private readonly int musteriID;
        private readonly int? isID;
        public FrmYeniIs(int musteriID, int? isID = null)
        {
            InitializeComponent();
            this.musteriID = musteriID;
            this.isID = isID;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void FrmYeniIs_Load(object sender, EventArgs e)
        {
            // Eğer isID null ise, bu YENİ KAYIT modudur.
            if (isID == null)
            {
                this.Text = "Yeni İş Ekle";
                btnSil.Enabled = false; // Yeni kayıtta Sil butonu pasif olmalı.
            }
            // Eğer isID bir değere sahipse, bu DÜZENLEME modudur.
            else
            {
                this.Text = "İş Bilgilerini Düzenle";
                btnSil.Enabled = true; // Düzenlemede Sil butonu aktif.
                BilgileriDoldur();
            }
        }
        private void BilgileriDoldur()
        {
            try
            {
                using (var baglanti = new SQLiteConnection(Veritabani.BaglantiCumlesi))
                {
                    baglanti.Open();
                    string sorgu = "SELECT * FROM Isler WHERE IsID = @id";
                    using (var komut = new SQLiteCommand(sorgu, baglanti))
                    {
                        komut.Parameters.AddWithValue("@id", this.isID.Value);
                        using (var okuyucu = komut.ExecuteReader())
                        {
                            if (okuyucu.Read())
                            {
                                txtIsTanimi.Text = okuyucu["IsTanimi"].ToString();
                                dtpIsTarihi.Value = DateTime.Parse(okuyucu["IsTarihi"].ToString());
                                nudFiyat.Value = Convert.ToDecimal(okuyucu["Fiyat"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("İş bilgileri yüklenirken hata oluştu: " + ex.Message);
                this.Close();
            }
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIsTanimi.Text) || nudFiyat.Value <= 0)
            {
                MessageBox.Show("İş tanımı ve fiyat alanları doğru şekilde doldurulmalıdır.", "Uyarı");
                return;
            }

            try
            {
                using (var baglanti = new SQLiteConnection(Veritabani.BaglantiCumlesi))
                {
                    baglanti.Open();
                    string sorgu;
                    SQLiteCommand komut;

                    if (isID == null) // isID boşsa, YENİ KAYIT (INSERT)
                    {
                        sorgu = "INSERT INTO Isler (MusteriID, IsTanimi, IsTarihi, Fiyat) VALUES (@musteriID, @isTanimi, @isTarihi, @fiyat)";
                        komut = new SQLiteCommand(sorgu, baglanti);
                    }
                    else // isID doluysa, GÜNCELLEME (UPDATE)
                    {
                        sorgu = "UPDATE Isler SET IsTanimi = @isTanimi, IsTarihi = @isTarihi, Fiyat = @fiyat WHERE IsID = @id";
                        komut = new SQLiteCommand(sorgu, baglanti);
                        komut.Parameters.AddWithValue("@id", this.isID.Value);
                    }

                    komut.Parameters.AddWithValue("@musteriID", this.musteriID);
                    komut.Parameters.AddWithValue("@isTanimi", txtIsTanimi.Text);
                    komut.Parameters.AddWithValue("@isTarihi", dtpIsTarihi.Value.ToString("yyyy-MM-dd"));
                    komut.Parameters.AddWithValue("@fiyat", nudFiyat.Value);
                    komut.ExecuteNonQuery();
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("İşlem sırasında bir hata oluştu: " + ex.Message, "Hata");
            }
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {   // Sil butonu sadece düzenleme modunda aktif olduğu için isID'nin dolu olduğunu biliyoruz.
            DialogResult cevap = MessageBox.Show("Bu iş kaydını ve bu işe ait TÜM ÖDEMELERİ kalıcı olarak silmek istediğinizden emin misiniz?\n\nBU İŞLEM GERİ ALINAMAZ!", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (cevap == DialogResult.Yes)
            {
                try
                {
                    using (var baglanti = new SQLiteConnection(Veritabani.BaglantiCumlesi))
                    {
                        baglanti.Open();
                        // Önce bu işe ait ödemeleri sil
                        string odemeSilSorgu = "DELETE FROM Odemeler WHERE IsID = @id";
                        using (var komut = new SQLiteCommand(odemeSilSorgu, baglanti))
                        {
                            komut.Parameters.AddWithValue("@id", this.isID.Value);
                            komut.ExecuteNonQuery();
                        }

                        // Sonra işin kendisini sil
                        string isSilSorgu = "DELETE FROM Isler WHERE IsID = @id";
                        using (var komut = new SQLiteCommand(isSilSorgu, baglanti))
                        {
                            komut.Parameters.AddWithValue("@id", this.isID.Value);
                            komut.ExecuteNonQuery();
                        }
                    }
                    this.DialogResult = DialogResult.OK; // Ana listeyi yenilemesi için OK sonucu döndür
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Silme işlemi sırasında bir hata oluştu: " + ex.Message, "Hata");
                }
            }
        }

    }
    
}
