using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Order
    { 
        public int Id { get; set; }
        public double DeliveryFee { get; set; } = 0;
        public double TotalAmount { get; set; } = 0;
        public int BuyerId { get; set; }
        public string DeliveryAddress { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public  DateTime DeliveryDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<BookOrder> BookOrders { get; set; } = new List<BookOrder>();
    }
}
