using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TADA.Dto.Book;
using TADA.Service;

namespace TADA.Pages;

public class BookManagementModel : PageModel
{
    private readonly IBookService bookService;
    public List<BookManagementDto> Books { get; set; }
    public BookManagementModel(IBookService bookService)
    {
        this.bookService = bookService;
    }

    public void OnGet()
    {
        Books = bookService.GetAllBooksForManagement();
    }
}
