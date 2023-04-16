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
        private readonly IAddressRepository addressRepository;
        public OrderService(IOrderRepository orderRepository, IAddressRepository addressRepository)
        {
            this.orderRepository = orderRepository;
            this.addressRepository = addressRepository;
        }

        public List<OrderDto> GetAllOrdersByCustomerId(int customerId)
        {
            return orderRepository.GetAllOrdersByCustomerId(customerId);
        }

        public List<OrderDto> GetAllOrdersByAccountId(int accountId)
        {
            return orderRepository.GetAllOrdersByAccountId(accountId);
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
        public List<OrderManagementDto> GetAllOrdersForManagement()
        {
            var list = new List<OrderManagementDto>();
            var orders = orderRepository.GetAllOrders();
            foreach (var order in orders)
            {
                var orderDetailList = orderRepository.GetOrderDetailsByOrderId(order.Id);
                int sum = 0;
                foreach (var orderDetail in orderDetailList)
                {
                    var book = orderRepository.GetBookByOrderDetail(orderDetail);
                    sum += book.GetCurrentPrice();
                }
                list.Add(new OrderManagementDto
                {
                    Id = order.Id,
                    DateOrder = order.DateOrder,
                    Address = addressRepository.GetAddressById((int)order.AddressId),
                    TelephoneNumber = order.TelephoneNumber,
                    Price = sum,
                    Status = orderRepository.GetStatusByOrderId(order.StatusId)
                });
            }
            return list;
        }
        public List<OrderManagementDto> GetOrdersByCustomerId(int customerId)
        {
            List<OrderDto> orders;
            if (customerId > 0)
            {
                orders = orderRepository.GetAllOrdersByCustomerId(customerId);
            }
            else
            {
                orders = orderRepository.GetAllOrders();
            }
            var list = new List<OrderManagementDto>();
            foreach (var order in orders)
            {
                var orderDetailList = orderRepository.GetOrderDetailsByOrderId(order.Id);
                int sum = 0;
                foreach (var orderDetail in orderDetailList)
                {
                    var book = orderRepository.GetBookByOrderDetail(orderDetail);
                    sum += book.GetCurrentPrice();
                }
                list.Add(new OrderManagementDto
                {
                    Id = order.Id,
                    DateOrder = order.DateOrder,
                    Address = addressRepository.GetAddressById((int)order.AddressId),
                    TelephoneNumber = order.TelephoneNumber,
                    Price = sum,
                    Status = orderRepository.GetStatusByOrderId(order.StatusId)
                });
            }
            return list;
        }
    }
}
