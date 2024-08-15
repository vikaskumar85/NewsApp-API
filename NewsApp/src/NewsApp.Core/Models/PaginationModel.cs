using System.Diagnostics.CodeAnalysis;

namespace NewsApp.Core.Models
{
    /// <summary>
    /// Pagination model
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class PaginationModel
    {
        /// <summary>
        /// Page no
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; set; } = 5;
    }
}
