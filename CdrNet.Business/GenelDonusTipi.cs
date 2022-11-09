using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CdrNet.Business
{
    public class GenelDonusTipi
    {
        public bool hataVarMi;
        public string hataMesaji;
        public object data;

        // ****** this ten sonra gelmesini istediğimiz parametreleri yazıyoruz****************

        // hata vardır ama hata mesajı yoktur
        public GenelDonusTipi(bool hataVarMi) : this(hataVarMi, null, null)
        {

        }
        // başka versiyon => hata yoksa data nın içini okuyabilirim 
        public GenelDonusTipi(bool hataVarMi, object data) : this(hataVarMi, null, data)
        {

        }

        // başka versiyon => hata varsa hata mesajını ver. Object tipinden datayı gönderme
        public GenelDonusTipi(bool hataVarMi, string hataMesaji):this(hataVarMi,hataMesaji,null)
        {
            
        }

        public GenelDonusTipi(bool hataVarMi, string hataMesaji, object data)
        {
            this.hataVarMi = hataVarMi;
            this.hataMesaji = hataMesaji;
            this.data = data;
        }
    }
}
