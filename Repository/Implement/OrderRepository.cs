using Microsoft.EntityFrameworkCore;
using TADA.Dto.Book;
using TADA.Dto.Order;
using TADA.Model;
using TADA.Model.Entity;

namespace TADA.Repository.Implement;

public class OrderRepository : IOrderRepository
{
    private readonly TadaContext context;
    public int LastId { get; set; }
    public OrderRepository(TadaContext context)
    {
        this.context = context;
    }

    public List<OrderDto> GetAllOrdersByCustomerId(int customerId)
    {
        return context.Orders
            .Where(order=> order.CustomerId == customerId)
            .Select(order => new OrderDto
            {
                Id = order.Id,
                TelephoneNumber = order.TelephoneNumber,
                DateOrder = order.DateOrder,
                AddressId = (int)order.AddressId,
                CustomerId = order.CustomerId,
                StatusId = (int)order.StatusId,
            }).ToList();
    }
    public List<OrderDetailDto> GetOrderDetailsByOrder (OrderDto orderDto)
    {
        List<OrderDetailDto> orderDetailDtos = new List<OrderDetailDto>();
        List<OrderDetail> orderDetails = context.Orders.Where(order=>order.Id==orderDto.Id)
            .Select(order=>order.OrderDetails).FirstOrDefault().ToList();
        foreach (OrderDetail orderDetail in orderDetails)
        {
            orderDetailDtos.Add(new OrderDetailDto(orderDetail));
        }
        return orderDetailDtos;
    }

    public List<OrderDto> GetAllOrdersByAccountId(int accountId)
    {
        var customerId= context.Customers.Where(customer => customer.AccountId == accountId)
            .Select(customer => customer.Id).FirstOrDefault();
        return context.Orders
            .Where(order => order.CustomerId == customerId && order.StatusId<6)
            .Select(order => new OrderDto
            {
                Id = order.Id,
                TelephoneNumber = order.TelephoneNumber,
                DateOrder = order.DateOrder,
                AddressId = (int)order.AddressId,
                CustomerId = order.CustomerId,
                StatusId = (int)order.StatusId,
            }).ToList();
    }
    public OrderDto GetOrderById(int orderId)
    {
        var order= context.Orders.Find(orderId);
        return new OrderDto
        {
            Id = order.Id,
            TelephoneNumber = order.TelephoneNumber,
            DateOrder = order.DateOrder,
            AddressId = (int)order.AddressId,
            CustomerId = order.CustomerId,
            StatusId = (int)order.StatusId,
        };
    }

    public BookDto GetBookByOrderDetail(OrderDetailDto orderDetail)
    {
        var book = context.Books.Find(orderDetail.BookId);
        return new BookDto
        {
            Id = book.Id,
            Name = book.Name,
            Author = book.Author,
            Publisher = book.Publisher,
            PublicationYear = book.PublicationYear,
            Genre = book.Genre,
            Pages = book.Pages,
            Length = book.Length,
            Weight = book.Weight,
            Width = book.Width,
            Price = book.Price,
            Cover = book.Cover,
            Quantity = book.Quantity,
            Description = book.Description,
            Image = book.Image,
            Promotion = book.Promotion,
            CategoryId = book.CategoryId,
        };
    }


    public List<OrderDto> GetOrdersByAccountId(int accountId, int statusId)
    {
        if (statusId== 0)
        {
            return GetAllOrdersByAccountId(accountId);
        }
        var customerId = context.Customers.Where(customer => customer.AccountId == accountId)
            .Select(customer => customer.Id).FirstOrDefault();
        var orders = context.Orders
            .Where(order => order.CustomerId == customerId && order.StatusId==statusId)
            .Select(order => order).ToList();
        List<OrderDto> orderDtos= new List<OrderDto>();
        foreach(var order in orders)
        {
            orderDtos.Add(new OrderDto
            {
                Id = order.Id,
                TelephoneNumber = order.TelephoneNumber,
                DateOrder = order.DateOrder,
                AddressId = (int)order.AddressId,
                CustomerId = order.CustomerId,
                StatusId = (int)order.StatusId,
            });
        }
        return orderDtos;
    }

