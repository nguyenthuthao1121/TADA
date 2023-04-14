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
    public int TotalNumberOfReview;
    public double AverageRating;
    public double OneStar;
    public double TwoStar;
    public double ThreeStar;
    public double FourStar;
    public double FiveStar;
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
        AverageRating = Math.Round(reviewService.GetAverageRating(10), 1);
        TotalNumberOfReview = Reviews.Count();
        int numberOfOneStar = reviewService.GetNumberOfStar(10, 1);
        int numberOfTwoStar = reviewService.GetNumberOfStar(10, 2);
        int numberOfThreeStar = reviewService.GetNumberOfStar(10, 3);
        int numberOfFourStar = reviewService.GetNumberOfStar(10, 4);
        int numberOfFiveStar = reviewService.GetNumberOfStar(10, 5);
        OneStar = numberOfOneStar == 0 ? 0 : Math.Round(Convert.ToDouble((double)numberOfOneStar / TotalNumberOfReview * 100), 1);
        TwoStar = numberOfTwoStar == 0 ? 0 : Math.Round(Convert.ToDouble((double)numberOfTwoStar / TotalNumberOfReview * 100), 1);
        ThreeStar = numberOfThreeStar == 0 ? 0 : Math.Round(Convert.ToDouble((double)numberOfThreeStar / TotalNumberOfReview * 100), 1);
        FourStar = numberOfFourStar == 0 ? 0 : Math.Round(Convert.ToDouble((double)numberOfFourStar / TotalNumberOfReview * 100), 1);
        FiveStar = numberOfFiveStar == 0 ? 0 : Math.Round(Convert.ToDouble((double)numberOfFiveStar / TotalNumberOfReview * 100), 1);

    }
}
