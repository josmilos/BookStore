using AutoMapper;
using BookAndOrderAPI.Services.IServices;
using DataLayer.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BookAndOrderAPI.Services
{
    public class OrderService : IOrderService
    {

        private readonly IMapper _mapper;
        private readonly StoreDbContext _dbContext;

        public OrderService(IMapper mapper, StoreDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<ServiceResponse<List<OrderDTO>>> AddOrder(OrderDTO newOrder)
        {
            var serviceResponse = new ServiceResponse<List<OrderDTO>>();

            if(newOrder == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Server received invalid request!";
                return serviceResponse;
            }

            #region DeliveryFeeValidation
            if(newOrder.DeliveryFee < 0)
            {
                serviceResponse.Success = false;
                serviceResponse.Message += "Delivery fee can not have negative value!";
            }
            #endregion DeliveryFeeValidation

            #region TotalAmountValidation
            if(newOrder.TotalAmount < 0)
            {
                serviceResponse.Success = false;
                serviceResponse.Message += "Total amount can not have negative value!";
            }
            #endregion TotalAmountValidation

            #region BuyerIdValidation
            // TO DO AFTER IMPLEMENTING USER DATABASE
            #endregion BuyerIdValidation

            #region DeliveryAddressValidation
            if (newOrder.DeliveryAddress == string.Empty || newOrder.DeliveryAddress == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Delivery address field can not be left empty!";
            }
            else if (newOrder.DeliveryAddress.Length < 5)
            {
                serviceResponse.Success = false;
                serviceResponse.Message += "Minimum length of address is 5 characters including numbers";
            }
            else if(newOrder.DeliveryAddress.Length > 100)
            {
                serviceResponse.Success = false;
                serviceResponse.Message += "Maximum length of address is 100 characters including numbers";
            }
            #endregion DeliveryAddressValidation

            #region CommentValidation
            // NOTHING TO VALIDATE
            #endregion CommentValidation

            #region OrderDateValidation
            if (DateTime.Compare(newOrder.OrderDate, DateTime.Now) > 0)
            {
                serviceResponse.Success = false;
                serviceResponse.Message += "Order date date can only be in the past";
            }
            #endregion OrderDateValidation

            #region
            #endregion

            if (serviceResponse.Success is false)
            {
                return serviceResponse;
            }
            else
            {
                Order order = _mapper.Map<Order>(newOrder);
                List<BookDTO> orderedBooks = newOrder.Books;
                List<Book> dbBooks = await _dbContext.Books.ToListAsync();

                foreach(BookDTO book in orderedBooks)
                {
                    Book bk = _dbContext.Books.FirstOrDefault(x => x.Id == book.Id);
                    if (bk == null)
                    {
                        serviceResponse.Success = false;
                        serviceResponse.Message = "Book " + book.Title + " from author " + book.Author + " does not exist in database.";
                        return serviceResponse;
                    }
                    else if(bk.Quantity < book.Quantity)
                    {
                        serviceResponse.Success = false;
                        serviceResponse.Message = "Book " + book.Title + " from author " + book.Author + " has only " + bk.Quantity + " available units.";
                        return serviceResponse;
                    }
                }

                foreach(BookDTO book in orderedBooks)
                {
                    BookOrder bookOrder = new BookOrder() { OrderId = newOrder.Id, BookId = book.Id, Quantity = book.Quantity, Order = order };
                    _dbContext.BookOrders.Add(bookOrder);
                    order.BookOrders.Add(bookOrder);
                    Book dbBook = await _dbContext.Books.FindAsync(book.Id);
                    dbBook.Quantity = dbBook.Quantity - book.Quantity;
                    await _dbContext.SaveChangesAsync();
                }
                _dbContext.Orders.Add(order);
                await _dbContext.SaveChangesAsync();
            }
            
            List<OrderDTO> returnOrders = _mapper.Map<List<OrderDTO>>(await _dbContext.Orders.ToListAsync());
            serviceResponse.Data = returnOrders;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<OrderDTO>>> DeleteOrder(int id)
        {
            var serviceResponse = new ServiceResponse<List<OrderDTO>>();
            var order = await _dbContext.Orders.FindAsync(id);
            if(order != null)
            {
                _dbContext.Orders.Remove(order);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Order with ID " + id + " does not exist!";
            }
            List<OrderDTO> returnOrders = _mapper.Map<List<OrderDTO>>(await _dbContext.Orders.ToListAsync());
            serviceResponse.Data = returnOrders;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<OrderDTO>>> GetAllOrders()
        {
            var serviceResponse = new ServiceResponse<List<OrderDTO>>();
            List<OrderDTO> returnOrders = _mapper.Map<List<OrderDTO>>(await _dbContext.Orders.ToListAsync());
            serviceResponse.Data = returnOrders;
            return serviceResponse;
        }

        public async Task<ServiceResponse<OrderDTO>> GetSingleOrder(int id)
        {
            var serviceResponse = new ServiceResponse<OrderDTO>();
            var order = await _dbContext.Orders.FindAsync(id);
            if (order == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Order with ID " + id + " does not exist!";
                return serviceResponse;
            }
            serviceResponse.Data = _mapper.Map<OrderDTO>(order);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<OrderDTO>?>> UpdateOrder(OrderDTO order)
        {
            throw new NotImplementedException();
        }
    }
}
