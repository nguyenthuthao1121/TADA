using System.Drawing.Printing;
using System.Globalization;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using TADA.Dto.Book;
using TADA.Dto.Order;
using TADA.Model;
using TADA.Model.Entity;
using TADA.Repository;
using TADA.Repository.Implement;
using TADA.Utilities;

namespace TADA.Service.Implement
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IAddressRepository addressRepository;
        private readonly IBookRepository bookRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly IStatusRepository statusRepository; 
        public OrderService(IOrderRepository orderRepository, IAddressRepository addressRepository, IBookRepository bookRepository, ICustomerRepository customerRepository, IStatusRepository statusRepository)
        {
            this.orderRepository = orderRepository;
            this.addressRepository = addressRepository;
            this.bookRepository = bookRepository;
            this.customerRepository = customerRepository;
            this.statusRepository = statusRepository;
        }

        public List<OrderDto> GetAllOrders()
        {
            try
            {
                return orderRepository.GetAllOrders();
            }
            catch (Exception)
            {
                return new List<OrderDto>();
            }
        }

        public List<OrderDto> GetAllOrdersByAccountId(int accountId)
        {
            try
            {
                return orderRepository.GetAllOrdersByAccountId(accountId);
            }
            catch (Exception)
            {
                return new List<OrderDto>();
            }
            
        }

        public OrderDto GetOrderById(int orderId)
        {
            try
            {
                return orderRepository.GetOrderById(orderId);
            }
            catch (Exception)
            {
                return null;
            }
            
        }
        public BookDto GetBookByOrderDetail(OrderDetailDto orderDetail)
        {
            try
            {
                return orderRepository.GetBookByOrderDetail(orderDetail);
            }
            catch (Exception)
            {
                return null;
            }
            
        }
        public List<BookDto> GetBooksByOrderId(int orderId)
        {
            try
            {
                List<BookDto> books = new List<BookDto>();
                var orderDetails = orderRepository.GetOrderDetailsByOrderId(orderId);
                foreach (var orderDetail in orderDetails)
                {
                    books.Add(orderRepository.GetBookByOrderDetail(orderDetail));
                }
                return books;
            }
            catch (Exception)
            {
                return new List<BookDto>();
            }
            
        }
        public List<OrderDto> GetOrdersByAccountId(int accountId, int statusId)
        {
            try
            {
                return orderRepository.GetOrdersByAccountId(accountId, statusId);
            }
            catch (Exception)
            {
                return new List<OrderDto>();
            }
            
        }

        public string GetStatusByOrder(int orderId)
        {
            try
            {
                return orderRepository.GetStatusByOrder(orderId);
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public List<OrderDetailDto> GetOrderDetailsByOrderId(int orderId)
        {
            try
            {
                return orderRepository.GetOrderDetailsByOrderId(orderId);
            }
            catch (Exception)
            {
                return new List<OrderDetailDto>();
            }
            
        }
        public OrderDetailDto GetOrderDetail(int orderId, int bookId)
        {
            try
            {
                return orderRepository.GetOrderDetail(orderId, bookId);
            }
            catch (Exception)
            {
                return null;
            }
            
        }
        public List<OrderManagementDto> GetAllOrdersForManagement(string province, string priceRange, int statusId, string sortBy)
        {
            try
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
                var orders = orderRepository.GetAllOrders(statusId, sortBy);
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
            catch (Exception)
            {
                return new List<OrderManagementDto>();
            }
            
        }
        public List<OrderManagementDto> GetOrdersByCustomerId(int customerId, string province, string priceRange, int statusId, string sortBy)
        {
            try
            {
                List<OrderDto> orders;
                if (customerId > 0)
                {
                    orders = orderRepository.GetAllOrdersOfCustomer(customerId, statusId, sortBy);
                }
                else
                {
                    orders = orderRepository.GetAllOrders(statusId, sortBy);
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
                            UpdateDate = order.UpdateDate,
                            Address = addressRepository.GetAddressById((int)order.AddressId),
                            TelephoneNumber = order.TelephoneNumber,
                            Price = sum + order.ShipFee,
                            Status = orderRepository.GetStatusByOrderId(order.StatusId)
                        });
                    }

                }

                return list;
            }
            catch (Exception)
            {
                return new List<OrderManagementDto>();
            }
            
        }
        public List<RecentlyOrderDto> GetRecentlyOrders(int count)
        {
            try
            {
                List<OrderDto> orderDtos = orderRepository.GetAllOrders().Take(count).ToList();
                //orderDtos.Reverse();
                List<RecentlyOrderDto> orders = new List<RecentlyOrderDto>();
                foreach (var order in orderDtos)
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
            catch (Exception)
            {
                return new List<RecentlyOrderDto>();
            }
            
        }

        public int RevueneOfMonth(int month, int year) 
        {
            try
            {
                var deliveredOrders = orderRepository.GetDeliveredOrderInMonth(month, year);
                int revuene = 0;
                foreach (var order in deliveredOrders)
                {
                    revuene += orderRepository.GetPriceOfOrder(order.OrderId);
                }
                return revuene;
            }
            catch (Exception)
            {
                return 0;
            }
            
        }

        public void UpdateStatusOrder(int orderId, int statusId)
        {
            try
            {
                orderRepository.UpdateStatusOrder(orderId, statusId);
                if (statusId == 6)
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
                else if (statusId == 2)
                {
                    var orderDetails = orderRepository.GetOrderDetailsByOrderId(orderId);
                    foreach (var orderDetail in orderDetails)
                    {
                        var book = bookRepository.GetBookById(orderDetail.BookId);
                        if (book != null)
                        {
                            bookRepository.UpdateQuantity(book.Id, book.Quantity - orderDetail.Quantity);
                        }
                    }
                }
                switch (statusId)
                {
                    case 2:
                        SendEmail("TADA: Đơn hàng của bạn đã được xác nhận", orderId); break;
                    case 3:
                        SendEmail("TADA: Đơn hàng của bạn đã được giao cho đơn vị vận chuyển", orderId); break;
                    case 4:
                        SendEmail("TADA: Đơn hàng của bạn đã được giao hàng thành công", orderId); break;
                    case 6:
                        SendEmail("TADA: Đơn hàng của bạn đã bị hủy", orderId); break;
                }
            }
            catch (Exception)
            {
            }
            
        }

        private void SendEmail(string subject, int orderId)
        {
            try
            {
                var order = orderRepository.GetOrderById(orderId);
                var customer = customerRepository.GetCustomerById(order.CustomerId);
                var status = statusRepository.GetStatusById(order.StatusId);
                var orderDetails = orderRepository.GetOrderDetailsByOrderId(orderId);
                string gender;
                if (customer.Gender) gender = "anh ";
                else gender = "chị ";
                string to = customer.Email; //To address    
                string from = "shopTADA21@gmail.com"; //From address    
                MailMessage message = new MailMessage(from, to);

                string mailbody = @"<html>" +
                          "<body>" +
                          "<p style=\"font-weight: 600; margin:0; padding:0;\">Chào " + gender + customer.Name + ",</p>" +
                    "<p style=\" margin:0; padding:0;\">TADA cảm ơn " + gender + " rất nhiều vì đã chọn mua hàng ở đây.</p>" +
                    "<p style=\" margin:0; padding:0;\">Đơn hàng của " + gender + " hiện đang chuyển sang trạng thái " +
                    "<span style=\"font-weight: 600; margin:0; padding:0;\">" + status + "</span>" + " vào lúc " + UIHelper.DisplayDateOrder(order.UpdateDate) + "</p>" +
                    "<br><p style=\"font-weight: 600; margin:0; padding:0;\">Chi tiết của đơn hàng bao gồm: </p>" +
                    "<table style=\"border-width:0;\" >" +
                        "<tr>" +
                        "<th style=\"width: 10%\">STT</th>" +
                        "<th style=\"width: 50%\">Tên sách</th>" +
                        "<th style=\"width: 15%\">Đơn giá (VND)</th>" +
                        "<th style=\"width: 10%\">Số lượng</th>" +
                        "<th style=\"width: 15%\">Thành tiền (VND)</th>" +
                        "</tr>";
                for (int i = 0; i < orderDetails.Count; i++)
                {
                    var book = bookRepository.GetBookById(orderDetails[i].BookId);
                    mailbody += "<tr style=\"color: #000\">" +
                        "<th>" + (i + 1) + "</th>" +
                        "<td style=\"text-align:left;\">" + book.Name + "</td>" +
                        "<td style=\"text-align:right;\">" + UIHelper.GetPriceString(orderDetails[i].Price / orderDetails[i].Quantity) + "</td>" +
                        "<td style=\"text-align:center;\">" + orderDetails[i].Quantity + "</td>" +
                        "<td style=\"text-align:right;\">" + UIHelper.GetPriceString(orderDetails[i].Price) + "</td>" +
                        "</tr>";
                }
                mailbody += "</table><div style=\" margin: 16px 0 0; padding:0; width:100%;\"><div style=\" margin:0; padding:0; display:flex; width:100%;\">" +
                    "<div style=\" margin:0; padding:0; width:100px;\">Tổng giá</div>" +
                    "<div style=\" margin:0; padding:0; width:300px;\">" + UIHelper.GetPriceString(orderRepository.GetPriceOfOrder(orderId)) + " VND</div></div>" +
                    "<div style =\" margin:0; padding:0; display:flex; width:100%;\">" +
                    "<div style=\" margin:0; padding:0; width:100px;\">Phí giao hàng</div>" +
                    "<div style=\" margin:0; padding:0; width:300px;\">" + UIHelper.GetPriceString(order.ShipFee) + " VND</div></div>" +
                    "<div style =\" margin:0; padding:0; display:flex; width:100%;\">" +
                    "<div style=\" font-weight: 600; margin:0; padding:0; width:100px;\">Tổng tiền</div>" +
                    "<div style=\" margin:0; padding:0; width:300px;\">" + UIHelper.GetPriceString(orderRepository.GetPriceOfOrder(orderId) + order.ShipFee) + " VND</div></div>" +
                    "</div><br><p style=\" margin:0; padding:0;\">TADA xin chân thành cảm ơn " + gender + " " + customer.Name + " đã mua hàng tại cửa hàng. Nếu có bất cứ vấn đề nào cần giải quyết, " + gender + "hãy liên hệ ngay với chúng tôi để xử lý nhé.</p>" +
                    "<br><p style=\"font-weight: 600;margin:0; padding:0;\"> Thông tin liên hệ:</p>" +
                    "<ul style=\" margin:0; padding:0;\"><li>Website: <a href='35.209.32.94'>TADA Shop</a></li><li>Hotline: 0909999999</li></ul></body></html>";

                message.Subject = subject;
                message.Body = mailbody;
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
                System.Net.NetworkCredential basicCredential1 = new
                System.Net.NetworkCredential("shopTADA21@gmail.com", "drmudtifljijdeke");
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = basicCredential1;
                client.Send(message);
            }
            catch (Exception)
            {
            }
            
        }

        public void UpdateOrder(int orderId, OrderDto orderDto)
        {
            try
            {
                orderRepository.UpdateOrder(orderId, orderDto);
                orderRepository.UpdateOrderShipfee(orderId, CalculateShipping(orderId));
            }
            catch (Exception)
            {
            }
            
        }
        public void DeleteOrder(int orderId)
        {
            try
            {
                var orderDetails = orderRepository.GetOrderDetailsByOrderId(orderId);
                orderRepository.DeleteOrder(orderId);
            }
            catch (Exception)
            {
            }
            
        }

        public void AddOrder(int accountId, List<OrderDetailDto> orderDetails)
        {
            try
            {
                orderRepository.AddOrder(accountId);
                var order = orderRepository.GetOrdersByAccountId(accountId, statusRepository.IdForUserConfirm()).FirstOrDefault();
                foreach (var orderDetail in orderDetails)
                {
                    orderRepository.UpdateOrderDetail(orderDetail.BookId, order.Id, orderDetail.Quantity, orderDetail.Price);
                }
                orderRepository.UpdateOrderShipfee(order.Id, CalculateShipping(order.Id));
            }
            catch (Exception)
            {
            }
            
        }

        public void DeleteOrderDetail(int bookId, int orderId)
        {
            try
            {
                orderRepository.DeleteOrderDetail(bookId, orderId);
            }
            catch (Exception)
            {
            }
            
        }

        public int GetNumOfSoldBooks()
        {
            try
            {
                int numOfSoldBooks = 0;
                var orders = orderRepository.GetOrderGroupByBookId();
                foreach (var order in orders)
                {
                    numOfSoldBooks += order.Quantity;
                }
                return numOfSoldBooks;
            }
            catch (Exception)
            {
                return 0;
            }
            
        }
        public int GetNumOfOrders()
        {
            try
            {
                return orderRepository.GetConfirmedOrderIds().Count;
            }
            catch (Exception)
            {
                return 0;
            }
            
        }
        public int GetNumOfOrdersByYear(int year)
        {
            try
            {
                return orderRepository.GetConfirmedOrderIdsByYear(year).Count;
            }
            catch (Exception)
            {
                return 0;
            }
            
        }
        private int CalculateShipping(int orderId)
        {
            try
            {
                var order = orderRepository.GetOrderById(orderId);
                var orderDetails = orderRepository.GetOrderDetailsByOrderId(orderId);
                var customer = customerRepository.GetCustomerById(order.CustomerId);
                using (var httpClient = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://dev-online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/create"))
                    {
                        request.Headers.TryAddWithoutValidation("ShopId", "121749");
                        request.Headers.TryAddWithoutValidation("Token", "ae3f43bd-b053-11ed-8181-eee966792c8f");

                        var items = new List<Item>();
                        foreach (var orderDetail in orderDetails)
                        {
                            var book = bookRepository.GetBookById(orderDetail.BookId);
                            items.Add(new Item()
                            {
                                name = book.Name,
                                //code = "200",
                                quantity = orderDetail.Quantity,
                                price = orderDetail.Price,
                                length = (int)book.Length,
                                width = (int)book.Width,
                                height = (int)(book.Pages / 2 * 0.01),
                                weight = (int)book.Weight,
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
                            if (item.length > orderLength) orderLength = item.length;
                            if (item.width > orderWidth) orderWidth = item.width;
                            orderHeight += item.height * item.quantity;
                            orderWeight += item.weight * item.quantity;
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
                            client_order_code = "",
                            to_name = customer.Name,
                            to_phone = order.TelephoneNumber,
                            to_address = addressRepository.GetAddressByIdAndPart((int)order.AddressId, 1),
                            to_ward_name = addressRepository.GetAddressByIdAndPart((int)order.AddressId, 2),
                            to_district_name = addressRepository.GetAddressByIdAndPart((int)order.AddressId, 3),
                            to_province_name = addressRepository.GetAddressByIdAndPart((int)order.AddressId, 4),
                            cod_amount = orderPrice,
                            content = "",
                            weight = orderWeight,
                            length = orderLength,
                            width = orderWidth,
                            height = orderHeight,
                            cod_failed_amount = (int)(orderPrice * 0.05),
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
            catch(Exception)
            {
                return 0;
            }
        }
    }
}
