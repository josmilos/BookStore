using BookAndOrderAPI.Services.IServices;

namespace BookAndOrderAPI.Services
{
    public class BookService : IBookService
    {
        public async Task<List<Book>> AddBook(BookDTO book)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Book>> DeleteBook(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Book>> GetAllBooks()
        {
            throw new NotImplementedException();
        }

        public async Task<Book?> GetSingleBook(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Book>?> UpdateBook(BookDTO book)
        {
            throw new NotImplementedException();
        }
    }
}
