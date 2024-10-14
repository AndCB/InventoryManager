using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    /// <summary>
    /// Application's database context, responsible for interacting with the database
    /// using EntotyFramework Core.
    /// </summary>
    /// <param name="dbContextOptions">Configuration options for the database context, such as the database connection string</param>
    public class AppDBContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
    {
        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> for the <see cref="InventoryItem"/> entity.
        /// This property represents the 'InventoryItems' table in the database, allowing CRUD operations on InventoryItem records.
        /// </summary>
        public DbSet<InventoryItem> InventoryItems { get; set; }
    }
}
