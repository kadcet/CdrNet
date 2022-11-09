using CdrNet.Business.KasaAggregate;
using CdrNet.Business.UrunAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CdrNet.Business
{
    public  class UygulamaServisi
    {
        private UrunServisi urunServisi = new UrunServisi();
        private KasaServisi kasaServisi = new KasaServisi();

        public GenelDonusTipi UrunEkle(string urunAdi,double alisFiyati,double satisFiyati,ushort stok)
        {
            var urunKayitsonuc = urunServisi.Kaydet(urunAdi, alisFiyati, satisFiyati, stok);
            if (urunKayitsonuc.hataVarMi)
                return urunKayitsonuc;

            var kasaKayitSonuc = kasaServisi.Kaydet(IslemTipi.Gider, (alisFiyati * stok), $"{urunAdi} isimli üründen {stok} adet alındı.");
            if (kasaKayitSonuc.hataVarMi)
                return kasaKayitSonuc;

            return new GenelDonusTipi(false);
        }

        public GenelDonusTipi SatisYap(string urunAdi, ushort satisAdedi)
        {
            var stokDusurSonuc = urunServisi.StokDusur(urunAdi, satisAdedi);
            if (stokDusurSonuc.hataVarMi)
                return stokDusurSonuc;

            var urun = (Urun)stokDusurSonuc.data;
            var kasaKayitSonuc = kasaServisi.Kaydet(IslemTipi.Gelir, (urun.satisFiyati*satisAdedi), $"{urunAdi} isimli üründen {urun.satisFiyati} liradan {satisAdedi} adet satıldı");

            if (kasaKayitSonuc.hataVarMi)
                return kasaKayitSonuc;

            return new GenelDonusTipi(false);
        }

        public double KasaBakiyesi()
        {
            return kasaServisi.Bakiye();
        }
        public IReadOnlyCollection<Kasa> KasaListesi()
        {
            return kasaServisi.KasaListesi();
        }

    }
}
