using System.Net.Http.Headers;
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

        public void DeleteOrder(OrderDto order)
        {
            orderRepository.DeleteOrder(order);
        }

        public void AddOrder(OrderDto order)
        {
            // tinh ship gan vo 1 bien
            // orderRepository.AddOrder(order, tienship)
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

        private double TinhTienShip()
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://dev-online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/create"))
                {
                    request.Headers.TryAddWithoutValidation("ShopId", "121749");
                    request.Headers.TryAddWithoutValidation("Token", "ae3f43bd-b053-11ed-8181-eee966792c8f");

                    var items = new List<Item>();
                    items.Add(new Item()
                    {
                        name = "Áo Polo",
                        code = "Polo123",
                        quantity = 1,
                        price = 200000,
                        length = 12,
                        width = 12,
                        height = 12,
                        category = new CategoryShipping()
                        {
                            level1 = "Sách"
                        }
                    });

                    var order = new OrderShipping()
                    {
                        payment_type_id = 2,
                        note = "Tintest 123",
                        from_name = "Tin",
                        from_phone = "0909999999",
                        from_address = "123 Đường 3/2",
                        from_ward_name = "Phường 5",
                        from_district_name = "Quận 11",
                        from_province_name = "TP Hồ Chí Minh",
                        required_note = "KHONGCHOXEMHANG",
                        return_name = "Tin",
                        return_phone = "0909999999",
                        return_address = "123 Đường 3/2",
                        return_ward_name = "Phường 5",
                        return_district_name = "Quận 11",
                        return_province_name = "TP Hồ Chí Minh",
                        client_order_code = "",
                        to_name = "Độ Mixi",
                        to_phone = "0909998877",
                        to_address = "Streaming house",
                        to_ward_name = "Phường 14",
                        to_district_name = "Quận 10",
                        to_province_name = "TP Hồ Chí Minh",
                        cod_amount = 200000,
                        content = "Theo New York Times",
                        weight = 200,
                        length = 1,
                        width = 19,
                        height = 10,
                        cod_failed_amount = 2000,
                        pick_station_id = 1444,
                        deliver_station_id = null,
                        insurance_value = 10000000,
                        service_id = 0,
                        service_type_id = 2,
                        coupon = null,
                        pick_shift = null,
                        pickup_time = 1665272576,
                        shop_id = 121749,
                        items = items
                    };

                    var reqBody = JsonSerializer.Serialize(order);

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
