namespace MlsMarket.Entity
{
    public class MlsMarketEntity
    {
        public string? PropertyType { get; set; }
        public string? Area { get; set; }
        public string? BenchmarkPrice { get; set; }
        public string? PriceIndex { get; set; }
        public string? OneMonthChange { get; set; }
        public string? ThreeMonthChange { get; set; }
        public string? SixMonthChange { get; set; }
        public string? OneYearChange { get; set; }
        public string? ThreeYearChange { get; set; }
        public string? FiveYearChange { get; set; }
        public string? TenYearChange { get; set; }
        public DateTime Date { get; set; }
    }
}
