using HtmlAgilityPack;
using MlsMarket.Helper;


namespace MlsMarket.Services
{
    public class PdfDownload
    {
        private readonly string _Month;
        public readonly int _Years;

        public PdfDownload(string month, int year)
        {
            _Month = month;
            _Years = year;

        }
        static readonly string _baseUrl = "https://www.rebgv.org/market-watch/monthly-market-report/";
        public string GenerateUrl()
        {
            string pagePath = _Month + "-" + _Years + ".html";

            return _baseUrl + pagePath;
        }


        public string GetPath()
        {
            System.Net.WebClient httpClient = new System.Net.WebClient();
            string response = "";
            try
            {
                response = httpClient.DownloadString(GenerateUrl());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "";
            }
            var htmlDocument = new HtmlDocument();

            htmlDocument.LoadHtml(response);


            var pdfLinks = htmlDocument.DocumentNode.SelectNodes("//a[contains(@href,'.pdf')]");

            if (pdfLinks != null)
            {
                foreach (var pdfLink in pdfLinks)
                {
                    string pdfUrl = pdfLink.GetAttributeValue("href", "");
                    return pdfUrl;
                }
            }
            return "";
        }

        public string Download()
        {
            System.Net.WebClient httpClient = new System.Net.WebClient();
            Console.WriteLine("Start Download");
            string pathDownload = GetPath();
            
            if (pathDownload != "")
            {

                if (!Directory.Exists(StaticVariables.PathDirectory + _Years))
                {
                    Directory.CreateDirectory(StaticVariables.PathDirectory + _Years);
                }
                if (File.Exists($"{StaticVariables.PathDirectory}{_Years}\\Mls-Report-{_Month}.pdf"))
                    return "Exist";
                try
                {
                    Console.WriteLine("Get Paths Download:" + (pathDownload.Contains("http") ? pathDownload : "https://www.rebgv.org" + pathDownload));
                    httpClient.DownloadFile(pathDownload.Contains("http") ? pathDownload : "https://www.rebgv.org" + pathDownload, $"{StaticVariables.PathDirectory}{_Years}\\Mls-Report-{_Month}.pdf");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return "Error";
                }

                if (File.Exists($"{StaticVariables.PathDirectory}{_Years}\\Mls-Report-{_Month}.pdf"))
                    return Path.GetFullPath($"{StaticVariables.PathDirectory}{_Years}\\Mls-Report-{_Month}.pdf");
            }
            return "Not Found Path";

        }

    }
}
