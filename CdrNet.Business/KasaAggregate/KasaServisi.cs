using CdrNet.Business.UrunAggregate;
using CdrNet.Data.Txt;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CdrNet.Business.KasaAggregate
{
    // sadece bussines en erişilecek.
    internal class KasaServisi
    {
        static KasaServisi()
        {
            KasaYukle();
        }

        private static List<Kasa> liste=new List<Kasa>();
        

        private static void KasaYukle()
        {
            string data = "";
            try
            {
                data = DosyaIslemleri.Oku(Sabitler.KASA_DOSYA_YOLU);
                liste = JsonSerializer.Deserialize<List<Kasa>>(data, new JsonSerializerOptions { IncludeFields = true });

            }
            catch (DosyaBulunamadiException ex)
            {
                DosyaIslemleri.Kaydet(Sabitler.KASA_DOSYA_YOLU, "");
            }
            catch (Exception ex)
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

        public GenelDonusTipi Kaydet(IslemTipi islemTipi,double tutar,string aciklama)
        {
            try
            {
                KasaYukle();
                liste.Add(new Kasa(islemTipi, DateTime.Now, aciklama, tutar));
                string json = JsonSerializer.Serialize(liste, new JsonSerializerOptions { IncludeFields = true });

                DosyaIslemleri.Kaydet(Sabitler.KASA_DOSYA_YOLU, json);
                return new GenelDonusTipi(false);
            }
            catch (Exception ex)
            {

                return new GenelDonusTipi(true, ex.Message);
            }
        }

        public IReadOnlyCollection<Kasa> KasaListesi()
        {
            return liste.AsReadOnly();
        }
        public IReadOnlyCollection<Kasa> GelirListesi()
        {
            return liste.Where(k => k.islemTipi == IslemTipi.Gelir).ToList().AsReadOnly();
        }

        public IReadOnlyCollection<Kasa> GiderListesi()
        {
            return liste.Where(k => k.islemTipi == IslemTipi.Gider).ToList().AsReadOnly();
        }

        public double Bakiye()
        {
            return GelirListesi().Sum(k => k.tutar) - GiderListesi().Sum(k => k.tutar);
        }


    }
}
