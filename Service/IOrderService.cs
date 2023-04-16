using TADA.Dto.Book;
using TADA.Dto.Order;

namespace TADA.Service;

public interface IOrderService
{
    List<OrderDto> GetAllOrdersByCustomerId(int customerId);
    List<OrderDto> GetAllOrdersByAccountId(int accountId);
    List<OrderDetailDto> GetOrderDetailsByOrder(OrderDto order);
    List<OrderDto> GetOrdersByAccountId(int accountId, int statusId);
    BookDto GetBookByOrderDetail(OrderDetailDto OrderDetail);
    string GetStatusByOrder(OrderDto order);
    List<OrderManagementDto> GetAllOrdersForManagement();
    List<OrderManagementDto> GetOrdersByCustomerId(int customerId);
}
