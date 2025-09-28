using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TEKMET
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void müşteriİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // FrmMusteriEkle formundan yeni bir örnek oluşturuyoruz.
            FrmMusteriEkle musteriFormu = new FrmMusteriEkle();

            // Formu, arkadaki formu kilitleyecek şekilde gösteriyoruz.
            musteriFormu.ShowDialog();
        }

        private void müsşteriHesapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMusteriHesap musteriHesap = new FrmMusteriHesap();
            musteriHesap.ShowDialog();
        }

        private void raporlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Windows'un standart hesap makinesi uygulamasını (calc.exe) başlatır.
                System.Diagnostics.Process.Start("calc.exe");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hesap makinesi başlatılırken bir hata oluştu: " + ex.Message,
                                "Hata",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void teklifToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTeklifOlustur musteriTeklif = new FrmTeklifOlustur();
            musteriTeklif.ShowDialog();

        }

        private void mÜŞTERİToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
