using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CdrNet.Business.Exceptions
{
    public class UrunZatenKayitliException : Exception
    {
        public UrunZatenKayitliException(string urunAdi) : base($"{urunAdi} isimli ürün zaten kayıtlı")
        {

        }
    }
}
