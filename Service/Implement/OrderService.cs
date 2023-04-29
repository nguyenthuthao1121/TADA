﻿using System.Net.Http.Headers;
using System.Text.Json;
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
        private readonly IBookRepository bookRepository;
        private readonly ICustomerRepository customerRepository;
        public OrderService(IOrderRepository orderRepository, IAddressRepository addressRepository, IBookRepository bookRepository, ICustomerRepository customerRepository)
        {
            this.orderRepository = orderRepository;
            this.addressRepository = addressRepository;
            this.bookRepository = bookRepository;
            this.customerRepository = customerRepository;
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

        public string GetStatusByOrder(int orderId)
        {
            return orderRepository.GetStatusByOrder(orderId);
        }

        public List<OrderDetailDto> GetOrderDetailsByOrderId(int orderId)
        {
            return orderRepository.GetOrderDetailsByOrderId(orderId);
        }
        public OrderDetailDto GetOrderDetail(int orderId, int bookId)
        {
            return orderRepository.GetOrderDetail(orderId, bookId);
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

        public void UpdateStatusOrder(int orderId, int statusId)
        {
            orderRepository.UpdateStatusOrder(orderId, statusId);
        }
        public void UpdateOrder(int orderId, OrderDto orderDto)
        {
            orderRepository.UpdateOrder(orderId, orderDto);
        }
        public void DeleteOrder(int orderId)
        {
            orderRepository.DeleteOrder(orderId);
        }

        public void AddOrder(int accountId, List<OrderDetailDto> orderDetails)
        {
            orderRepository.AddOrder(accountId);
            var order = orderRepository.GetOrdersByAccountId(accountId, 6).FirstOrDefault();
            foreach(var orderDetail in orderDetails)
                orderRepository.UpdateOrderDetail(orderDetail.BookId, order.Id, orderDetail.Quantity, orderDetail.Price);
            orderRepository.UpdateOrderShipfee(order.Id, CalculateShipping(order.Id));
        }

        public void DeleteOrderDetail(int bookId, int orderId)
        {
            orderRepository.DeleteOrderDetail(bookId, orderId);
        }

        private int CalculateShipping(int orderId)
        {
            var order=orderRepository.GetOrderById(orderId);
            var orderDetails = orderRepository.GetOrderDetailsByOrderId(orderId);
            var customer=customerRepository.GetCustomerById(order.CustomerId);
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://dev-online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/create"))
                {
                    request.Headers.TryAddWithoutValidation("ShopId", "121749");
                    request.Headers.TryAddWithoutValidation("Token", "ae3f43bd-b053-11ed-8181-eee966792c8f");

                    var items = new List<Item>();
                    foreach(var orderDetail in orderDetails)
                    {
                        var book=bookRepository.GetBookById(orderDetail.BookId);
                        items.Add(new Item()
                        {
                            name = book.Name,
                            //code = "200",
                            quantity = orderDetail.Quantity,
                            price = orderDetail.Price,
                            length = (int)book.Length,
                            width = (int)book.Width,
                            height = (int)(book.Pages/2*0.01),
                            weight=(int)book.Weight,
                            category = new CategoryShipping()
                            {
                                level1 = "Sách"
                            }
                        });
                    }
                    int orderLength, orderWidth, orderHeight, orderWeight, orderPrice;
                    orderLength = orderWidth = orderHeight = orderWeight = orderPrice = 0;
                    foreach (var item in items)
                    {
                        if (item.length>orderLength) orderLength = item.length;
                        if (item.width> orderWidth) orderWidth = item.width;
                        orderHeight += item.height;
                        orderWeight += item.weight;
                        orderPrice += item.price;
                    }
                    var orderShipping = new OrderShipping()
                    {
                        payment_type_id = 2, //Ma nguoi thanh toan dich vu. 1: nguoi ban, 2: nguoi mua
                        //note = "Tintest 123",
                        from_name = "TADA",
                        from_phone = "0909999999",
                        from_address = "123 Đường 3/2",
                        from_ward_name = "Phường 5",
                        from_district_name = "Quận 11",
                        from_province_name = "TP Hồ Chí Minh",
                        required_note = "KHONGCHOXEMHANG",
                        return_name = "TADA",
                        return_phone = "0909999999",
                        return_address = "123 Đường 3/2",
                        return_ward_name = "Phường 5",
                        return_district_name = "Quận 11",
                        return_province_name = "TP Hồ Chí Minh",
                        client_order_code = "11",
                        to_name = customer.Name,
                        to_phone = order.TelephoneNumber,
                        to_address = addressRepository.GetAddressByIdAndPart((int)order.AddressId,1),
                        to_ward_name = addressRepository.GetAddressByIdAndPart((int)order.AddressId, 2),
                        to_district_name = addressRepository.GetAddressByIdAndPart((int)order.AddressId, 3),
                        to_province_name = addressRepository.GetAddressByIdAndPart((int)order.AddressId, 4),
                        cod_amount = orderPrice,
                        content = "",
                        weight = orderWeight,
                        length = orderLength,
                        width = orderWidth,
                        height = orderHeight,
                        cod_failed_amount =(int)(orderPrice * 0.05),
                        pick_station_id = 1058,
                        deliver_station_id = null,
                        insurance_value = orderPrice,
                        service_id = 0,
                        service_type_id = 2,
                        coupon = null,
                        pick_shift = null,
                        pickup_time = 1665272576,
                        shop_id = 121749,
                        items = items
                    };

                    var reqBody = JsonSerializer.Serialize(orderShipping);

                    request.Content = new StringContent(reqBody);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = httpClient.SendAsync(request).Result;

                    var resBody = response.Content.ReadAsStringAsync().Result;
                    var detail = JsonSerializer.Deserialize<ResponseOrder>(resBody);
                    return detail.data.total_fee;
                }
            }
        }
    }
}
