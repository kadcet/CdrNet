using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CdrNet.Business.Exceptions
{
    public class YeterliStokYokException:Exception
    {
        public YeterliStokYokException(string urunAdi,ushort stok,ushort dusulecekStok)
            :base($"{urunAdi} isimli üründen stokta {stok} adet var.{dusulecekStok} kadar mevcut değil")
        {

        }
    }
}
