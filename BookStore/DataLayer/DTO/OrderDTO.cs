using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public double DeliveryFee { get; set; } = 0;
        public double TotalAmount { get; set; } = 0;
        public int BuyerId { get; set; }
        public string DeliveryAddress { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<BookDTO> Books { get; set; } = new List<BookDTO>();
    }
}
