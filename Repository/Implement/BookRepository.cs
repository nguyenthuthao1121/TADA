﻿using Microsoft.CodeAnalysis.Operations;
using System.Linq;
using TADA.Dto;
using TADA.Dto.Book;
using TADA.Model;
using TADA.Model.Entity;

namespace TADA.Repository.Implement;

public class BookRepository : IBookRepository
{
    private readonly TadaContext context;
    public BookRepository(TadaContext context)
    {
        this.context = context;
    }
    public List<BookDto> GetAllBooks()
    {
        var bookIds = context.Books.Select(p => p.Id).ToList();
        List<BookDto> books = new List<BookDto>();
        foreach(var book in bookIds)
        {
            books.Add(GetBookById(book));
        }
        return books;
    }

    public List<BookDto> GetBooks(int category, string search, string priceRange, string genre, string sortBy)
    {
        if(genre.Equals("All"))
        {
            genre = "";
        }
        int min = 0;
        int max = int.MaxValue;
        var i = priceRange.Split(",");
        try
        {
            min = int.Parse(i[0]);
        }
        catch (Exception)
        {
            min = 0;
        }
        try
        {
            max = int.Parse(i[1]);
        }
        catch (Exception)
        {
            max = int.MaxValue;
        }
        List<int> bookIds = new List<int>();
        List<BookDto> books = new List<BookDto>();
        if (string.IsNullOrWhiteSpace(search))
        {
            if (category != 0)
            {
                switch (sortBy)
                {
                    case "Asc":
                        bookIds = context.Books.Where(x => x.CategoryId == category).Where(x => x.Price <= max && x.Price >= min && x.Genre.Contains(genre)).OrderBy(x => x.Price).Select(p => p.Id).ToList();
                        break;
                    case "Desc":
                        bookIds = context.Books.Where(x => x.CategoryId == category).Where(x => x.Price <= max && x.Price >= min && x.Genre.Contains(genre)).OrderByDescending(x => x.Price).Select(p => p.Id).ToList();
                        break;
                    default:
                        bookIds = context.Books.Where(x => x.CategoryId == category).Where(x => x.Price <= max && x.Price >= min && x.Genre.Contains(genre)).OrderByDescending(x => x.Id).Select(p => p.Id).ToList();
                        break;
                }
            }
            else
            {
                switch (sortBy)
                {
                    case "Asc":
                        bookIds = context.Books.OrderBy(x => x.Price).Where(x => x.Price <= max && x.Price >= min && x.Genre.Contains(genre)).Select(p => p.Id).ToList();
                        break;
                    case "Desc":
                        bookIds = context.Books.OrderByDescending(x => x.Price).Where(x => x.Price <= max && x.Price >= min && x.Genre.Contains(genre)).Select(p => p.Id).ToList();
                        break;
                    default:
                        bookIds = context.Books.OrderByDescending(x => x.Id).Where(x => x.Price <= max && x.Price >= min && x.Genre.Contains(genre)).Select(p => p.Id).ToList();
                        break;
                }
            }
        }
        else
        {
            if (category != 0)
            {
                switch (sortBy)
                {
                    case "Asc":
                        bookIds = context.Books.Where(x => x.CategoryId == category).Where(x => x.Price <= max && x.Price >= min && x.Genre.Contains(genre) && x.Name.Contains(search)).OrderBy(x => x.Price).Select(p => p.Id).ToList();
                        break;
                    case "Desc":
                        bookIds = context.Books.Where(x => x.CategoryId == category).Where(x => x.Price <= max && x.Price >= min && x.Genre.Contains(genre) && x.Name.Contains(search)).OrderByDescending(x => x.Price).Select(p => p.Id).ToList();
                        break;
                    default:
                        bookIds = context.Books.Where(x => x.CategoryId == category).Where(x => x.Price <= max && x.Price >= min && x.Genre.Contains(genre) && x.Name.Contains(search)).OrderByDescending(x => x.Id).Select(p => p.Id).ToList();
                        break;
                }
            }
            else
            {
                switch (sortBy)
                {
                    case "Asc":
                        bookIds = context.Books.OrderBy(x => x.Price).Where(x => x.Price <= max && x.Price >= min && x.Genre.Contains(genre) && x.Name.Contains(search)).Select(p => p.Id).ToList();
                        break;
                    case "Desc":
                        bookIds = context.Books.OrderByDescending(x => x.Price).Where(x => x.Price <= max && x.Price >= min && x.Genre.Contains(genre) && x.Name.Contains(search)).Select(p => p.Id).ToList();
                        break;
                    default:
                        bookIds = context.Books.OrderByDescending(x => x.Id).Where(x => x.Price <= max && x.Price >= min && x.Genre.Contains(genre) && x.Name.Contains(search)).Select(p => p.Id).ToList();
                        break;
                }
            }
        }
        foreach(int id in bookIds)
        {
            books.Add(GetBookById(id));
        }
        return books;
    }
    public List<BookDto> GetBooksForManagement(int category, int provider, string search, int inStock, string sortBy, string sortType)
    {
        var books = GetAllBooks();
        if (category != 0)
        {
            books = books.Where(p => p.CategoryId == category).ToList();
        }
        if (provider != 0)
        {
            books = books.Where(p => p.ProviderId == provider).ToList();
        }
        if (!string.IsNullOrWhiteSpace(search))
        {
            books = books.Where(p => p.Name.Contains(search)).ToList();
        }
        switch (inStock)
        {
            case 0:
                break;
            case 1:
                books =  books.Where(p => p.Quantity > 0).ToList(); break;
            case 2:
                books = books.Where(p => p.Quantity <= 0).ToList(); break;
        }
        switch (sortType)
        {
            case "desc":
                switch (sortBy)
                {
                    case "book":
                        books = books.OrderByDescending(p => p.Name).ToList();
                        break;
                    case "category":
                        books = books.OrderByDescending(p => p.CategoryName).ToList();
                        break;
                    case "provider":
                        books = books.OrderByDescending(p => p.ProviderName).ToList();
                        break;
                    default:
                        books = books.OrderByDescending(p => p.Id).ToList();
                        break;
                }
                break;
            default:
                switch (sortBy)
                {
                    case "book":
                        books = books.OrderBy(p => p.Name).ToList();
                        break;
                    case "category":
                        books = books.OrderBy(p => p.CategoryName).ToList();
                        break;
                    case "provider":
                        books = books.OrderBy(p => p.ProviderName).ToList();
                        break;
                    default:
                        break;
                }
                break;
        }
        return books;
    }
    public BookDto GetBookById(int bookId)
    {
        var book = context.Books.FirstOrDefault(x => x.Id == bookId);
        var entry = context.Entry(book);
        entry.Reference(b => b.Category).Load();
        entry.Reference(b => b.Provider).Load();
        entry.Collection(b => b.Reviews).Load();
        double rating = (book.Reviews.Count() == 0) ? 0 : book.Reviews.Average(x => x.Rating);
        return new BookDto
        {
            Id = book.Id,
            Name = book.Name,
            Author = book.Author,
            Publisher = book.Publisher,
            PublicationYear = book.PublicationYear,
            Genre = book.Genre,
            Pages = book.Pages,
            Length = book.Length,
            Width = book.Width,
            Weight = book.Weight,
            Price = book.Price,
            Cover = book.Cover,
            Quantity = book.Quantity,
            Description = book.Description,
            Image = book.Image,
            Promotion = book.Promotion,
            AverageRating = rating,
            NumberOfReview = book.Reviews.Count(),
            CategoryId = book.CategoryId,
            CategoryName = book.Category.Name,
            ProviderId = book.ProviderId,
            ProviderName = book.Provider.Name
		};
    }
    public int GetNumOfBooksByCategoryId(int categoryId)
    {
        return context.Books.Where(book => book.CategoryId == categoryId).Count();
    }

