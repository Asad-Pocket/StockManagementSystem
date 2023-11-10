namespace StockManagementSystem.BusinessObjects
{
    public class SoldItemsBO
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public String ItemName { get; set; }
        public String CompanyName { get; set; }
        public int QuantityToChange { get; set; }
        public DateTime Date { get; set; }
    }
}