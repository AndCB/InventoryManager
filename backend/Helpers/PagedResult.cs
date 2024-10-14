using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Helpers
{
    /// <summary>
    /// A paginated query result
    /// </summary>
    /// <typeparam name="T">Type of the expected result from the query</typeparam>
    public class PagedResult<T>
    {
        /// <summary>
        /// List of items received from the query
        /// </summary>
        public List<T> Items { get; set; } = [];

        /// <summary>
        /// Total number of entries in the table
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Number of records being returned in this page
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// number of the page being returned
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// total number of pages given the total amount of records and the number of records per page
        /// </summary>
        public int TotalPages { get; set; }
    }
}
