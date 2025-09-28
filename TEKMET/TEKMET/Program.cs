using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TEKMET.Properties;
using System.IO;

namespace TEKMET
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string kayitliYol = Settings.Default.DatabasePath;

            if (string.IsNullOrEmpty(kayitliYol) || !File.Exists(kayitliYol))
            {
                DialogResult cevap = MessageBox.Show("Uygulama için bir veritabanı dosyası bulunamadı.\n\nYeni bir veritabanı oluşturmak için 'Evet'e,\nMevcut bir veritabanını seçmek için 'Hayır'a tıklayın.", "Veritabanı Kurulumu", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                string secilenYol = "";

                if (cevap == DialogResult.Yes)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Title = "Yeni Veritabanı Oluştur";
                    sfd.Filter = "Veritabanı Dosyası (*.db)|*.db";
                    sfd.FileName = "TekMetVeritabani.db";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        secilenYol = sfd.FileName;
                        Veritabani.VeritabaniOlustur(secilenYol);
                    }
                }
                else if (cevap == DialogResult.No)
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Title = "Mevcut Veritabanını Seç";
                    ofd.Filter = "Veritabanı Dosyası (*.db)|*.db";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        secilenYol = ofd.FileName;
                    }
                }
                else
                {
                    return;
                }

                if (string.IsNullOrEmpty(secilenYol))
                {
                    return;
                }

                Settings.Default.DatabasePath = secilenYol;
                Settings.Default.Save();
                Veritabani.YoluAyarla(secilenYol);
            }
            else
            {
                Veritabani.YoluAyarla(kayitliYol);
            }

            Application.Run(new Form1());
        }
    }
}
