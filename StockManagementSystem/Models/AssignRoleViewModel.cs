using System;
using System.Collections.Generic;

namespace StockManagementSystem.Models
{
    public class AssignRoleViewModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public IList<string> UserRoles { get; set; }
        public List<string>? AllRoles { get; set; } // Add this property
    }
}
