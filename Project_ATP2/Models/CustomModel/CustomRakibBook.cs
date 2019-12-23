
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_ATP2.Models.CustomModel
{
    public class CustomRakibBook
    {
        private IEnumerable<Book> bookList;
        private IEnumerable<Author> authorList;
        private IEnumerable<Publisher> publisherList;
        private IEnumerable<Category> categoryList;
        public CustomRakibBook(IEnumerable<Author> authorList, IEnumerable<Publisher> publisherList, IEnumerable<Category> categoryList, IEnumerable<Book> bookList)
        {
            this.bookList = bookList;
            this.authorList = authorList;
            this.publisherList = publisherList;
            this.categoryList = categoryList;
        }
        public IEnumerable<Book> BookList { get { return bookList; } }
        public IEnumerable<Author> AuthorList { get { return authorList; } }
        public IEnumerable<Category> CategoryList { get { return categoryList; } }
        public IEnumerable<Publisher> PublisherList { get { return publisherList; } }
    }
}