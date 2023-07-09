namespace BookAndOrderAPI.Services.IServices
{
    public interface IBookService
    {
        Task<ServiceResponse<BookDTO>> GetSingleBook(int id);
        Task<ServiceResponse<List<BookDTO>>> GetAllBooks();
        Task<ServiceResponse<List<BookDTO>>> AddBook(BookDTO book);
        Task<ServiceResponse<List<BookDTO>?>> UpdateBook(BookDTO book);
        Task<ServiceResponse<List<BookDTO>>> DeleteBook(int id);
    }
}
