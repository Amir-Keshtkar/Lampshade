using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.VisualBasic.CompilerServices;
using ShopManagement.Application.Contract.Product;

namespace InventoryManagement.Application.Contract.Inventory {
    public class CreateInventory {
        [Range(1, 100000, ErrorMessage = ValidationMessages.IsRequired)]
        public long ProductId { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = ValidationMessages.IsRequired)]
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [DataType(DataType.Currency, ErrorMessage = ValidationMessages.NotInteger)]
        public double UnitPrice { get; set; }

        public List<ProductViewModel> Products { get; set; }
    }
}

