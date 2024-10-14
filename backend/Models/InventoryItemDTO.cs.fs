using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class InventoryItemCreateDTO
    {
        [Required]
        public required string Name { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be 0 or greater.")]
        public int Quantity { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be 0 or greater.")]
        public decimal Price { get; set; }
    }
}
