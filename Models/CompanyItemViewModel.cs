using AutoMapper;
using StockManagementSystem.Models;
using StockManagementSystem.Service;
using System.ComponentModel.DataAnnotations;

namespace StockManagementSystem.Models
{
    public class CompanyItemViewModel
    {

        public IEnumerable<Company>? Companies { get; set; }
        public IEnumerable<Item>? Items { get; set; }
        public Item? SelectedItem { get; set; } = new Item();
        public int StockNewQuantity { get; set; }

        public int SelectedCompanyId { get; set; }
        public int SelectedItemId { get; set; }
    }
}