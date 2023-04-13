using TADA.Dto;
using TADA.Model;
using TADA.Model.Entity;

namespace TADA.Repository;

public interface ICartRepository
{
    CartDto GetCartByCustomerId(int customerId);
    List<CartDetailDto> GetCartDetailsByCustomerId(int customerId);
    CartDto GetCartByAccountId(int accountId);
    List<CartDetailDto> GetCartDetailsByAccountId(int accountId);
    BookDto GetBookByCartDetail(CartDetailDto cartDetail);
}
