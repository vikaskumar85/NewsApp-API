using System.Diagnostics.CodeAnalysis;

namespace NewsApp.Core.Models
{
    /// <summary>
    /// News item model
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class NewsItemModel
    {
        /// <summary>
        /// Item identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Title of the item
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Url of the item
        /// </summary>
        public string? Url { get; set; }
    }
}
