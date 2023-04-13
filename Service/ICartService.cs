using TADA.Dto;
using TADA.Dto.BookDto;

namespace TADA.Service;

public interface ICartService
{
    CartDto GetCartByCustomerId(int customerId);
    List<CartDetailDto> GetCartDetailsByCustomerId(int customerId);
    CartDto GetCartByAccountId(int accountId);
    List<CartDetailDto> GetCartDetailsByAccountId(int accountId);
    BookDto GetBookByCartDetail(CartDetailDto cartDetail);
}
