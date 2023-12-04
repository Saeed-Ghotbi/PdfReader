using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using MlsMarket.Entity;
using MlsMarket.Helper;
using System.Text.RegularExpressions;

namespace MlsMarket.Services
{
    public class PdfReading
    {
        private readonly string _PathPdf;
        public List<MlsMarketEntity> Markets { get; set; } = new List<MlsMarketEntity>();
        public PdfReading(string path)
        {
            _PathPdf = path;
        }
        public void GetPDF()
        {
            if (_PathPdf == "")
                return;
            using PdfReader reader = new PdfReader(_PathPdf);
            Console.WriteLine("Start Pdf Reader");
            string[] Propertytype = new[] { "Residential / Composite", "Single Family Detached", "Townhouse", "Apartment" };

            Regex regexHeader = new Regex(@"(Property\sType\sArea|Benchmark\s|Price|Index|Change\s%|Year|3\sMonth\s|1\sMonth|6\sMonth|1\sYear|3\sYear|5\sYear)");
            Regex regexFindPropertyType = new Regex(@"(Residential\s\/\sComposite|Single\sFamily\sDetached|Townhouse|Apartment)\s");
            Regex regexFindArea = new Regex(@"([A-Za-z]+\s+[A-Za-z]|[A-Za-z])+\s");
            Regex regexFindTablePage = new Regex(@"Property\sType\sArea\s");
            int indexPropertyTable = -1;
            int indexPdfPage = 0;

            for (int i = 3; i < 6; i++)
            {
                ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                string pageText = PdfTextExtractor.GetTextFromPage(reader, i, strategy);
                if (pageText.Length != 0)
                {
                    if (!regexFindTablePage.IsMatch(pageText))
                        continue;


                    int endIndex = pageText.IndexOf("HOW");

                    string text1 = pageText.Substring(0, endIndex);

                    string matchesRemoveHeader = regexHeader.Replace(text1, string.Empty);

                    string[] rowText = matchesRemoveHeader.Split("\n");

                    foreach (var row in rowText)
                    {

                        if (row.Length > 20)
                        {
                            string result = "";
                            string[] resultArray;
                            if (regexFindPropertyType.IsMatch(row))
                            {
                                result = regexFindPropertyType.Replace(row, string.Empty);
                                indexPropertyTable++;
                            }
                            else
                            {
                                result = row;
                            }

                            if (!regexFindArea.IsMatch(result))
                                return;


                            resultArray = regexFindArea.Replace(result, string.Empty).Split(" ");

                            MlsMarketEntity market = new MlsMarketEntity()
                            {
                                PropertyType = Propertytype[indexPropertyTable],
                                Area = regexFindArea.Match(result).Value,
                                BenchmarkPrice = resultArray.ElementAtOrDefault(0),
                                PriceIndex = resultArray.ElementAtOrDefault(1),
                                OneMonthChange = resultArray.ElementAtOrDefault(2),
                                ThreeMonthChange = resultArray.ElementAtOrDefault(3),
                                SixMonthChange = resultArray.ElementAtOrDefault(4),
                                OneYearChange = resultArray.ElementAtOrDefault(5),
                                ThreeYearChange = resultArray.ElementAtOrDefault(6),
                                FiveYearChange = resultArray.ElementAtOrDefault(7),
                                TenYearChange = resultArray.ElementAtOrDefault(8),
                                Date = DateTime.Parse(StaticVariables.Years + StaticVariables.Month)
                            };
                            Markets.Add(market);
                        }
                    }
                }
            }
        }
    }
}
