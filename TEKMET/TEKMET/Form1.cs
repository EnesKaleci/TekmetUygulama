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
      
        }

        private void teklifToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTeklifOlustur musteriTeklif = new FrmTeklifOlustur();
            musteriTeklif.ShowDialog();

        }

        private void mÜŞTERİToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void hesapMakinesiToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void takvimToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Windows Takvim uygulamasını başlatan özel protokol komutudur.
                System.Diagnostics.Process.Start("notepad.exe");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Windows Takvim uygulaması başlatılırken bir hata oluştu. Uygulama bilgisayarınızda yüklü olmayabilir.\n\nHata: " + ex.Message,
                                "Hata",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void yapışkanNotlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Windows 10 ve 11'deki Yapışkan Notlar uygulamasını başlatan özel protokol komutu.
                System.Diagnostics.Process.Start("https://eneskaleci.com");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Yapışkan Notlar uygulaması başlatılırken bir hata oluştu. Uygulama bilgisayarınızda yüklü olmayabilir.\n\nHata: " + ex.Message,
                                "Hata",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void çalışanlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCalisanlar calısanlar = new FrmCalisanlar();
            calısanlar.ShowDialog();
            

        }

        private void giderlerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmGiderler frm = new FrmGiderler();
            frm.ShowDialog();
        }
    }
    }

