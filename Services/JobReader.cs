using MlsMarket.Helper;

namespace MlsMarket.Services
{
    public class JobReader
    {
        
        public void StartJobs()
        {

            Task.Delay(3000).Wait();
            PdfDownload download = new PdfDownload(StaticVariables.Month, StaticVariables.Years);
            string path = download.Download();
            //string path = AppDomain.CurrentDomain.BaseDirectory + "FileDownload\\" + year + $"\\Mls-Report-{month}.pdf";
            if (path == "Exist")
            {
                Console.WriteLine($"Mls Report {StaticVariables.Month} {StaticVariables.Years} Already Exist!!!");
            }
            else if (path == "Not Found Path" || path == "Error")
            {
                Console.WriteLine($"{StaticVariables.Month} {StaticVariables.Years} Not Found!!!");
            }
            else
            {
                PdfReading pdf = new PdfReading(path);
                pdf.GetPDF();
                PdfInsert insert = new PdfInsert(pdf.Markets);
                int result = insert.InsertPdfToDatabase();
                Console.WriteLine($"!!!!!!!!!Added {result} Mls Report {StaticVariables.Month} {StaticVariables.Years} To Database!!!");
            }

        }
    }
}
