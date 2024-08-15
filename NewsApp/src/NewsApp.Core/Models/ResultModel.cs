using System.Diagnostics.CodeAnalysis;

namespace NewsApp.Core.Models
{
    /// <summary>
    /// News result model
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ResultModel
    {
        // News items
        public IEnumerable<NewsItemModel> Items { get; set; }

        /// <summary>
        /// Total no of records found
        /// </summary>
        public int TotalNoOfItems { get; set; }
    }
}
