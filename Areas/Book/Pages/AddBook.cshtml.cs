using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using TADA.Dto.Book;
using TADA.Dto.Category;
using TADA.Dto.Provider;
using TADA.Service;
using TADA.Middleware;
using Newtonsoft.Json;

namespace TADA.Pages;

[Authorize("Quản trị viên", "Nhân viên bán hàng")]
public class AddBookModel : PageModel
{
    private readonly IBookService bookService;
    private readonly IProviderService providerService;
    private readonly ICategoryService categoryService;

    public AddBookModel(IBookService bookService, IProviderService providerService, ICategoryService categoryService)
    {
        this.bookService = bookService;
        this.providerService = providerService;
        this.categoryService = categoryService;
    }
    [BindProperty]
    public BookDto Book { get; set; }
    [BindProperty]
    public string DescriptionText { get; set; }
    public List<ProviderManagementDto> Providers { get; set; }
    public List<CategoryDto> Categories { get; set; }
    public string ISBNMessage { get; set; } = null;
    public void OnGet()
    {
        Providers = providerService.GetAllProviders();
        Categories= categoryService.GetAllCategories();
        if (TempData["Book"]!=null)
        {
            Book = JsonConvert.DeserializeObject<BookDto>(TempData["Book"] as string);
            DescriptionText = TempData["DescriptionText"] as string;
            DescriptionText = DescriptionText.Substring(1, DescriptionText.Length - 2);
            DescriptionText = DescriptionText.Replace("\\n", "\n");
            DescriptionText = DescriptionText.Replace("\\r", "\r");
            ISBNMessage = "Mã sách đã tồn tại";
        }
    }
    
    public IActionResult OnPostAddNewBook(IFormFile imageFile)
    {
        int bookId = bookService.AddBook(Book);
        if(bookId == 0)
        {
            TempData["Book"] = JsonConvert.SerializeObject(Book);
            if (string.IsNullOrEmpty(DescriptionText)) DescriptionText = "";
            TempData["DescriptionText"] = JsonConvert.SerializeObject(DescriptionText);
            return RedirectToPage("AddBook");
        }
        else
        {
            Directory.CreateDirectory("wwwroot/img/books/book" + bookId + "/cover-img");
            string descriptionPath = "wwwroot/img/books/book" + bookId + "/description.txt";
            using (StreamWriter sw = new StreamWriter(descriptionPath))
            {
                DescriptionText = DescriptionText.Replace("<br>", "\n");
                sw.Write(DescriptionText);
                sw.Close();
            }
            string imagePath = "wwwroot/img/books/book" + bookId + "/cover-img/cover.jpg";
            if (imageFile != null)
            {
                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    imageFile.CopyTo(fileStream);
                }
            }
            return RedirectToPage("BookManagement", new { area = "Book" });
        }
    }
}
