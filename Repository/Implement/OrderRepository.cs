using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using System.Net;
using System.Runtime.InteropServices;
using System.Globalization;
using TADA.Dto.Book;
using TADA.Dto.Order;
using TADA.Model;
using TADA.Model.Entity;
using static System.Reflection.Metadata.BlobBuilder;

namespace TADA.Repository.Implement;

public class OrderRepository : IOrderRepository
{
    private readonly TadaContext context;
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
            UpdateDate = order.UpdateDate,
            AddressId = order.AddressId,
            CustomerId = order.CustomerId,
            StatusId = (int)order.StatusId,
            ShipFee=order.ShipFee,
        }).OrderByDescending(order=>order.Id).ToList();
    }
    public List<OrderDto> GetAllOrders(int statusId, string sortBy)
    {
        
        List<int> orderIds = new List<int>();
        List<OrderDto> orders = new List<OrderDto>();

        if (statusId == 0)
        {
            switch (sortBy)
            {
                case "Asc":
                    orderIds = context.Orders.OrderBy(x => x.Id).Select(p => p.Id).ToList();
                    break;
                case "Desc":
                    orderIds = context.Orders.OrderByDescending(x => x.Id).Select(p => p.Id).ToList();
                    break;
                default:
                    orderIds = context.Orders.OrderByDescending(x => x.Id).Select(p => p.Id).ToList();
                    break;
            }
        }
        else
        {
            switch (sortBy)
            {
                case "Asc":
                    orderIds = context.Orders.OrderBy(x => x.Id).Where(x => x.StatusId == statusId).Select(p => p.Id).ToList();
                    break;
                case "Desc":
                    orderIds = context.Orders.OrderByDescending(x => x.Id).Where(x => x.StatusId == statusId).Select(p => p.Id).ToList();
                    break;
                default:
                    orderIds = context.Orders.OrderByDescending(x => x.Id).Where(x => x.StatusId == statusId).Select(p => p.Id).ToList();
                    break;
            }
        }
        foreach (int id in orderIds)
        {
            orders.Add(GetOrderById(id));
        }

        return orders;
    }
    public List<OrderDto> GetAllOrdersOfCustomer(int customerId, int statusId, string sortBy)
    {

        List<int> orderIds = new List<int>();
        List<OrderDto> orders = new List<OrderDto>();

        if (statusId == 0)
        {
            switch (sortBy)
            {
                case "Asc":
                    orderIds = context.Orders.OrderBy(x => x.Id).Where(x => x.CustomerId == customerId).Select(p => p.Id).ToList();
                    break;
                case "Desc":
                    orderIds = context.Orders.OrderByDescending(x => x.Id).Where(x => x.CustomerId == customerId).Select(p => p.Id).ToList();
                    break;
                default:
                    orderIds = context.Orders.OrderByDescending(x => x.Id).Where(x => x.CustomerId == customerId).Select(p => p.Id).ToList();
                    break;
            }
        }
        else
        {
            switch (sortBy)
            {
                case "Asc":
                    orderIds = context.Orders.OrderBy(x => x.Id).Where(x => x.StatusId == statusId && x.CustomerId == customerId).Select(p => p.Id).ToList();
                    break;
                case "Desc":
                    orderIds = context.Orders.OrderByDescending(x => x.Id).Where(x => x.StatusId == statusId && x.CustomerId == customerId).Select(p => p.Id).ToList();
                    break;
                default:
                    orderIds = context.Orders.OrderByDescending(x => x.Id).Where(x => x.StatusId == statusId && x.CustomerId == customerId).Select(p => p.Id).ToList();
                    break;
            }
        }

        foreach (int id in orderIds)
        {
            orders.Add(GetOrderById(id));
        }

        return orders;
    }
    public List<OrderDto> GetAllOrdersByCustomerId(int customerId)
    {
        int lastStatusId = context.Statuses.Max(status => status.Id);
        return context.Orders
            .Where(order => order.CustomerId == customerId && order.StatusId < lastStatusId)
            .Select(order => new OrderDto
            {
                Id = order.Id,
                TelephoneNumber = order.TelephoneNumber,
                DateOrder = order.DateOrder,
                UpdateDate = order.UpdateDate,
                AddressId = (int)order.AddressId,
                CustomerId = order.CustomerId,
                ShipFee=order.ShipFee,
                StatusId = (int)order.StatusId,
            }).OrderByDescending(order=>order.Id).ToList();
    }

    public List<OrderDto> GetAllOrdersByAccountId(int accountId)
    {
        int lastStatusId = context.Statuses.Max(status => status.Id);
        var customerId= context.Customers.Where(customer => customer.AccountId == accountId)
            .Select(customer => customer.Id).FirstOrDefault();
        return context.Orders
            .Where(order => order.CustomerId == customerId && order.StatusId< lastStatusId)
            .Select(order => new OrderDto
            {
                Id = order.Id,
                TelephoneNumber = order.TelephoneNumber,
                DateOrder = order.DateOrder,
                UpdateDate = order.UpdateDate,
                AddressId = (int)order.AddressId,
                CustomerId = order.CustomerId,
                ShipFee=order.ShipFee,
                StatusId = (int)order.StatusId,
            }).OrderByDescending(order => order.Id).ToList();
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
            UpdateDate = order.UpdateDate,
            AddressId = (int)order.AddressId,
            CustomerId = order.CustomerId,
            ShipFee=order.ShipFee,
            StatusId = (int)order.StatusId,
            Address= addressRepository.GetAddressById((int)order.AddressId),
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
            .Select(order => order).OrderByDescending(order => order.Id).ToList();
        List<OrderDto> orderDtos= new List<OrderDto>();
        foreach(var order in orders)
        {
            orderDtos.Add(new OrderDto
            {
                Id = order.Id,
                TelephoneNumber = order.TelephoneNumber,
                UpdateDate = order.UpdateDate,
                DateOrder = order.DateOrder,
                AddressId = (int)order.AddressId,
                CustomerId = order.CustomerId,
                ShipFee=order.ShipFee,
                StatusId = (int)order.StatusId,
            });
        }
        return orderDtos;
    }

    public string GetStatusByOrder(int orderId)
    {
        return context.Statuses.Find((context.Orders.Find(orderId)).StatusId).Name;
    }
    public string GetStatusByStatusId(int statusId)
    {
        return context.Statuses.Find(statusId).Name;
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

    public List<int> GetConfirmedOrderIds()
    {
        var statusIds = context.Statuses.Where(status => status.Name != "Chờ xác nhận" && status.Name != "Đã hủy").Select(status => status.Id).ToList();
        return context.Orders.Where(order => statusIds.Contains((int)order.StatusId)).Select(order => order.Id).ToList();
    }
    public List<OrderGroupDto> GetOrderGroupByBookId()
    {
        var ids = GetConfirmedOrderIds();
        return context.OrderDetail.Where(order => ids.Contains(order.OrderId)).GroupBy(order => order.BookId).Select(orderGroup => new OrderGroupDto
        {
            BookId = orderGroup.First().BookId,
            Quantity = orderGroup.Sum(order => order.Quantity)
        }).ToList();
    }
    public int GetPriceOfOrder(int orderId)
    {
        int price = 0;
        foreach (var order in context.OrderDetail)
        {
            if (order.OrderId == orderId)
            {
                price += order.Quantity * order.Price;
            }
        }
        return price;
    }

    public List<OrderOfMonthDto> GetDeliveredOrderInMonth(int month, int year)
    {
        var statusIds = context.Statuses.Where(status => status.Name != "Chờ xác nhận" && status.Name != "Chờ giao hàng" && status.Name != "Đang giao hàng" && status.Name != "Đã hủy").Select(status => status.Id).ToList();
        return context.Orders.Where(order => statusIds.Contains((int)order.StatusId) && order.DateOrder.Month == month && order.DateOrder.Year == year).Select(order => new OrderOfMonthDto()
        {
            OrderId = order.Id,
            StatusId = (int)order.StatusId
        }).ToList();
    }
    public OrderDetailDto GetOrderDetail(int orderId, int bookId)
    {
        var orderDetail = context.OrderDetail.Where(orderDetail=>orderDetail.OrderId== orderId && orderDetail.BookId== bookId).FirstOrDefault();
        return new OrderDetailDto
        {
            OrderId = orderDetail.OrderId,
            BookId = orderDetail.BookId,
            Price = orderDetail.Price,
            Quantity = orderDetail.Quantity,
        };
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
        int lastStatusId = context.Statuses.Max(status => status.Id);
        var customer =context.Customers.Where(customer=>customer.AccountId==accountId).FirstOrDefault();
        var order=context.Orders.Where(order=>order.CustomerId==customer.Id && order.StatusId== lastStatusId).FirstOrDefault();
        if (order == null)
        {
            Order newOrder = new Order
            {
                TelephoneNumber = customer.TelephoneNumber,
                DateOrder = DateTime.Now,
                UpdateDate= DateTime.Now,
            };
            context.Orders.Add(newOrder);
            newOrder.AddressId = customer.AddressId;
            newOrder.Address=customer.Address;
            newOrder.CustomerId = customer.Id;
            newOrder.Customer=customer;
            newOrder.StatusId= lastStatusId;
            newOrder.Status=context.Statuses.Find(lastStatusId);
            context.SaveChanges();
        }
    }

    public void UpdateStatusOrder(int orderId, int statusId)
    {
        var orderUpdate = context.Orders.Find(orderId);
        if (orderUpdate != null)
        {
            orderUpdate.StatusId= statusId;
            orderUpdate.UpdateDate=DateTime.Now;
            context.SaveChanges();
        }
    }
    public void UpdateOrder(int orderId, OrderDto orderDto)
    {
        var order = context.Orders.Find(orderId);
        if (order != null )
        {
            order.TelephoneNumber = orderDto.TelephoneNumber;
            //var entry = context.Entry(order);
           // entry.Reference(p => p.Address).Load();
            order.AddressId = orderDto.AddressId;
            //order.StatusId = orderDto.StatusId;
            //order.AddressId = orderDto.AddressId;
        }
        context.SaveChanges();
    }
    public void UpdateOrderShipfee(int orderId, int fee)
    {
        var order = context.Orders.Find(orderId);
        if (order != null)
        {
            order.ShipFee = fee;
            context.SaveChanges();
        }
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
