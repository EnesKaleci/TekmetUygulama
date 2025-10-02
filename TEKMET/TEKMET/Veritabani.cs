using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;

namespace TEKMET
{
    public static class Veritabani
    {
        public static string BaglantiCumlesi { get; private set; }

        public static void YoluAyarla(string dosyaYolu)
        {
            BaglantiCumlesi = $"Data Source={dosyaYolu};Version=3;";
        }

        public static void VeritabaniOlustur(string dosyaYolu)
        {
            try
            {
                SQLiteConnection.CreateFile(dosyaYolu);
                YoluAyarla(dosyaYolu);

                using (var baglanti = new SQLiteConnection(BaglantiCumlesi))
                {
                    baglanti.Open();

                    // YENİ VE GÜNCELLENMİŞ SQL KOMUTLARI
                    string sqlKomutlari = @"
    CREATE TABLE Musteriler (
        MusteriID    INTEGER PRIMARY KEY AUTOINCREMENT,
        AdSoyad      TEXT NOT NULL,
        Telefon      TEXT,
        Adres        TEXT,
        KayitTarihi  TEXT NOT NULL
    );

    CREATE TABLE Isler (
        IsID         INTEGER PRIMARY KEY AUTOINCREMENT,
        MusteriID    INTEGER NOT NULL,
        IsTanimi     TEXT,
        IsTarihi     TEXT,
        Fiyat        REAL,
        FOREIGN KEY(MusteriID) REFERENCES Musteriler(MusteriID)
    );

    CREATE TABLE Odemeler (
        OdemeID      INTEGER PRIMARY KEY AUTOINCREMENT,
        IsID         INTEGER NOT NULL,
        OdemeTarihi  TEXT,
        OdemeMiktari REAL,
        Aciklama     TEXT,
        FOREIGN KEY(IsID) REFERENCES Isler(IsID)
    );

    -- YENİ EKLENEN TEKLİF TABLOLARI --
    CREATE TABLE Teklifler (
    TeklifID        INTEGER PRIMARY KEY AUTOINCREMENT,
    MusteriID       INTEGER NOT NULL,
    TeklifAdi       TEXT,   -- YENİ EKLENDİ
    TeklifTarihi    TEXT,
    AraToplam       REAL,
    KDV             REAL,
    GenelToplam     REAL,
    Notlar          TEXT,
    FOREIGN KEY(MusteriID) REFERENCES Musteriler(MusteriID)
);
CREATE TABLE Calisanlar (
        CalisanID   INTEGER PRIMARY KEY AUTOINCREMENT,
        AdSoyad     TEXT NOT NULL,
        Telefon     TEXT
    );
CREATE TABLE Giderler (
        GiderID     INTEGER PRIMARY KEY AUTOINCREMENT,
        CalisanID   INTEGER,
        Aciklama    TEXT,
        Tutar       REAL,
        Tarih       TEXT,
        FOREIGN KEY(CalisanID) REFERENCES Calisanlar(CalisanID)
    );
    CREATE TABLE TeklifKalemleri (
        KalemID         INTEGER PRIMARY KEY AUTOINCREMENT,
        TeklifID        INTEGER NOT NULL,
        Aciklama        TEXT,
        Miktar          REAL,
        Birim           TEXT,
        BirimFiyat      REAL,
        ToplamFiyat     REAL,
        FOREIGN KEY(TeklifID) REFERENCES Teklifler(TeklifID)
    );";

                    using (var komut = new SQLiteCommand(sqlKomutlari, baglanti))
                    {
                        komut.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kritik Hata! Tablolar oluşturulamadı: " + ex.Message, "Veritabanı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

