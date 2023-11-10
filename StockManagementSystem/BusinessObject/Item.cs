

namespace StockManagementSystem.BusinessObject
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string ReOrderLevel { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public int CompanyId { get; set; }
        public Company? Company { get; set; }
    }
}
