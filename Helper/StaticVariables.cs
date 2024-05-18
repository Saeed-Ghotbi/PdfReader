using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MlsMarket.Helper
{
    public static class StaticVariables
    {
        private static string[] Time { get; set; } = DateTime.Now.AddMonths(-1).ToString("MMMM yyyy").Split(" ");

        public static string PathDirectory { get; set; } = AppDomain.CurrentDomain.BaseDirectory + "FileDownload\\";
        public static string Month { get; set; } = Time.First().ToLower();
        public static int Years { get; set; } = int.Parse(Time.Last());

    }
}
