using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.NetworkInformation;
using TADA.Dto.Book;
using TADA.Dto.Order;
using TADA.Dto.Review;
using TADA.Model.Entity;
using TADA.Service;
using TADA.Service.Implement;
using TADA.Utilities;

namespace TADA.Pages;

public class EvaluateUserModel : PageModel
{
    private readonly IBookService bookService;
    private readonly IReviewService reviewService;
    private readonly IOrderService orderService;

    public EvaluateUserModel(IBookService bookService, IReviewService reviewService, IOrderService orderService)
    {
        this.bookService = bookService;
        this.reviewService = reviewService;
        this.orderService = orderService;
    }

    public OrderDto Order { get; set; }
    public List<OrderDetailDto> OrderDetails { get; set; }
    [BindProperty]
    public int Rating { get; set; } = 5;
    [BindProperty]
    public string Comment { get; set; }
    public BookDto GetBookByOrderDetail(OrderDetailDto orderDetail)
    {
        return orderService.GetBookByOrderDetail(orderDetail);
    }
    public int SumPriceOfBooks()
    {
        int sum = 0;
        foreach (var orderDetail in OrderDetails)
        {
            sum += orderDetail.Price;
        }
        return sum;
    }
    public int SumPriceOfOrder()
    {
        return SumPriceOfBooks() + Order.ShipFee;
    }
    public void OnGet()
    {
        if (int.TryParse(Request.Query["id"], out int orderId))
        {
            Order=orderService.GetOrderById(orderId);
            OrderDetails=orderService.GetOrderDetailsByOrderId(orderId);
            
        }
    }
    public IActionResult OnPostAddReview(IFormFile imageFile, int? bookId, int? orderId)
    {
        Directory.CreateDirectory("wwwroot/img/books/book" + (int)bookId + "/reviews");
        string imagePath = "wwwroot/img/books/book" + (int)bookId + "/reviews/review.jpg";
        if (imageFile != null)
        {
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                imageFile.CopyTo(fileStream);
            }
            imagePath = "~/img/books/book" + (int)bookId + "/reviews";
        }
        else imagePath = "";
        reviewService.AddReview(new ReviewDto
        {
            Comment = Comment,
            Rating = Rating,
            DateReview = DateTime.Now,
            Image = imagePath,
            CustomerId = 1,
            BookId = (int)bookId,
        });
        orderService.UpdateStatusOrder((int)orderId, 4);
        return RedirectToPage("OrderListFillReview", new {area="Order"});
    }
}
