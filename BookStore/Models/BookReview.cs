using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BookReview
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int BuyerId { get; set; }
        public DateTime ReviewDate { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public double Rating { get; set; } = 0;
        public string Image { get; set; } = string.Empty;
        public Book Book { get; set; }
    }
}
