using System.Threading.Tasks;
using SwiftMart.API.Models;

namespace SwiftMart.API.Interfaces
{
    public interface IPaymentRepository
    {
        Task<Payment> GetPaymentByOrderIdAsync(int orderId);
        Task AddPaymentAsync(Payment payment);
        Task UpdatePaymentAsync(Payment payment);
    }
}
