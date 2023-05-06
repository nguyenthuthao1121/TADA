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

namespace TADA.Pages;

public class EditBookModel : PageModel
{
    private readonly IBookService bookService;
    private readonly IProviderService providerService;
    private readonly ICategoryService categoryService;

    [BindProperty]
    public BookDto Book { get; set; }

    [BindProperty]
    public string DescriptionText { get; set; }
    public List<ProviderManagementDto> Providers { get; set; }
    public List<CategoryDto> Categories { get; set; }
    [TempData]
    public string SuccessMsg { get; set; }


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
    public IActionResult OnPostChangeBookInformation(IFormFile imageFile)
    {
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
        if (System.IO.File.Exists(imagePath))
        {
            if (imageFile != null)
            {
                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    imageFile.CopyTo(fileStream);
                }
            }
        }

        bookService.UpdateBook(Book);
        return RedirectToPage("./BookManagement");
    }
    public IActionResult OnPostCancelUpdate()
    {
        SuccessMsg = "Cập nhật thành công";
        return RedirectToPage("./BookManagement");
    }
}
