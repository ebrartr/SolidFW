using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums
{
    public enum AuthLevel
    {
        /// <summary>
        /// <para>Tr: yetkili olunan metot sisimleri jwt den alınacaksa</para>
        /// <para>En : get metot names from jwt</para>
        /// </summary>
        WithJwt,

        WithDb
    }
}