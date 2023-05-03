using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TADA.Dto.Book;
using TADA.Dto.Provider;
using TADA.Dto.Review;
using TADA.Service;

namespace TADA.Pages;

public class EditBookModel : PageModel
{
    private readonly IBookService bookService;
    private readonly IProviderService providerService;

    [BindProperty]
    public BookDto Book { get; set; }
    [BindProperty]
    public string DescriptionText { get; set; }
    [BindProperty]
    public string ImagePath { get; set; }
    public List<ProviderManagementDto> Providers { get; set; }

    public EditBookModel(IBookService bookService, IProviderService providerService)
    {
        this.bookService = bookService;
        this.providerService = providerService;
    }
    public void OnGet()
    {
        Providers = providerService.GetAllProviders();
        if (int.TryParse(Request.Query["id"], out int bookId))
        {
            Book = bookService.GetBookById(bookId);
            DescriptionText = "";
            var filePath = Book.Description;
            if (System.IO.File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    DescriptionText = sr.ReadToEnd();
                    sr.Close();
                }
            }
        }

    }
    public IActionResult OnPostChangeBookInformation()
    {
        return RedirectToPage("BookDetailAdmin");
    }
}
