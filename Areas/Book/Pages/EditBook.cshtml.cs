using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TADA.Dto.Book;
using TADA.Dto.Category;
using TADA.Dto.Provider;
using TADA.Dto.Review;
using TADA.Model.Entity;
using TADA.Service;
using System.IO;
using static System.Reflection.Metadata.BlobBuilder;
using TADA.Middleware;
using Newtonsoft.Json;
using System.Net;
using Microsoft.AspNetCore.Html;

namespace TADA.Pages;

[Authorize("Quản trị viên", "Nhân viên bán hàng")]
public class EditBookModel : PageModel
{
    private readonly IBookService bookService;
    private readonly IProviderService providerService;
    private readonly ICategoryService categoryService;

    [BindProperty]
    public BookDto Book { get; set; }

    [BindProperty]
    public string DescriptionText { get; set; } = "";
    public List<ProviderManagementDto> Providers { get; set; }
    public List<CategoryDto> Categories { get; set; }
    [TempData]
    public string SuccessMsg { get; set; } = null;
    public string ISBNMessage { get; set; } = null;

    public EditBookModel(IBookService bookService, IProviderService providerService, ICategoryService categoryService)
    {
        this.bookService = bookService;
        this.providerService = providerService;
        this.categoryService = categoryService;
    }
    public void OnGet()
    {
        Providers = providerService.GetAllProviders();
        Categories= categoryService.GetAllCategories();
        
        if (TempData["Book"] != null)
        {
            Book = JsonConvert.DeserializeObject<BookDto>(TempData["Book"] as string);
            DescriptionText = TempData["DescriptionText"] as string;
            DescriptionText = DescriptionText.Substring(1, DescriptionText.Length - 2);
            DescriptionText = DescriptionText.Replace("\\n", "\n");
            DescriptionText = DescriptionText.Replace("\\r", "\r");
            ISBNMessage = "Mã sách đã tồn tại";
        }
        else if (int.TryParse(Request.Query["id"], out int bookId))
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
    public IActionResult OnPostChangeBookInformation(IFormFile imageFile)
    {
        if (bookService.GetBookByISBN(Book.ISBN) == null || bookService.GetBookById(Book.Id).ISBN == Book.ISBN)
        {
            Directory.CreateDirectory("wwwroot/img/books/book" + Book.Id + "/cover-img");
            string descriptionPath = "wwwroot/img/books/book" + Book.Id + "/description.txt";
            if (System.IO.File.Exists(descriptionPath))
            {
                using (StreamWriter sw = new StreamWriter(descriptionPath))
                {
                    DescriptionText = DescriptionText.Replace("<br>", "\n");
                    sw.Write(DescriptionText);
                    sw.Close();
                }
            }
            string imagePath = "wwwroot/img/books/book" + Book.Id + "/cover-img/cover.jpg";
            if (imageFile != null)
            {
                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    imageFile.CopyTo(fileStream);
                }
            }
            bookService.UpdateBook(Book);
            //SuccessMsg = "The information was successfully updated.";
            return RedirectToPage("BookDetailAdmin", new { area = "Book", id=Book.Id, message = "IsUpdated" });
        }
        else
        {
            TempData["Book"] = JsonConvert.SerializeObject(Book);
            //if (!string.IsNullOrEmpty(DescriptionText)) DescriptionText = DescriptionText.Replace("<br>", "\n");
            //else DescriptionText = "";
            if (string.IsNullOrEmpty(DescriptionText)) DescriptionText = "";
            TempData["DescriptionText"] = JsonConvert.SerializeObject(DescriptionText);
            return RedirectToPage("EditBook", new {id=Book.Id});
        }
    }

}
