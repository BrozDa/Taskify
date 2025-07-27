
namespace Taskify.API.Services.Interfaces
{
    /// <summary>
    /// Provides inidital data seeding operation
    /// </summary>
    public interface ISeedService
    {
        /// <summary>
        /// Inserts initial data to the database
        /// </summary>
        Task InsertSeedData();
    }
}