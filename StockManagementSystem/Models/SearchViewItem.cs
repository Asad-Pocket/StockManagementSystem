
namespace StockManagementSystem.Models
{
    public class SearchViewItem 
    {
        public List<Company> Companies { get; set; }
        public List<Category> Categories { get; set; }

        public int SelectedCompany { get; set; }
        public int SelectedCategory { get; set; }

        public List<Item> Items { get; set; }
    }
}
