using Microsoft.CodeAnalysis.Elfie.Diagnostics;
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
    public List<OrderDto> GetAllOrders()
    {
        return context.Orders.Select(order => new OrderDto
        {
            Id = order.Id,
            TelephoneNumber = order.TelephoneNumber,
            DateOrder = order.DateOrder,
            AddressId = order.AddressId,
            CustomerId = order.CustomerId,
            StatusId = (int)order.StatusId
        }).ToList();
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
    public string GetStatusByOrderId(int orderId)
    {
        return context.Statuses.Where(status => status.Id == orderId).Select(status => status.Name).FirstOrDefault();
    }
    public List<OrderDetailDto> GetOrderDetailsByOrderId(int orderId)
    {
        List<OrderDetailDto> orderDetailDtos = new List<OrderDetailDto>();
        List<OrderDetail> orderDetails = context.Orders.Where(order => order.Id == orderId)
            .Select(order => order.OrderDetails).FirstOrDefault().ToList();
        foreach (OrderDetail orderDetail in orderDetails)
        {
            orderDetailDtos.Add(new OrderDetailDto(orderDetail));
        }
        return orderDetailDtos;
    }

}
