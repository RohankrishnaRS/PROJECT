using System.Collections.Generic;
using System.Threading.Tasks;
using SwiftMart.API.Models;

namespace SwiftMart.API.Interfaces
{
    public interface ICartRepository
    {
        Task<IEnumerable<CartItem>> GetCartItemsByUserIdAsync(int userId);
        Task<CartItem> GetCartItemByIdAsync(int id);
        Task AddToCartAsync(CartItem cartItem);
        Task UpdateCartItemAsync(CartItem cartItem);
        Task RemoveFromCartAsync(int cartItemId);
    }
}
