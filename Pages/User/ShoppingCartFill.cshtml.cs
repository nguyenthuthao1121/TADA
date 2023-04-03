using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TADA.Dto;
using TADA.Service;

namespace TADA.Pages;

public class ShoppingCartFillModel : PageModel
{
    private readonly ILogger<ShoppingCartFillModel> _logger;
    private readonly IBookService bookService;

    public List<BookDto> Books { get; set; }

    public ShoppingCartFillModel(ILogger<ShoppingCartFillModel> logger, IBookService bookService)
    {
        _logger = logger;
        this.bookService = bookService;
    }

    public void OnGet()
    {
        Books=bookService.GetAllBook();
    }
}
