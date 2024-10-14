namespace backend.Helpers
{
    /// <summary>
    /// Object for handling filtering and pagination on querys
    /// </summary>
    public class QueryObject
    {
        /// <summary>
        /// Field to sort by
        /// </summary>
        public string? SortBy { get; set; } = null;

        /// <summary>
        /// Whether the sorting is descenfing or not
        /// </summary>
        public bool IsDescending { get; set; } = false;

        /// <summary>
        /// Page number to display
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// Number of records per page
        /// </summary>
        public int PageSize { get; set; } = 20;

        /// <summary>
        /// text to filter the results by
        /// </summary>
        public string? Filter { get; set; } = null;
    }
}
