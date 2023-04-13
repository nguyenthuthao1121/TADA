using Microsoft.EntityFrameworkCore;
using TADA.Dto;
using TADA.Dto.BookDto;
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
            .Select(order => new OrderDto(order)).ToList();
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
        return GetAllOrdersByCustomerId(customerId);
    }


    public BookDto GetBookByOrderDetail(OrderDetailDto orderDetail)
    {
        return new BookDto(context.Books.Find(orderDetail.BookId));
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
            orderDtos.Add(new OrderDto(order));
        }
        return orderDtos;
    }

    public string GetStatusByOrder(OrderDto order)
    {
        return context.Statuses.Find(order.StatusId).Name;
    }

}
