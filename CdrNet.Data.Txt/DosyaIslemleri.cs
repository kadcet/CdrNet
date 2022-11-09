using System.Text;

namespace CdrNet.Data.Txt
{
    public class DosyaIslemleri
    {
        // ürünü-kasayı kaydettiğim başka txt dosyaları olacak
        public static void Kaydet(string dosyaYolu,string icerik)
        {
            File.WriteAllText(dosyaYolu, icerik);
        }
        public static string Oku(string dosyaYolu)
        {
            try
            {
                return File.ReadAllText(dosyaYolu);
            }
            catch (Exception ex)
            {
                // hata çıkarsa handling et. Exception gelirse bunu yakala ve throw et fırlat

                StringBuilder sb=new StringBuilder();
                sb.Append("**********************");
                sb.Append($"Log Zamanı :{DateTime.Now}\r\n");
                sb.Append($"Hata Mesajı : {ex.Message}\r\n");
                sb.Append($"Dosya Yolu : {dosyaYolu}\r\n");
                sb.Append("**********************");
                File.AppendAllText("log.txt", sb.ToString());
                // burda bir hata olduğunu farketsin istiyorum. Üstte loglama yaptık sadece
                throw new DosyaBulunamadiException(dosyaYolu);
                //throw new Exception("dosya bulunamadı");

            }
            
        }
    }
}