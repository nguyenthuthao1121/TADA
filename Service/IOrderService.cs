using TADA.Dto.Book;
using TADA.Dto.Order;

namespace TADA.Service;

public interface IOrderService
{
    List<OrderDto> GetAllOrdersByAccountId(int accountId);
    List<OrderDetailDto> GetOrderDetailsByOrderId(int orderId);
    List<OrderDto> GetOrdersByAccountId(int accountId, int statusId);
    OrderDto GetOrderById(int orderId);
    BookDto GetBookByOrderDetail(OrderDetailDto OrderDetail);
    string GetStatusByOrder(int orderId);
    List<OrderManagementDto> GetAllOrdersForManagement();
    List<OrderManagementDto> GetOrdersByCustomerId(int customerId);
    void UpdateOrder(int orderId, OrderDto orderDto);
    void UpdateStatusOrder(int orderId, int statusId);
    void DeleteOrder(int orderId);
    void AddOrder(int accountId, OrderDetailDto orderDetail);
    void DeleteOrderDetail(int bookId, int orderId);
}
