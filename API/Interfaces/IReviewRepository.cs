using System.Collections.Generic;
using System.Threading.Tasks;
using SwiftMart.API.Models;

namespace SwiftMart.API.Interfaces
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetReviewsByProductIdAsync(int productId);
        Task AddReviewAsync(Review review);
        Task UpdateReviewAsync(Review review);
        Task DeleteReviewAsync(int id);
    }
}
