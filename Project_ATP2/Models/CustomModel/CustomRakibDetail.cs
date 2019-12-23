using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_ATP2.Models.CustomModel
{
    public class CustomRakibDetail
    {
        public Book Book { get; private set; }
        public IEnumerable<Rating> Ratings { get; private set; }

        public CustomRakibDetail(IEnumerable<Rating> ratings, Book book)
        {
            this.Book = book;
            this.Ratings = ratings;
        }    
    }
}