using TADA.Dto.Book;
using TADA.Dto.Order;

namespace TADA.Service;

public interface IOrderService
{
    List<OrderDto> GetAllOrdersByCustomerId(int customerId);
    List<OrderDto> GetAllOrdersByAccountId(int accountId);
    List<OrderDetailDto> GetOrderDetailsByOrder(OrderDto order);
    List<OrderDto> GetOrdersByAccountId(int accountId, int statusId);
    OrderDto GetOrderById(int orderId);
    BookDto GetBookByOrderDetail(OrderDetailDto OrderDetail);
    string GetStatusByOrder(OrderDto order);
    List<OrderManagementDto> GetAllOrdersForManagement();
    List<OrderManagementDto> GetOrdersByCustomerId(int customerId);
    void DeleteOrder(OrderDto order);
    void AddOrder(OrderDto order);
    void UpdateStatusOrder(int orderId, int statusId);
    void AddOrderDetail(OrderDetailDto orderDetail);
    void UpdateOrderDetail(OrderDetailDto orderDetail, int quantity);
    void DeleteOrderDetail(OrderDetailDto orderDetail);
}
