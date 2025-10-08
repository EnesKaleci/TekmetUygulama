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
    public partial class FrmYeniOdeme : Form
    {// Bu ödemenin ekleneceği işin ID'sini tutmak için.
        private readonly int isID;
        public FrmYeniOdeme(int isID)
        {
            InitializeComponent();
            this.isID = isID;
        }

        private void FrmYeniOdeme_Load(object sender, EventArgs e)
        {

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (nudOdemeMiktari.Value <= 0)
            {
                MessageBox.Show("Ödeme miktarı 0'dan büyük olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var baglanti = new SQLiteConnection(Veritabani.BaglantiCumlesi))
                {
                    baglanti.Open();
                    string sorgu = "INSERT INTO Odemeler (IsID, OdemeTarihi, OdemeMiktari, Aciklama) VALUES (@isID, @odemeTarihi, @odemeMiktari, @aciklama)";
                    using (var komut = new SQLiteCommand(sorgu, baglanti))
                    {
                        komut.Parameters.AddWithValue("@isID", this.isID);
                        komut.Parameters.AddWithValue("@odemeTarihi", dtpOdemeTarihi.Value.ToString("yyyy-MM-dd"));
                        komut.Parameters.AddWithValue("@odemeMiktari", nudOdemeMiktari.Value);
                        komut.Parameters.AddWithValue("@aciklama", txtAciklama.Text);
                        komut.ExecuteNonQuery();
                    }
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ödeme kaydedilirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
