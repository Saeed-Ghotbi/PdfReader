using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MlsMarket.Helper
{
    public static class StaticVariables
    {
      
        public static string PathDirectory { get; set; } = AppDomain.CurrentDomain.BaseDirectory + "FileDownload\\";
        public static string Month { get; set; }
        public static int Years { get; set; }
    }
}
