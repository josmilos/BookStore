namespace BookAndOrderAPI.Services.IServices
{
    public interface IBookService
    {
        Task<Book?> GetSingleBook(int id);
        Task<List<Book>> GetAllBooks();
        Task<List<Book>> AddBook(BookDTO book);
        Task<List<Book>?> UpdateBook(BookDTO book);
        Task<List<Book>> DeleteBook(int id);
    }
}
