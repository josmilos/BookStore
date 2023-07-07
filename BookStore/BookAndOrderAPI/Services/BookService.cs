using AutoMapper;
using BookAndOrderAPI.Services.IServices;
using DataLayer.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Text.RegularExpressions;

namespace BookAndOrderAPI.Services
{
    public class BookService : IBookService
    {
        private readonly IMapper _mapper;
        private readonly StoreDbContext _dbContext;

        public BookService(IMapper mapper, StoreDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<ServiceResponse<List<Book>>> AddBook(BookDTO book)
        {
            if (book == null)
            {
                // return error
            }
            #region TitleValidation
            if (book.Title == string.Empty || book.Title == null)
            {
                // return error
            }
            else if(book.Title.Length >= 100)
            {
                //return error
            }
            #endregion TitleValidation
            #region DescriptionValidation
            if (book.Description == string.Empty || book.Description == null)
            {
                // return error
            }
            else if(book.Description.Length >= 500)
            {
                // return error
            }
            #endregion DescriptionValidation
            #region AuthorValidation
            if (book.Author == string.Empty || book.Author == null)
            {
                // return error
            }
            else if (!Regex.IsMatch(book.Author, @"^([a-zA-Z]{2,}\s[a-zA-Z]{1,}'?-?[a-zA-Z]{2,}\s?([a-zA-Z]{1,})?)"))
            {
                // return error 
            }
            else if(book.Author.Length >= 99)
            {
                // return error
            }
            #endregion AuthorValidation
            #region PriceValidation
            if (book.Price <= 0 || book.Price >= 1000)
            {
                // return error
            }
            #endregion PriceValidation
            #region PageNumberValidation
            if (book.PageNumber <= 0 || book.PageNumber >= 9999)
            {
                // return error
            }
            #endregion PageNumberValidation
            #region WritingValidation
            if (book.Writing == string.Empty || book.Writing == null)
            {
                // return error
            }
            #endregion WritingValidation
            #region ReleaseDateValidation
            if (DateTime.Compare(book.ReleaseDate, DateTime.Now) > 0)
            {
                // return error
            }
            #endregion ReleaseDateValidation
            #region ISBNValidation
            if (book.ISBN == string.Empty || book.ISBN == null)
            {
                var existingBook = _dbContext.Books.Where(x => x.ISBN == book.ISBN);
                if(existingBook != null)
                {
                    // return error
                }

            }
            #endregion ISBNValidation

            Book insertBook = _mapper.Map<Book>(book);
            _dbContext.Books.Add(insertBook);
            await _dbContext.SaveChangesAsync();

            //return await _dbContext.Books.ToListAsync();
        }

        public async Task<ServiceResponse<List<Book>>> DeleteBook(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<Book>>> GetAllBooks()
        {
            var books = await _dbContext.Books.ToListAsync();
            return books;
        }

        public async Task<ServiceResponse<Book?>> GetSingleBook(int id)
        {
            var book = await _dbContext.Books.FindAsync(id);
            if(book == null)
            {
                return null;
            } 
            return book;
        }

        public async Task<ServiceResponse<List<Book>?>> UpdateBook(BookDTO book)
        {
            throw new NotImplementedException();
        }
    }
}
