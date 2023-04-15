using TADA.Dto.Book;
using TADA.Dto.Order;
using TADA.Model;
using TADA.Model.Entity;
using TADA.Repository;
using TADA.Repository.Implement;

namespace TADA.Service.Implement
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public List<OrderDto> GetAllOrdersByCustomerId(int customerId)
        {
            return orderRepository.GetAllOrdersByCustomerId(customerId);
        }

        public List<OrderDto> GetAllOrdersByAccountId(int accountId)
        {
            return orderRepository.GetAllOrdersByAccountId(accountId);
        }

        public OrderDto GetOrderById(int orderId)
        {
            return orderRepository.GetOrderById(orderId);
        }
        public BookDto GetBookByOrderDetail(OrderDetailDto OrderDetail)
        {
            return orderRepository.GetBookByOrderDetail(OrderDetail);
        }

        public List<OrderDto> GetOrdersByAccountId(int accountId, int statusId)
        {
            return orderRepository.GetOrdersByAccountId(accountId, statusId);
        }

        public string GetStatusByOrder(OrderDto order)
        {
            return orderRepository.GetStatusByOrder(order);
        }

        public List<OrderDetailDto> GetOrderDetailsByOrder(OrderDto order)
        {
            return orderRepository.GetOrderDetailsByOrder(order);
        }

        public void DeleteOrder(OrderDto order)
        {
            orderRepository.DeleteOrder(order);
        }

        public void AddOrder(OrderDto order)
        {
            orderRepository.AddOrder(order);
        }

        public void UpdateStatusOrder(int orderId, int statusId)
        {
            orderRepository.UpdateStatusOrder(orderId, statusId);
        }

        public void AddOrderDetail(OrderDetailDto orderDetail)
        {
            orderRepository.AddOrderDetail(orderDetail);
        }

        public void UpdateOrderDetail(OrderDetailDto orderDetail, int quantity)
        {
            orderRepository.UpdateOrderDetail(orderDetail, quantity);
        }

        public void DeleteOrderDetail(OrderDetailDto orderDetail)
        {
            orderRepository.DeleteOrderDetail(orderDetail);
        }
    }
}
