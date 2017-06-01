using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetFines
{
    public class HistoryViewModel
    {
        public int HistoryId { get; set; }
        public string Brand { get; set; }
        public string Desctiption { get; set; }
        public decimal Price { get; set; }
        public bool State { get; set; }
    }
}