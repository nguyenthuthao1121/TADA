using TADA.Dto.Book;
using TADA.Dto.Order;
using TADA.Model;
using TADA.Model.Entity;

namespace TADA.Repository;

public interface IOrderRepository
{
    List<OrderDto> GetAllOrders();
    List<OrderDto> GetAllOrders(int statusId, string sortBy);
    List<OrderDto> GetAllOrdersByCustomerId(int customerId);
    List<OrderDto> GetAllOrdersOfCustomer(int customerId, int statusId, string sortby);
    List<OrderDto> GetAllOrdersByAccountId(int accountId);
    List<OrderDto> GetOrdersByAccountId(int accountId, int statusId);
    OrderDto GetOrderById(int orderId);
    BookDto GetBookByOrderDetail(OrderDetailDto orderDetail);
    string GetStatusByOrder(int orderId);
    string GetStatusByOrderId(int orderId);
    string GetStatusByStatusId(int statusId);
    List<OrderDetailDto> GetOrderDetailsByOrderId(int orderId);
    List<int> GetConfirmedOrderIds();
    List<int> GetConfirmedOrderIdsByYear(int year);
    List<OrderGroupDto> GetOrderGroupByBookId();
    int GetPriceOfOrder(int orderId);
    List<OrderOfMonthDto> GetDeliveredOrderInMonth(int month, int year);
    OrderDetailDto GetOrderDetail(int orderId, int bookId);
    void DeleteOrder(int orderId);
    void AddOrder(int accountId);
    void UpdateOrder(int orderId, OrderDto orderDto);
    void UpdateOrderShipfee(int orderId, int fee);
    void UpdateStatusOrder(int orderId, int statusId);
    void UpdateOrderDetail(int bookId, int orderId, int quantity, int price);
    void DeleteOrderDetail(int bookId, int orderId);
}
