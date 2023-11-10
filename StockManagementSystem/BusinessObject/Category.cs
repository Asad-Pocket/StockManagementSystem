namespace StockManagementSystem.BusinessObject
{
    public class Category
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public List<Item> items { get; set; }
    }
}
