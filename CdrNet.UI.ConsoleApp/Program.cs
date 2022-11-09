
using CdrNet.Business;
using System.ComponentModel.Design;

public class prgram
{
    public static void Main()
    {
        Menu();
    }

    private static void Menu()
    {
        Console.Clear();
        var secim = CevapAl("1. Ürün Ekle\n2. Ürün Sat\n3.Kasa Durumu\n4.Çıkış", false);

        switch (secim)
        {
            case "1":
                UrunEkle();
                break;
            case "2":
                UrunSat();
                break;
            case "3":
                KasaDurumu();
                break;
            case "4":
                Environment.Exit(0);
                break;
            default:
                MenuyeDonus("Hatalı bir seçim yaptınız. Tekrar denemek için ENTER");
                break;
        }
    }

    

    private static void KasaDurumu()
    {
        UygulamaServisi servis = new UygulamaServisi();
        var bakiye = servis.KasaBakiyesi();
        var liste = servis.KasaListesi();

        Console.WriteLine($"Tarih\t\t\tTutar\t\tAçıklama");

        foreach (var k in liste)
        {
            Console.WriteLine($"{k.tarih.ToShortDateString()}\t\t{k.tutar}\t\t{k.aciklama}");
        }
        Console.WriteLine("güncel kasa bakiyesi :" + bakiye);
        MenuyeDonus();
    }

    private static void UrunSat()
    {
        UygulamaServisi servis = new UygulamaServisi();
        string urunAdi = CevapAl("Ürün adı :");
        ushort adet = Convert.ToUInt16(CevapAl("Satış Adedi : "));
        GenelDonusTipi sonuc = servis.SatisYap(urunAdi, adet);

        if (sonuc.hataVarMi)
        {
            Console.WriteLine(sonuc.hataMesaji);
            TekrarDeneme();
            UrunSat();
            return;
        }
        MenuyeDonus();   
    }
     
    

    private static void TekrarDeneme()
    {
        Console.WriteLine("Tekrar denemek için ENTER");
        Console.ReadLine();
    }

    private static void UrunEkle()
    {
        Console.Clear();
        string urunAdi = CevapAl("Ürün Adı : ");
        double alisFiyati = Convert.ToDouble(CevapAl("Ürün alış fiyatı :"));
        double satisFiyati = Convert.ToDouble(CevapAl("Ürün satış fiyatı :"));
        ushort stok = Convert.ToUInt16(CevapAl("Ürün adedi :"));

        GenelDonusTipi sonuc = new UygulamaServisi().UrunEkle(urunAdi, alisFiyati, satisFiyati, stok);

        if (sonuc.hataVarMi)
        {
            Console.WriteLine(sonuc.hataMesaji);
            TekrarDeneme();
            UrunEkle();
            return;
        }
        MenuyeDonus();
    }

    private static string CevapAl(string ekranMetni,bool ayniSatirdami=true)
    {
        if(ayniSatirdami)
            Console.Write(ekranMetni);
        else
            Console.WriteLine(ekranMetni);
        return Console.ReadLine();
    }

    private static void MenuyeDonus(string mesaj="Menüye dönmek için ENTER.")
    {
        Console.WriteLine(mesaj);
        Console.ReadLine();
        Menu();
    }
}
