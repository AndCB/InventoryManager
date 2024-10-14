using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    /// <summary>
    /// Inventory item
    /// </summary>
    public class InventoryItem
    {
        /// <summary>
        /// The unique identifier of the item
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the item
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The quantity of the item in stock
        /// </summary>
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        /// <summary>
        /// The price of the item
        /// </summary>
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        /// <summary>
        /// constructor for mapping InventoryItemDTOs to InventoryItem
        /// </summary>
        /// <param name="itemDTO">DTO for handling create operations</param>
        public InventoryItem(InventoryItemDTO itemDTO)
        {
            Name = itemDTO.Name;
            Quantity = itemDTO.Quantity;
            Price = itemDTO.Price;
        }

        /// <summary>
        /// Constructor for initializing inventory items
        /// </summary>
        /// <param name="id">item id</param>
        /// <param name="name">item name</param>
        /// <param name="quantity">item quantity</param>
        /// <param name="price">item price</param>
        public InventoryItem(int id, string name, int quantity, decimal price)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            Price = price;
        }

        /// <summary>
        /// Empty constructor for instanciation
        /// </summary>
        public InventoryItem()
        {
            Name = string.Empty;
        }
    }
}
