using CdrNet.Business.Exceptions;
using CdrNet.Data.Txt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CdrNet.Business.UrunAggregate
{
    // bu sınıf türetildiği anda belleğimize tüm ürünlerin yüklenmesi gerekir. Yapıcı metod üzerinde yürüycez. 
    // static yapıcaz ki yapıcı metod 1 kere çalışsın

    // sadece bussines en erişilecek.
    internal class UrunServisi
    {
        // burda dosyadan tüm ürünleri okuyup bir listeye yüklemek ve o liste üzerinde bellekte taşımak
        static UrunServisi()
        {
            UrunleriYukle();
        }
        private static List<Urun> liste=new List<Urun>();

        private static void UrunleriYukle()
        {
            string data = "";
            try
            {
                data = DosyaIslemleri.Oku(Sabitler.URUN_DOSYA_YOLU);
                liste = JsonSerializer.Deserialize<List<Urun>>(data, new JsonSerializerOptions { IncludeFields = true });
               
            }
            catch (DosyaBulunamadiException ex)
            {
                DosyaIslemleri.Kaydet(Sabitler.URUN_DOSYA_YOLU, "");
            }
            catch(Exception ex)
            {
                // Todo:loglama işlemi yapılabilir.
                // Todo:sisteme log altyapısı entegre edelim
                StringBuilder sb = new StringBuilder();
                sb.Append($"Log zamanı {DateTime.Now}");
                sb.Append($"Hata Mesajı {ex.Message}");
                sb.Append($"Dosya Yolu {Sabitler.URUN_DOSYA_YOLU}");
                File.AppendAllText(Sabitler.LOG_DOSYA_YOLU, sb.ToString());

                throw new DosyaBulunamadiException(Sabitler.URUN_DOSYA_YOLU);
                
            }
        }

        public GenelDonusTipi Kaydet(string urunAdi,double alisFiyati,double satisFiyati,ushort stok)
        {
            try
            {
                var urunVarmiDonusTipi = UrunVarMi(urunAdi);
                if (urunVarmiDonusTipi.hataVarMi)
                    return urunVarmiDonusTipi;

                bool urunVarmi = (bool)urunVarmiDonusTipi.data;
                if (urunVarmi)
                    throw new UrunZatenKayitliException(urunAdi);

                var urun = new Urun(urunAdi, alisFiyati, satisFiyati, stok);
                liste.Add(urun);

                string json = JsonSerializer.Serialize(liste, new JsonSerializerOptions { IncludeFields = true });
                DosyaIslemleri.Kaydet(Sabitler.URUN_DOSYA_YOLU,json);

                return new GenelDonusTipi(false, urun);
            }
            catch (Exception ex)
            {

                return new GenelDonusTipi(true, ex.Message);
            }
        }

        public IReadOnlyCollection<Urun> UrunListesi()
        {
            return liste.AsReadOnly();
        }

        public GenelDonusTipi StokDusur(string urunAdi,ushort dusulecekUrunAdedi)
        {
            if (dusulecekUrunAdedi == 0)
                return new GenelDonusTipi(true, "0 adet ürün satışı yapılamaz");

            var aramaSonucu = UrunAra(urunAdi);

            if (aramaSonucu.hataVarMi)
                return aramaSonucu;

            Urun urun = (Urun)aramaSonucu.data;

            try
            {
                if (urun.stokAdedi < dusulecekUrunAdedi)
                    throw new YeterliStokYokException(urunAdi, urun.stokAdedi, dusulecekUrunAdedi);

                liste.Remove(urun);
                urun.stokAdedi -= dusulecekUrunAdedi;
                return Kaydet(urun.urunAdi, urun.alisFiyati, urun.satisFiyati, urun.stokAdedi);
            }
            catch (Exception ex)
            {

                return new GenelDonusTipi(true, ex.Message);
            }
;
        }

        public GenelDonusTipi UrunAra(string urunAdi)
        {
            try
            {
                urunAdi.NullOrEmpty(nameof(urunAdi));
                var donusTipi = new GenelDonusTipi(false);

                donusTipi.data = liste.FirstOrDefault(u => u.urunAdi.ToLower() == urunAdi.ToLower());
                if (donusTipi.data == null)
                    throw new UrunBulunamadiException(urunAdi);

                return donusTipi;
            }
            catch (Exception ex)
            {

                return new GenelDonusTipi(true, ex.Message);
            }
        }

        public GenelDonusTipi UrunVarMi(string urunAdi)
        {
            try
            {
                urunAdi.NullOrEmpty(nameof(urunAdi));
                var donusTipi = new GenelDonusTipi(false);
                donusTipi.data= liste.Any(u => u.urunAdi.Contains(urunAdi));
                return donusTipi;
            }
            catch (Exception ex)
            {

                return new GenelDonusTipi(true, ex.Message);
            }
        }


    }
}
