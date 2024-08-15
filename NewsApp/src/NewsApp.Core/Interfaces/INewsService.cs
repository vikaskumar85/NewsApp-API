using NewsApp.Core.Models;

namespace NewsApp.Core.Interfaces
{
    /// <summary>
    /// News Service Interface
    /// </summary>
    public interface INewsService
    {
        /// <summary>
        /// Gets all new stories
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<NewsItemModel>> GetAllStories();
    }
}
