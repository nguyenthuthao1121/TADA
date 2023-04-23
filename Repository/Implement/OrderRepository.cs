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
            .Where(order => order.CustomerId == customerId && order.StatusId < 6)
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
        AddressRepository addressRepository = new AddressRepository(context);

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

    public string GetStatusByOrder(int orderId)
    {
        return context.Statuses.Find((context.Orders.Find(orderId)).StatusId).Name;
    }
    public string GetStatusByOrderId(int orderId)
    {
        return context.Statuses.Where(status => status.Id == orderId).Select(status => status.Name).FirstOrDefault();
    }
    public List<OrderDetailDto> GetOrderDetailsByOrderId(int orderId)
    {
        List<OrderDetailDto> orderDetailDtos = new List<OrderDetailDto>();
        List<OrderDetail> orderDetails = context.OrderDetail
            .Where(orderDetail => orderDetail.OrderId == orderId).ToList();
        foreach (OrderDetail orderDetail in orderDetails)
        {
            orderDetailDtos.Add(new OrderDetailDto
            {
                OrderId = orderDetail.OrderId,
                BookId = orderDetail.BookId,
                Price = orderDetail.Price,
                Quantity= orderDetail.Quantity,
            });
        }
        return orderDetailDtos;
    }

    public void DeleteOrder(int orderId)
    {
        var orderDel= context.Orders.Find(orderId);
        if (orderDel != null)
        {
            var orderDetail = context.OrderDetail.Where(orderDetail => orderDetail.OrderId==orderId).ToList();
            context.OrderDetail.RemoveRange(orderDetail);
            context.Orders.Remove(orderDel);
            context.SaveChanges();
        }
    }

    public void AddOrder(int accountId)
    {
        var customer=context.Customers.Where(customer=>customer.AccountId==accountId).FirstOrDefault();
        var order=context.Orders.Where(order=>order.CustomerId==customer.Id && order.StatusId==6).FirstOrDefault();
        if (order == null)
        {
            Order newOrder = new Order
            {
                TelephoneNumber = customer.TelephoneNumber,
                DateOrder = DateTime.Now,
            };
            context.Orders.Add(newOrder);
            newOrder.AddressId = customer.AddressId;
            newOrder.Address=customer.Address;
            newOrder.CustomerId = customer.Id;
            newOrder.Customer=customer;
            newOrder.StatusId=6;
            newOrder.Status=context.Statuses.Find(6);
            context.SaveChanges();
        }
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
    public void UpdateOrder(int orderId, OrderDto orderDto)
    {
        var order = context.Orders.Find(orderId);
        if (order != null )
        {
            order.TelephoneNumber = orderDto.TelephoneNumber;
            var entry = context.Entry(order);
            entry.Reference(p => p.Address).Load();
            order.AddressId = orderDto.AddressId;
            //order.StatusId = orderDto.StatusId;
            //order.AddressId = orderDto.AddressId;
        }
        context.SaveChanges();
    }

    public void UpdateOrderDetail(int bookId, int orderId, int quantity, int price)
    {
        var orderUpdate = context.Orders.Find(orderId);
        var book = context.Books.Find(bookId);
        var orderDetail = context.OrderDetail
            .Where(orderDetail => orderDetail.OrderId == orderId && orderDetail.BookId == bookId)
            .FirstOrDefault();

        if (orderDetail == null)
        {
            OrderDetail newOrderDetail = new OrderDetail
            {
                Quantity = quantity,
                Price = price,
            };
            context.OrderDetail.Add(newOrderDetail);
            newOrderDetail.OrderId = orderId;
            newOrderDetail.Order = orderUpdate;
            newOrderDetail.BookId= bookId;
            newOrderDetail.Book= book;
        }
        else
        {
            orderDetail.Quantity = quantity;
            orderDetail.Price = price;
        }
        context.SaveChanges();
    }

    public void DeleteOrderDetail(int bookId, int orderId)
    {
        var orderDetail = context.OrderDetail
            .Where(orderDetail => orderDetail.OrderId == orderId && orderDetail.BookId == bookId)
            .FirstOrDefault();
        context.OrderDetail.Remove(orderDetail);
        context.SaveChanges();
    }
}
