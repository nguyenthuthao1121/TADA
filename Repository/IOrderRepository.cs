﻿using TADA.Dto.Book;
using TADA.Dto.Order;
using TADA.Model;
using TADA.Model.Entity;

namespace TADA.Repository;

public interface IOrderRepository
{
    List<OrderDto> GetAllOrders();
    List<OrderDto> GetAllOrdersByCustomerId(int customerId);
    List<OrderDto> GetAllOrdersByAccountId(int accountId);
    List<OrderDto> GetOrdersByAccountId(int accountId, int statusId);
    OrderDto GetOrderById(int orderId);
    BookDto GetBookByOrderDetail(OrderDetailDto orderDetail);
    string GetStatusByOrder(int orderId);
    string GetStatusByOrderId(int orderId);
    List<OrderDetailDto> GetOrderDetailsByOrderId(int orderId);
    void DeleteOrder(int orderId);
    void AddOrder(int accountId);
    void UpdateOrder(int orderId, OrderDto orderDto);
    void UpdateOrderShipfee(int orderId, int fee);
    void UpdateStatusOrder(int orderId, int statusId);
    void UpdateOrderDetail(int bookId, int orderId, int quantity, int price);
    void DeleteOrderDetail(int bookId, int orderId);
}
