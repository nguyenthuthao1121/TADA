using TADA.Dto.Book;
using TADA.Dto.Order;

namespace TADA.Service;

public interface IOrderService
{
    List<OrderDto> GetAllOrders();
    List<OrderDto> GetAllOrdersByAccountId(int accountId);
    List<OrderDetailDto> GetOrderDetailsByOrderId(int orderId);
    List<BookDto> GetBooksByOrderId(int orderId);
    OrderDetailDto GetOrderDetail(int orderId, int bookId);
    List<OrderDto> GetOrdersByAccountId(int accountId, int statusId);
    OrderDto GetOrderById(int orderId);
    BookDto GetBookByOrderDetail(OrderDetailDto OrderDetail);
    string GetStatusByOrder(int orderId);
    List<RecentlyOrderDto> GetRecentlyOrders(int count);
    int RevueneOfMonth(int month, int year);
    List<OrderManagementDto> GetAllOrdersForManagement(string? search, string province, string priceRange, int statusId, string sortBy);
    List<OrderManagementDto> GetOrdersByCustomerId(int customerId, string? search, string province, string priceRange, int statusId, string sortBy);
    void UpdateOrder(int orderId, OrderDto orderDto);
    void UpdateStatusOrder(int orderId, int statusId);
    void DeleteOrder(int orderId);
    void AddOrder(int accountId, List<OrderDetailDto> orderDetail);
    void DeleteOrderDetail(int bookId, int orderId);
    int GetNumOfSoldBooks();
    int GetNumOfOrders();
    int GetNumOfOrdersByYear(int year);
}
