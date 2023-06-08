using System.Drawing.Printing;
using System.Globalization;
using System.Net;
using TADA.Dto.Book;
using TADA.Model.Entity;
using TADA.Repository;
using TADA.Utilities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        try
        {
            return bookRepository.GetAllBooks();
        }
        catch (Exception)
        {
            return new List<BookDto>();
        }
    }
    public List<BookDto> GetBooks(int category, string search, string priceRange, string genre, string sortBy)
    {
        try
        {
            List<BookDto> list = new List<BookDto>();
            foreach (BookDto book in bookRepository.GetBooks(category, priceRange, genre, sortBy))
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
        catch(Exception)
        {
            return new List<BookDto>();
        }
    }

    public BookDto GetBookById(int id)
    {
        try
        {
            return bookRepository.GetBookById(id);
        }
        catch(Exception)
        {
            return null;
        }
    }
    public List<BookManagementDto> GetAllBooksForManagement()
    {
        try
        {
            List<BookManagementDto> list = new List<BookManagementDto>();
            var books = bookRepository.GetAllBooks();
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
            return list;
        }
        catch (Exception)
        {
            return new List<BookManagementDto>();
        }
        
    }
    public List<BookManagementDto> GetBooksForManagement(int category, int provider, string search, int inStock, string sortBy, string sortType)
    {
        try
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
            }
            return list;
        }
        catch (Exception)
        {
            return new List<BookManagementDto>();
        }
        
    }
    public void UpdateQuantity(int bookId, int quantity)
    {
        try
        {
            bookRepository.UpdateQuantity(bookId, quantity);
        }
        catch(Exception) { }
    }
    public int AddBook(BookDto book)
    {
        try
        {
            return bookRepository.AddBook(book);
        }
        catch(Exception) 
        {
            return 0;
        }
    }
    public List<BookDto> SearchBooks(string query)
    {
        try
        {
            return bookRepository.SearchBooks(query);
        }
        catch (Exception)
        {
            return new List<BookDto>();
        }
        
    }
    public List<SoldBookDto> GetSoldBooks()
    {
        try
        {
            return bookRepository.GetSoldBooks();
        }
        catch (Exception)
        {
            return new List<SoldBookDto>();
        }
        
    }

    public void UpdateBook(BookDto book)
    {
        try
        {
            bookRepository.UpdateBook(book);
        }
        catch (Exception) { }
        
    }
    public void HideBook(int bookId)
    {
        try
        {
            bookRepository.HideBook(bookId);
        }
        catch (Exception) { }
       
    }
    public void DisplayBook(int bookId)
    {
        try
        {
            bookRepository.DisplayBook(bookId);
        }
        catch (Exception) { }
        
    }
}
