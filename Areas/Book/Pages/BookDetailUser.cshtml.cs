using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TADA.Dto;
using TADA.Dto.BookDto;
using TADA.Service;

namespace TADA.Pages;

public class BookDetailUserModel : PageModel
{
    private readonly IBookService bookService;
    private readonly IReviewService reviewService;

    public BookDto Book { get; set; }
    public List<ReviewDto> Reviews { get; set; }

    public string Username;
    public BookDetailUserModel(IBookService bookService, IReviewService reviewService)
    {
        this.bookService = bookService;
        this.reviewService = reviewService;
    }
    public void OnGet()
    {
        Username = HttpContext.Session.GetString("Name");
        Book = bookService.GetBookById(10);
        Reviews = reviewService.GetReviewsByBookId(10);
    }
}
