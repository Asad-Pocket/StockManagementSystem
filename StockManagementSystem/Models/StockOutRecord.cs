namespace StockManagementSystem.Models
{
    public class StockOutRecord
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public String ItemName { get; set; }
        public String CompanyName { get; set; }
        public int QuantityToChange { get; set; }
        public DateTime Date { get; set; }
        public StockOutType StockOutType { get; set; }
    }
    public enum StockOutType
    {
        Sell = 1,
        Damaged = 2,
        Lost = 3
    };
}
