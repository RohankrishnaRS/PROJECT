using System.Collections.Generic;
using System.Threading.Tasks;
using SwiftMart.API.Models;

namespace SwiftMart.API.Interfaces
{
    public interface ISellerRepository
    {
        Task<IEnumerable<User>> GetAllSellersAsync();
        Task<User> GetByIdAsync(int sellerId);
        Task AddSellerAsync(User seller);
        Task UpdateSellerAsync(User seller);
        Task DeleteSellerAsync(int sellerId);
    }
}
