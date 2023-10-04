namespace StockManagementSystem.BusinessObject
{
    public class Company
    {
        public int Id { get; set; }
        public string name { get; set; }
        public List<Item> items { get; set; }
    }
}
