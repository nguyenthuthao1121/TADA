using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.Metadata;
using TADA.Dto.Book;
using TADA.Dto.Order;
using TADA.Dto.Review;
using TADA.Model.Entity;
using TADA.Service;

namespace TADA.Pages;

public class BookDetailUserModel : PageModel
{
    private readonly IBookService bookService;
    private readonly IReviewService reviewService;
    private readonly ICartService cartService;
    private readonly IOrderService orderService;

    public BookDto Book { get; set; }
    public List<ReviewDto> Reviews { get; set; }

    public double OneStar;
    public double TwoStar;
    public double ThreeStar;
    public double FourStar;
    public double FiveStar;
    [BindProperty]
    public string Quantity { get; set; }
    public string Message { get; set; } = string.Empty;
    public BookDetailUserModel(IBookService bookService, IReviewService reviewService, ICartService cartService, IOrderService orderService)
    {
        this.bookService = bookService;
        this.reviewService = reviewService;
        this.cartService = cartService;
        this.orderService = orderService;
    }
    public void OnGet()
    {
        if (int.TryParse(Request.Query["id"], out int bookId))
        {
            Book = bookService.GetBookById(bookId);
            Reviews = reviewService.GetReviewsByBookId(bookId);
            int numberOfOneStar = reviewService.GetNumberOfStar(bookId, 1);
            int numberOfTwoStar = reviewService.GetNumberOfStar(bookId, 2);
            int numberOfThreeStar = reviewService.GetNumberOfStar(bookId, 3);
            int numberOfFourStar = reviewService.GetNumberOfStar(bookId, 4);
            int numberOfFiveStar = reviewService.GetNumberOfStar(bookId, 5);
            OneStar = numberOfOneStar == 0 ? 0 : Math.Round(Convert.ToDouble((double)numberOfOneStar / Book.NumberOfReview * 100), 1);
            TwoStar = numberOfTwoStar == 0 ? 0 : Math.Round(Convert.ToDouble((double)numberOfTwoStar / Book.NumberOfReview * 100), 1);
            ThreeStar = numberOfThreeStar == 0 ? 0 : Math.Round(Convert.ToDouble((double)numberOfThreeStar / Book.NumberOfReview * 100), 1);
            FourStar = numberOfFourStar == 0 ? 0 : Math.Round(Convert.ToDouble((double)numberOfFourStar / Book.NumberOfReview * 100), 1);
            FiveStar = numberOfFiveStar == 0 ? 0 : Math.Round(Convert.ToDouble((double)numberOfFiveStar / Book.NumberOfReview * 100), 1);
            if (!string.IsNullOrEmpty(Request.Query["message"])) Message = Request.Query["message"];
        }
        
    }
    public IActionResult OnPostAddToCart(int? id)
    {
        if (id != null && HttpContext.Session.GetInt32("Id")!=null)
        {
            cartService.AddBookToCart((int)id, (int)HttpContext.Session.GetInt32("Id"), Convert.ToInt32(Quantity));
            return RedirectToPage("BookDetailUser",new {id=id});
        }
        else
        {
            return RedirectToPage("Login", new {area="Authentication"});
        }
    }
    public IActionResult OnPostOrderNow(int? id)
    {
        if (id != null && HttpContext.Session.GetInt32("Id") != null)
        {
            var orders = orderService.GetOrdersByAccountId((int)HttpContext.Session.GetInt32("Id"), 6);
            if (orders != null)
            {
                foreach (var order in orders)
                {
                    orderService.DeleteOrder(order.Id);
                }
            }
            var bookOrder=bookService.GetBookById((int)id);
            if (bookOrder.Quantity>= Convert.ToInt32(Quantity))
            {
                List<OrderDetailDto> orderDetailDtos = new List<OrderDetailDto>();
                orderDetailDtos.Add(new OrderDetailDto
                {
                    OrderId = 1,
                    BookId = (int)id,
                    Quantity = Convert.ToInt32(Quantity),
                    Price = bookOrder.GetCurrentPrice() * Convert.ToInt32(Quantity),
                });
                orderService.AddOrder((int)HttpContext.Session.GetInt32("Id"), orderDetailDtos);
                return RedirectToPage("ConfirmPackage", new { area = "Order" });
            }
            else
            {
                Message = "11";
                return RedirectToPage("BookDetailUser",new { id = id });
            }
                
        }
        else
        {
            return RedirectToPage("Login", new { area = "Authentication" });
        }
    }
}
