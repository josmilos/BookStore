using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public double Price { get; set; } = 0;
        public int PageNumber { get; set; } = 0;
        public string Writing { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public double Rating { get; set; } = 0;
    }
}
