using System.Data.SqlClient;
using Dapper;
using MlsMarket.Entity;

namespace MlsMarket.Services
{
    public class PdfInsert
    {
        private List<MlsMarketEntity> _MarketList { get; set; }

        public PdfInsert(List<MlsMarketEntity> market)
        {
            _MarketList = market;
        }
        public int InsertPdfToDatabase()
        {
            Console.WriteLine($"Start Insert {_MarketList.Count} To DataBase");

            string query = "";
            foreach (var market in _MarketList)
            {
                query += $@"
                    insert into MlsMarketReport (PropertyType,Area,BenchmarkPrice,PriceIndex,OneMonthChange,ThreeMonthChange,SixMonthChange,OneYearChange,ThreeYearChange,FiveYearChange,TenYearChange,Date) values
                    ('{market.PropertyType}','{market.Area}','{market.BenchmarkPrice}','{market.PriceIndex}','{market.OneMonthChange}','{market.ThreeMonthChange}','{market.SixMonthChange}','{market.OneYearChange}','{market.ThreeYearChange}','{market.FiveYearChange}','{market.TenYearChange}','{market.Date}');     
                ";
            }



            string connetionString;
            int rowAffected = 0;
            SqlConnection cnn;
            connetionString = @"Data Source=.;Initial Catalog=BestPresales;TrustServerCertificate=True;Integrated Security=True;";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            rowAffected = cnn.Execute(query);
            cnn.Close();
            return rowAffected;
        }
    }
}
