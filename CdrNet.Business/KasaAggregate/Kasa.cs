using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CdrNet.Business.KasaAggregate
{
    public class Kasa
    {
        public IslemTipi islemTipi;
        public DateTime tarih;
        public string aciklama;
        public double tutar;

        public Kasa(IslemTipi islemTipi, DateTime tarih, string aciklama, double tutar)
        {
            // validation

            this.islemTipi = islemTipi;
            this.tarih = tarih;
            this.aciklama = aciklama;
            this.tutar = tutar;
        }
    }
}
