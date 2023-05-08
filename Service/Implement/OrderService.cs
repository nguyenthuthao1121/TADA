using System.Drawing.Printing;
using System.Globalization;
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
        private readonly IBookRepository bookRepository;
        private readonly ICustomerRepository customerRepository;
        public OrderService(IOrderRepository orderRepository, IAddressRepository addressRepository, IBookRepository bookRepository, ICustomerRepository customerRepository)
        {
            this.orderRepository = orderRepository;
            this.addressRepository = addressRepository;
            this.bookRepository = bookRepository;
            this.customerRepository = customerRepository;
        }

        public List<OrderDto> GetAllOrders()
        {
            return orderRepository.GetAllOrders();
        }

        public List<OrderDto> GetAllOrdersByAccountId(int accountId)
        {
            return orderRepository.GetAllOrdersByAccountId(accountId);
        }

        public OrderDto GetOrderById(int orderId)
        {
            return orderRepository.GetOrderById(orderId);
        }
        public BookDto GetBookByOrderDetail(OrderDetailDto orderDetail)
        {
            return orderRepository.GetBookByOrderDetail(orderDetail);
        }
        public List<BookDto> GetBooksByOrderId(int orderId)
        {
            List<BookDto> books= new List<BookDto>();
            var orderDetails = orderRepository.GetOrderDetailsByOrderId(orderId);
            foreach (var orderDetail in orderDetails)
            {
                books.Add(orderRepository.GetBookByOrderDetail(orderDetail));
            }
            return books;
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
        public List<OrderManagementDto> GetAllOrdersForManagement(string? search,string province, string priceRange, int statusId, string sortBy)
        {
            int min = 0;
            int max = int.MaxValue;
            if (!priceRange.Equals("All") && !string.IsNullOrEmpty(priceRange))
            {
                var i = priceRange.Split(",");
                try
                {
                    min = int.Parse(i[0]);
                }
                catch (Exception)
                {
                    min = 0;
                }
                try
                {
                    max = int.Parse(i[1]);
                }
                catch (Exception)
                {
                    max = int.MaxValue;
                }
            }
            if (province.Equals("All") || string.IsNullOrEmpty(province))
            {
                province = "";
            }
            var list = new List<OrderManagementDto>();
            var orders = orderRepository.GetAllOrders(search, statusId, sortBy);
            foreach (var order in orders)
            {
                var orderDetailList = orderRepository.GetOrderDetailsByOrderId(order.Id);
                int sum = 0;
                foreach (var orderDetail in orderDetailList)
                {
                    sum += orderDetail.Price;
                }
                if((sum + order.ShipFee) <= max && (sum + order.ShipFee) >= min && order.Address.Contains(province))
                {
                    list.Add(new OrderManagementDto
                    {
                        Id = order.Id,
                        DateOrder = order.DateOrder,
                        Address = addressRepository.GetAddressById((int)order.AddressId),
                        TelephoneNumber = order.TelephoneNumber,
                        Price = sum + order.ShipFee,
                        Status = orderRepository.GetStatusByOrderId(order.StatusId)
                    });
                }
                
            }

            return list;
        }
        public List<OrderManagementDto> GetOrdersByCustomerId(int customerId, string? search, string province, string priceRange, int statusId, string sortBy)
        {
            List<OrderDto> orders;
            if (customerId > 0)
            {
                orders = orderRepository.GetAllOrdersOfCustomer(customerId, search, statusId, sortBy);
            }
            else
            {
                orders = orderRepository.GetAllOrders(search, statusId, sortBy);
            }
            
            int min = 0;
            int max = int.MaxValue;
            if (!priceRange.Equals("All") && !string.IsNullOrEmpty(priceRange))
            {
                var i = priceRange.Split(",");
                try
                {
                    min = int.Parse(i[0]);
                }
                catch (Exception)
                {
                    min = 0;
                }
                try
                {
                    max = int.Parse(i[1]);
                }
                catch (Exception)
                {
                    max = int.MaxValue;
                }
            }
            if (province.Equals("All") || string.IsNullOrEmpty(province))
            {
                province = "";
            }

            var list = new List<OrderManagementDto>();
            foreach (var order in orders)
            {
                var orderDetailList = orderRepository.GetOrderDetailsByOrderId(order.Id);
                int sum = 0;
                foreach (var orderDetail in orderDetailList)
                {
                    sum += orderDetail.Price;
                }
                if ((sum + order.ShipFee) <= max && (sum + order.ShipFee) >= min && order.Address.Contains(province))
                {
                    list.Add(new OrderManagementDto
                    {
                        Id = order.Id,
                        DateOrder = order.DateOrder,
                        Address = addressRepository.GetAddressById((int)order.AddressId),
                        TelephoneNumber = order.TelephoneNumber,
                        Price = sum + order.ShipFee,
                        Status = orderRepository.GetStatusByOrderId(order.StatusId)
                    });
                }

            }
            
            return list;
        }
        public List<RecentlyOrderDto> GetRecentlyOrders(int count)
        {
            List<OrderDto> orderDtos = orderRepository.GetAllOrders().Take(count).ToList();
            //orderDtos.Reverse();
            List<RecentlyOrderDto> orders = new List<RecentlyOrderDto>();
            foreach(var order in orderDtos)
            {
                var address = addressRepository.GetOrderAddressDto(order.Id);
                orders.Add(new RecentlyOrderDto()
                {
                    OrderId = order.Id,
                    CustomerName = customerRepository.GetCustomerById(order.CustomerId).Name,
                    Province = $"{address.Street}, {address.WardName}, {address.DistrictName}, {address.ProvinceName}",
                    Status = orderRepository.GetStatusByStatusId(order.StatusId)
                });
            }
            return orders;
        }

        public int RevueneOfMonth(int month, int year) 
        {
            var deliveredOrders = orderRepository.GetDeliveredOrderInMonth(month, year);
            int revuene = 0;
            foreach (var order in deliveredOrders)
            {
                revuene += orderRepository.GetPriceOfOrder(order.OrderId);
            }
            return revuene;
        }
        public void UpdateStatusOrder(int orderId, int statusId)
        {
            orderRepository.UpdateStatusOrder(orderId, statusId);
            if (statusId == 5)
            {
                var orderDetails = orderRepository.GetOrderDetailsByOrderId(orderId);
                foreach (var orderDetail in orderDetails)
                {
                    var book = bookRepository.GetBookById(orderDetail.BookId);
                    if (book != null)
                    {
                        bookRepository.UpdateQuantity(book.Id, book.Quantity + orderDetail.Quantity);
                    }
                }
            }
        }
        public void UpdateOrder(int orderId, OrderDto orderDto)
        {
            orderRepository.UpdateOrder(orderId, orderDto);
            orderRepository.UpdateOrderShipfee(orderId, CalculateShipping(orderId));
        }
        public void DeleteOrder(int orderId)
        {
            var orderDetails = orderRepository.GetOrderDetailsByOrderId(orderId);
            foreach (var orderDetail in orderDetails)
            {
                var book = bookRepository.GetBookById(orderDetail.BookId);
                if (book != null)
                {
                    bookRepository.UpdateQuantity(book.Id, book.Quantity + orderDetail.Quantity);
                }
            }
            orderRepository.DeleteOrder(orderId);
        }

        public void AddOrder(int accountId, List<OrderDetailDto> orderDetails)
        {
            orderRepository.AddOrder(accountId);
            var order = orderRepository.GetOrdersByAccountId(accountId, 6).FirstOrDefault();
            foreach(var orderDetail in orderDetails)
            {
                orderRepository.UpdateOrderDetail(orderDetail.BookId, order.Id, orderDetail.Quantity, orderDetail.Price);
                var book=bookRepository.GetBookById(orderDetail.BookId);
                if(book!= null)
                {
                    bookRepository.UpdateQuantity(book.Id, book.Quantity - orderDetail.Quantity);
                }
            }
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
                        from_address = "54 Nguyễn Lương Bằng",
                        from_ward_name = "Phường Hòa Khánh Bắc",
                        from_district_name = "Quận Liên Chiểu",
                        from_province_name = "TP Đà Nẵng",
                        required_note = "KHONGCHOXEMHANG",
                        return_name = "TADA",
                        return_phone = "0909999999",
                        return_address = "54 Nguyễn Lương Bằng",
                        return_ward_name = "Phường Hòa Khánh Bắc",
                        return_district_name = "Quận Liên Chiểu",
                        return_province_name = "TP Đà Nẵng",
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
