using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TADA.Dto.Book;
using TADA.Dto.Review;
using TADA.Model.Entity;
using TADA.Service;
using TADA.Service.Implement;
using TADA.Middleware;

namespace TADA.Pages;

[Authorize("Quản trị viên", "Nhân viên bán hàng")]
public class BookDetailAdminModel : PageModel
{
    private readonly IBookService bookService;
    private readonly IReviewService reviewService;

    public BookDto Book { get; set; }
    [TempData]
    public int BookId { get; set; }
    [TempData]
    public string SuccessMsg { get; set; }
    public List<ReviewDto> Reviews { get; set; }

    public double OneStar;
    public double TwoStar;
    public double ThreeStar;
    public double FourStar;
    public double FiveStar;
    [BindProperty]
    public string Quantity { get; set; }
    public BookDetailAdminModel(IBookService bookService, IReviewService reviewService)
    {
        this.bookService = bookService;
        this.reviewService = reviewService;
    }
    public void OnGet()
    {
        if (int.TryParse(Request.Query["id"], out int bookId))
        {
            Book = bookService.GetBookById(bookId);
            BookId = bookId;
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
            SuccessMsg = Request.Query["message"];
        }

    }
    [HttpGet]
    public IActionResult OnGetHideBook()
    {
        if (int.TryParse(Request.Query["itemId"], out int itemId))
        {
            bookService.HideBook(itemId);
        }
        return RedirectToPage("./BookManagement");
    }
    [HttpGet]
    public IActionResult OnGetDisplayBook()
    {
        if (int.TryParse(Request.Query["itemId"], out int itemId))
        {
            bookService.DisplayBook(itemId);
        }
        return RedirectToPage("./BookManagement");
    }

}

