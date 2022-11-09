using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CdrNet.Business.Exceptions
{
    public class UrunBulunamadiException:Exception
    {
        public UrunBulunamadiException(string urunAdi):base($"{urunAdi} isimli ürün bulunamadı")
        {

        }
    }
}