    public List<BookDto> SearchBooks(string query)
    {
        List<int> bookIds = context.Books.Where(p => p.Name.Contains(query)).Select(p => p.Id).ToList();
        List<BookDto> bookDtos = new List<BookDto>();
        foreach (int id in bookIds)
        {
            bookDtos.Add(GetBookById(id));
        }
        return bookDtos;
    }
    public void UpdateBook(BookDto book)
    {
        var updateBook = context.Books.FirstOrDefault(p => p.Id == book.Id);
        if (updateBook != null)
        {
            updateBook.Name = book.Name;
            updateBook.Author = book.Author;
            updateBook.Publisher = book.Publisher;
            updateBook.PublicationYear = book.PublicationYear;
            updateBook.Genre = book.Genre;
            updateBook.Pages = book.Pages;
            updateBook.Length = book.Length;
            updateBook.Width = book.Width;
            updateBook.Weight = book.Weight;
            updateBook.Price = book.Price;
            updateBook.Cover = book.Cover;
            updateBook.Quantity = book.Quantity;
            updateBook.Promotion = book.Promotion;
            var entry = context.Entry(updateBook);
            entry.Reference(p => p.Provider).Load();
            entry.Reference(p => p.Category).Load();
            updateBook.ProviderId = book.ProviderId;
            updateBook.CategoryId = book.CategoryId;
            context.SaveChanges();
        }
    }


}
