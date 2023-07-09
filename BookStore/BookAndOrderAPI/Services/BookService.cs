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
        public async Task<ServiceResponse<List<BookDTO>>> AddBook(BookDTO book)
        {
            var serviceResponse = new ServiceResponse<List<BookDTO>>();

            if (book == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Server received invalid request!";
                return serviceResponse;
            }
            #region TitleValidation
            if (book.Title == string.Empty || book.Title == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Book title field can not be left empty!";
            }
            else if(book.Title.Length >= 101)
            {
                serviceResponse.Success = false;
                serviceResponse.Message += "Book title has maximum possible length of 100 characters!";
            }
            #endregion TitleValidation
            #region DescriptionValidation
            if (book.Description == string.Empty || book.Description == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message += "Book description field can not be left empty!";
            }
            else if(book.Description.Length >= 501)
            {
                serviceResponse.Success = false;
                serviceResponse.Message += "Book description has maximum possible length of 500 characters!";
            }
            #endregion DescriptionValidation
            #region AuthorValidation
            if (book.Author == string.Empty || book.Author == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Book author field can not be left empty!";
            }
            else if (!Regex.IsMatch(book.Author, @"^([a-zA-Z]{2,}\s[a-zA-Z]{1,}'?-?[a-zA-Z]{2,}\s?([a-zA-Z]{1,})?)"))
            {
                serviceResponse.Success = false;
                serviceResponse.Message += "Book author has invalid format!";
            }
            else if(book.Author.Length >= 101)
            {
                serviceResponse.Success = false;
                serviceResponse.Message += "Book author has maximum possible length of 100 characters!";
            }
            #endregion AuthorValidation
            #region PriceValidation
            if (book.Price <= 0 || book.Price >= 1000)
            {
                serviceResponse.Success = false;
                serviceResponse.Message += "Book price has maximum possible price of 1000 and can not be negative or zero!";
            }
            #endregion PriceValidation
            #region PageNumberValidation
            if (book.PageNumber <= 0 || book.PageNumber >= 9999)
            {
                serviceResponse.Success = false;
                serviceResponse.Message += "Book has maximum possible pages number of 9999 and can not be negative or zero!";
            }
            #endregion PageNumberValidation
            #region WritingValidation
            if (book.Writing == string.Empty || book.Writing == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message += "Book writing field can not be left empty!";
            }
            #endregion WritingValidation
            #region ReleaseDateValidation
            if (DateTime.Compare(book.ReleaseDate, DateTime.Now) > 0)
            {
                serviceResponse.Success = false;
                serviceResponse.Message += "Book release date can only be in the past";
            }
            #endregion ReleaseDateValidation
            #region ISBNValidation
            if (book.ISBN == string.Empty || book.ISBN == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message += "Book ISBN field can not be left empty!";
            }
            else
            {
                Book existingBook = _dbContext.Books.FirstOrDefault(x => x.ISBN == book.ISBN);
                Console.WriteLine(existingBook);
                if (existingBook != null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message += "Book ISBN must be unique, book with this ISBN already exists!";
                }
            }
            #endregion ISBNValidation

            if(serviceResponse.Success is false)
            {
                return serviceResponse;
            }

            Book insertBook = _mapper.Map<Book>(book);
            _dbContext.Books.Add(insertBook);
            await _dbContext.SaveChangesAsync();
            List<BookDTO> returnBooks = _mapper.Map<List<BookDTO>>(await _dbContext.Books.ToListAsync());
            serviceResponse.Data = returnBooks;

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<BookDTO>>> DeleteBook(int id)
        {
            var serviceResponse = new ServiceResponse<List<BookDTO>>();
            var book = await _dbContext.Books.FindAsync(id);
            if(book != null)
            {
                _dbContext.Books.Remove(book);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Book with ID " + id + " does not exist!";
            }
            List<BookDTO> returnBooks = _mapper.Map<List<BookDTO>>(await _dbContext.Books.ToListAsync());
            serviceResponse.Data = returnBooks;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<BookDTO>>> GetAllBooks()
        {
            var serviceResponse = new ServiceResponse<List<BookDTO>>();
            List<BookDTO> returnBooks = _mapper.Map<List<BookDTO>>(await _dbContext.Books.ToListAsync());
            serviceResponse.Data = returnBooks;
            return serviceResponse;
        }

        public async Task<ServiceResponse<BookDTO>> GetSingleBook(int id)
        {
            var serviceResponse = new ServiceResponse<BookDTO>();
            var book = await _dbContext.Books.FindAsync(id);
            if (book == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Book with ID " + id + " does not exist!";
                return serviceResponse;
            }
            serviceResponse.Data = _mapper.Map<BookDTO>(book); 
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<BookDTO>?>> UpdateBook(BookDTO book)
        {
            var serviceResponse = new ServiceResponse<List<BookDTO>>();
            var updatingBook = await _dbContext.Books.FindAsync(book.Id);
            if(updatingBook != null)
            {
                if(book.Title != string.Empty && book.Title != null)
                {
                    if(book.Title.Length < 101)
                    {
                        updatingBook.Title = book.Title;
                    }
                    else
                    {
                        serviceResponse.Success = false;
                        serviceResponse.Message = "Book title has maximum possible length of 100 characters!";
                    }
                    
                }

                if(book.Description != string.Empty && book.Description != null)
                {
                    if (book.Description.Length < 500)
                    {
                        updatingBook.Description = book.Description;
                    }
                    else
                    {
                        serviceResponse.Success = false;
                        serviceResponse.Message += "Book description has maximum possible length of 500 characters!";
                    }
                }

                if (book.Author != string.Empty && book.Author != null)
                {
                    if (!Regex.IsMatch(book.Author, @"^([a-zA-Z]{2,}\s[a-zA-Z]{1,}'?-?[a-zA-Z]{2,}\s?([a-zA-Z]{1,})?)"))
                    {
                        serviceResponse.Success = false;
                        serviceResponse.Message += "Book author has invalid format!";
                    }
                    else if (book.Author.Length >= 101)
                    {
                        serviceResponse.Success = false;
                        serviceResponse.Message += "Book author has maximum possible length of 100 characters!";
                    }
                    else
                    {
                        updatingBook.Author = book.Author;
                    }
                    
                }

                if (book.Price < 0 || book.Price >= 1000)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message += "Book price has maximum possible price of 1000 and can not be negative or zero!";
                }
                else if(book.Price != 0)
                {
                    updatingBook.Price = book.Price;
                }

                if (book.PageNumber < 0 || book.PageNumber >= 10000)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message += "Book has maximum possible pages number of 9999 and can not be negative or zero!";
                }
                else if (book.PageNumber != 0)
                {
                    updatingBook.PageNumber = book.PageNumber;
                }

                if (book.Writing != string.Empty && book.Writing != null)
                {

                    updatingBook.Writing = book.Writing;
                }

                if (DateTime.Compare(book.ReleaseDate, DateTime.Now) > 0)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message += "Book release date can only be in the past";
                }

                await _dbContext.SaveChangesAsync();
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Book with ID " + book.Id + " does not exist!";
            }
            List<BookDTO> returnBooks = _mapper.Map<List<BookDTO>>(await _dbContext.Books.ToListAsync());
            serviceResponse.Data = returnBooks;
            return serviceResponse;
             
        }
    }
}
