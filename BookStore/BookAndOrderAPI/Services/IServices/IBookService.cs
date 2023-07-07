namespace BookAndOrderAPI.Services.IServices
{
    public interface IBookService
    {
        Task<ServiceResponse<Book?>> GetSingleBook(int id);
        Task<ServiceResponse<List<Book>>> GetAllBooks();
        Task<ServiceResponse<List<Book>>> AddBook(BookDTO book);
        Task<ServiceResponse<List<Book>?>> UpdateBook(BookDTO book);
        Task<ServiceResponse<List<Book>>> DeleteBook(int id);
    }
}
