using MlsMarket.Helper;

namespace MlsMarket.Services
{
    public class JobReader
    {
        //"january", "february", "march", "april", "may", "june", "july", "august", "september", "october", "november", "december"
        private readonly string[] MonthsList = { "january", "february", "march", "april", "may", "june", "july", "august", "september", "october", "november", "december" };
        private readonly int[] YearsList = { 2013, 2014, 2015, 2016, 2017, 2018, 2019, 2020, 2021, 2022, 2023 };

        public void StartJobs()
        {
            foreach (var year in YearsList)
            {
                foreach (var month in MonthsList)
                {
                    Task.Delay(3000).Wait();
                    StaticVariables.Month = month;
                    StaticVariables.Years = year;
                    PdfDownload download = new PdfDownload(month, year);
                    string path = download.Download();
                    
                    if (path == "Exist")
                    {
                        Console.WriteLine($"Mls Report {month} {year} Already Exist!!!");
                    }
                    else if (path == "Not Found Path" || path == "Error")
                    {
                        Console.WriteLine($"{month} {year} Not Found!!!");
                    }
                    else
                    {
                        PdfReading pdf = new PdfReading(path);
                        pdf.GetPDF();
                        PdfInsert insert = new PdfInsert(pdf.Markets);
                        int result = insert.InsertPdfToDatabase();
                        Console.WriteLine($"!!!!!!!!!Added {result} Mls Report {month} {year} To Database!!!");
                    }
                }

            }
        }
    }
}
