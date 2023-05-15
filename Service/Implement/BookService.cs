using TADA.Dto.Book;
using TADA.Model.Entity;
using TADA.Repository;
using TADA.Utilities;

namespace TADA.Service.Implement;

public class BookService : IBookService
{
    private readonly IBookRepository bookRepository;
    private readonly ICategoryRepository categoryRepository;
    private readonly IProviderRepository providerRepository;
    public BookService(IBookRepository bookRepository, ICategoryRepository categoryRepository, IProviderRepository providerRepository)
    {
        this.bookRepository = bookRepository;
        this.categoryRepository = categoryRepository;
        this.providerRepository = providerRepository;
    }
    public List<BookDto> GetAllBooks()
    {
        return bookRepository.GetAllBooks();
    }
    public List<BookDto> GetBooks(int category, string search, string priceRange, string genre, string sortBy)
    {
        List<BookDto> list = new List<BookDto>();
        foreach(BookDto book in bookRepository.GetBooks(category, priceRange, genre, sortBy))
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                list.Add(book);
            }
            else
            {
                if ((UIHelper.RemoveUnicodeSymbol(book.Name)).Contains(UIHelper.RemoveUnicodeSymbol(search)))
                {
                    list.Add(book);
                }
            }
        }
        return list;
    }

    public BookDto GetBookById(int id)
    {
        return bookRepository.GetBookById(id);
    }
    public List<BookManagementDto> GetAllBooksForManagement()
    {
        List<BookManagementDto> list = new List<BookManagementDto>();
        var books = bookRepository.GetAllBooks();
        foreach(var book in books)
        {
            list.Add(new BookManagementDto
            {
                Id = book.Id,
                ISBN= book.ISBN,
                Name = book.Name,
                Author = book.Author,
                Publisher = book.Publisher,
                Price = book.Price,
                Promotion = book.Promotion,
                Quantity = book.Quantity,
                Image = book.Image,
                Category = categoryRepository.GetCategoryNameById(book.CategoryId),
                Provider = book.ProviderName
            });
        }
        return list;
    }
    public List<BookManagementDto> GetBooksForManagement(int category, int provider, string search, int inStock, string sortBy, string sortType)
    {
        List<BookManagementDto> list = new List<BookManagementDto>();
        var books = bookRepository.GetBooksForManagement(category, provider, inStock, sortBy, sortType);
        if (string.IsNullOrWhiteSpace(search))
        {
            foreach (var book in books)
            {
                list.Add(new BookManagementDto
                {
                    Id = book.Id,
                    ISBN = book.ISBN,
                    Name = book.Name,
                    Author = book.Author,
                    Publisher = book.Publisher,
                    Price = book.Price,
                    Promotion = book.Promotion,
                    Quantity = book.Quantity,
                    Image = book.Image,
                    Category = categoryRepository.GetCategoryNameById(book.CategoryId),
                    Provider = book.ProviderName
                });
            }
        }
        else
        {
            foreach (var book in books)
            {
                if ((UIHelper.RemoveUnicodeSymbol(book.Name)).Contains(UIHelper.RemoveUnicodeSymbol(search)))
                {
                    list.Add(new BookManagementDto
                    {
                        Id = book.Id,
                        ISBN= book.ISBN,
                        Name = book.Name,
                        Author = book.Author,
                        Publisher = book.Publisher,
                        Price = book.Price,
                        Promotion = book.Promotion,
                        Quantity = book.Quantity,
                        Image = book.Image,
                        Category = categoryRepository.GetCategoryNameById(book.CategoryId),
                        Provider = book.ProviderName
                    });
                }
            }
        }
        return list;
    }
    public void UpdateQuantity(int bookId, int quantity)
    {
        bookRepository.UpdateQuantity(bookId, quantity);
    }
    public int AddBook(BookDto book)
    {
        return bookRepository.AddBook(book);
    }
    public List<BookDto> SearchBooks(string query)
    {
        return bookRepository.SearchBooks(query);
    }
    public List<SoldBookDto> GetSoldBooks()
    {
        return bookRepository.GetSoldBooks();
    }

    public void UpdateBook(BookDto book)
    {
        bookRepository.UpdateBook(book);
    }
    public void HideBook(int bookId)
    {
        bookRepository.HideBook(bookId);
    }
    public void DisplayBook(int bookId)
    {
        bookRepository.DisplayBook(bookId);
    }
}
