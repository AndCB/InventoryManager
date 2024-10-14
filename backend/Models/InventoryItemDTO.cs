using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    /// <summary>
    ///
    /// </summary>
    public class InventoryItemDTO
    {
        /// <summary>
        /// The name of the item
        /// </summary>
        [Required]
        public required string Name { get; set; }

        /// <summary>
        /// The quantity in stock of the item
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be 0 or greater.")]
        public int Quantity { get; set; }

        /// <summary>
        /// The price of the item
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Price must be 0 or greater.")]
        public decimal Price { get; set; }
    }
}
