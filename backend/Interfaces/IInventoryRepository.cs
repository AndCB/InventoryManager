using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Helpers;
using backend.Models;

namespace backend.Interfaces
{
    /// <summary>
    /// Interface for managing inventory items.
    /// Provides methods for creating, retrieving, updating, and deleting inventory items.
    /// </summary>
    public interface IInventoryRepository
    {
        /// <summary>
        ///  Creates a new inventory item asynchronously.
        /// </summary>
        /// <param name="item">The inventory item to create, represented as an <see cref="InventoryItemDTO"/>.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the created <see cref="InventoryItem"/>.</returns>
        Task<InventoryItem> CreateAsync(InventoryItemDTO item);

        /// <summary>
        ///  Deletes an existing inventory item asynchronously by its ID.
        /// </summary>
        /// <param name="id">The ID of the inventory item to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the deleted <see cref="InventoryItem"/> if found; otherwise, null.</returns>
        Task<InventoryItem?> DeleteAsync(int id);

        /// <summary>
        /// Retrieves all inventory items asynchronously, with optional filtering and pagination.
        /// </summary>
        /// <param name="query">The query object that contains filtering, sorting, and pagination parameters.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="PagedResult{InventoryItem}"/> containing the inventory items.</returns>
        Task<PagedResult<InventoryItem>> GetAllAsync(QueryObject query);

        /// <summary>
        /// Retrieves a specific inventory item asynchronously by its ID.
        /// </summary>
        /// <param name="id">The ID of the inventory item to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="InventoryItem"/> if found; otherwise, null.</returns>
        Task<InventoryItem?> GetByIdAsync(int id);

        /// <summary>
        /// Checks whether an inventory item exists asynchronously by its ID.
        /// </summary>
        /// <param name="id">The ID of the inventory item to check for existence.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is true if the item exists; otherwise, false.</returns>
        Task<bool> ItemExists(int id);

        /// <summary>
        /// Updates an existing inventory item asynchronously by its ID.
        /// </summary>
        /// <param name="id">The ID of the inventory item to update.</param>
        /// <param name="itemDto">The updated inventory item data, represented as an <see cref="InventoryItemDTO"/>.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated <see cref="InventoryItem"/> if the update was successful; otherwise, null.</returns>
        Task<InventoryItem?> UpdateAsync(int id, InventoryItemDTO itemDto);
    }
}
