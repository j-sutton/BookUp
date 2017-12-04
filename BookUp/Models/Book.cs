using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookUp.Models
{
    public class Book
    {
        public string Title { get; set; }
        public List<string> Authors { get; set; }
    }
}