    public string GetStatusByOrder(OrderDto order)
    {
        return context.Statuses.Find(order.StatusId).Name;
    }

    public void DeleteOrder(OrderDto order)
    {
        var orderDel= context.Orders.Find(order.Id);
        if (orderDel != null)
        {
            context.Orders.Remove(orderDel);
            context.SaveChanges();
        }
    }

    public void AddOrder(OrderDto order)
    {
        Order newOrder = new Order
        {
            TelephoneNumber = order.TelephoneNumber,
            DateOrder = order.DateOrder,
            AddressId = order.AddressId,
            CustomerId = order.CustomerId,
            StatusId = order.StatusId,
        };
        context.Orders.Add(newOrder);
        context.SaveChanges();
    }

    public void UpdateStatusOrder(int orderId, int statusId)
    {
        var orderUpdate = context.Orders.Find(orderId);
        if (orderUpdate != null)
        {
            orderUpdate.StatusId= statusId;
            context.SaveChanges();
        }
    }
    public void UpdateBookOrder(OrderDto order, BookDto book, int quantity)
    {
        var orderUpdate = context.Orders.Find(order.Id);
        var bookNew = context.Books.Find(book.Id);
        OrderDetail orderDetail = new OrderDetail
        {
            OrderId = order.Id,
            BookId = book.Id,
            Quantity = quantity,
            Price = book.GetCurrentPrice() * quantity,
        };
        if (orderUpdate != null)
        {
            orderUpdate.OrderDetails.Add(orderDetail);
            bookNew.OrderDetails.Add(orderDetail);
        }

    }

    public void AddOrderDetail(OrderDetailDto orderDetailDto)
    {
        var orderUpdate = context.Orders.Find(orderDetailDto.OrderId);
        var book = context.Books.Find(orderDetailDto.BookId);
        OrderDetail orderDetail = new OrderDetail
        {
            OrderId = orderDetailDto.OrderId,
            BookId = orderDetailDto.BookId,
            Quantity = orderDetailDto.Quantity,
            Price = orderDetailDto.Price,
        };
        if (orderUpdate != null)
        {
            orderUpdate.OrderDetails.Add(orderDetail);
            book.OrderDetails.Add(orderDetail);
        }
    }

    public void UpdateOrderDetail(OrderDetailDto orderDetailDto, int quantity)
    {
        var orderUpdate = context.Orders.Find(orderDetailDto.OrderId);
        var book = context.Books.Find(orderDetailDto.BookId);
        foreach (var orderDetail in orderUpdate.OrderDetails)
        {
            if (orderDetail.BookId == orderDetailDto.BookId)
            {
                orderDetail.Quantity = quantity;
                orderDetail.Price = orderDetailDto.Price / orderDetailDto.Price * quantity;
            }
        }
        foreach (var orderDetail in book.OrderDetails)
        {
            if (orderDetail.OrderId == orderDetailDto.OrderId)
            {
                orderDetail.Quantity = quantity;
                orderDetail.Price = orderDetailDto.Price / orderDetailDto.Price * quantity;
            }
        }
    }

    public void DeleteOrderDetail(OrderDetailDto orderDetailDto)
    {
        var orderUpdate = context.Orders.Find(orderDetailDto.OrderId);
        var book = context.Books.Find(orderDetailDto.BookId);
        OrderDetail orderDetail = new OrderDetail
        {
            OrderId = orderDetailDto.OrderId,
            BookId = orderDetailDto.BookId,
            Quantity = orderDetailDto.Quantity,
            Price = orderDetailDto.Price,
        };
        if (orderUpdate != null)
        {
            orderUpdate.OrderDetails.Remove(orderDetail);
            book.OrderDetails.Remove(orderDetail);
        }
    }
}
