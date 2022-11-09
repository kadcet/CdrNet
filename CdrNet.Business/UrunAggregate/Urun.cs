using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CdrNet.Business.UrunAggregate
{
    public class Urun
    {
        public string urunAdi;
        public double alisFiyati;
        public double satisFiyati;
        public ushort stokAdedi;

        
        public Urun(string _urunAdi, double _alisFiyati, double _satisFiyati, ushort _stokAdedi)
        {
            // nameof parantez içine verilen tipin ismini string olarak döner
            // nameof metoddaki papametre ismini değiştirirsem alttakide değişecek
            // newlerken değerleri alıcaz. Değerleri alırkende kontrol edicez. Gerekirse hatalar meydana gelsin
            // Validations ta yapıcaz
            _urunAdi.NullOrEmpty(nameof(_urunAdi)); // boş veya null gelirse
            //_urunAdi.NullOrEmpty("_urunAdi");
            _alisFiyati.Zero(nameof(_alisFiyati));
            _satisFiyati.Zero(nameof(_satisFiyati));
            _stokAdedi.Zero(nameof(_stokAdedi));


            urunAdi = _urunAdi;
            alisFiyati = _alisFiyati;
            satisFiyati = _satisFiyati;
            stokAdedi = _stokAdedi;
            
        }
    }
}
