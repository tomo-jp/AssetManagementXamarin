namespace AssetManagementXamarin.Models
{
    public class ResultModel
    {
        public int Year { get; set; }
        public int TotalAsset { get; }
        public int Dividend { get; }

        public ResultModel(int year, int totalAsset, int dividend)
        {
            Year = year;
            TotalAsset = totalAsset;
            Dividend = dividend;
        }
    }
}
