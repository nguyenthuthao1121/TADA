using TADA.Dto.Book;
using TADA.Dto.Order;
using TADA.Model;
using TADA.Model.Entity;

namespace TADA.Repository;

public interface IOrderRepository
{
    List<OrderDto> GetAllOrdersByCustomerId(int customerId);
    List<OrderDto> GetAllOrdersByAccountId(int accountId);
    List<OrderDetailDto> GetOrderDetailsByOrder(OrderDto order);
    List<OrderDto> GetOrdersByAccountId(int accountId, int statusId);
    BookDto GetBookByOrderDetail(OrderDetailDto OrderDetail);
    string GetStatusByOrder(OrderDto order);
    void DeleteOrder(OrderDto order);
    void AddOrder(OrderDto order);
    void UpdateStatusOrder(int orderId, int statusId);
    void AddOrderDetail(OrderDetailDto orderDetail);
    void UpdateOrderDetail(OrderDetailDto orderDetail, int quantity);
    void DeleteOrderDetail(OrderDetailDto orderDetail);

}
