using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TADA.Pages;

public class AddBookModel : PageModel
{
    private readonly ILogger<AddBookModel> _logger;

    public AddBookModel(ILogger<AddBookModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
    public void OnPost()
    {
        var bookName = Request.Form["inputName"];
        Console.WriteLine(bookName);
        RedirectToAction("/Admin/BookManagement");
    }
}
