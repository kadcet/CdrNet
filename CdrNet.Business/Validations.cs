using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CdrNet.Business
{
    public static class Validations
    {
        public static void NullOrEmpty(this string value,string paramName)
        {
            // sistemdeki exception u throw ediyorum manuel olarak
            if(string.IsNullOrEmpty(value))
                throw new ArgumentNullException(paramName,$"{paramName} değeri boş yada null olamaz");
        }

        public static void Zero(this double value,string paramName)
        {
            if (0 == value)
                throw new ArgumentException(paramName, $"{paramName} değeri 0 olamaz");
        }

        public static void Zero(this ushort value, string paramName)
        {
            if (0 == value)
                throw new ArgumentException(paramName, $"{paramName} değeri 0 olamaz");
        }
    }
}
