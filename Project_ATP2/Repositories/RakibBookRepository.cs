using Project_ATP2.Interfaces;
using Project_ATP2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_ATP2.Repositories
{
    public class RakibBookRepository : Repository<Book>
    {
        public RakibBookRepository(ProjectDBEntities Context) : base(Context)
        {
        }
        //Guys please copy paste the EContext property in all your created repositories
        public ProjectDBEntities EContext
        {
            get { return Context as ProjectDBEntities; }
        }

        public IEnumerable<Book> GetSearchBooksPriceHighToLow(string publisher, string language, string category, string author, string searchStr)
        {
            //return EContext.Posts.Where(m=>m.Title.Contains(searchStr) || m.City.Contains(city) || m.Area.Contains(area)).OrderBy(m=>m.Price).ToList();
            //var r = EContext.Books.Where(m => m.Title.Contains(searchStr) || m.Summary.Contains(searchStr)).ToList();
            //var res = EContext.Books.Where(m => m.Title.Contains(searchStr) || m.Summary.Contains(searchStr)).Where(n => n.Language.Contains(language) || n.Category.Name.Contains(category) || n.Author.Name.Contains(author)).ToList();
            var res1 = EContext.Books.Where(m => m.Title.Contains(searchStr) || m.Summary.Contains(searchStr)).Where(n => n.Language.Contains(language) && n.Category.Name.Contains(category) && n.Author.Name.Contains(author) && n.Publisher.Name.Contains(publisher)).OrderByDescending(x=>x.Price).ToList();
            //var res2 = EContext.Books.Where(m => m.Title.Contains(searchStr) || m.Summary.Contains(searchStr)).Where(n => n.Language.Contains(language) && n.Category.Name.Contains(category)).ToList();
            //var rs = EContext.Books.ToList();
            return res1;
        }

        public IEnumerable<Book> GetSearchBooksPriceLowToHigh(string publisher, string language, string category, string author, string searchStr)
        {
            //return EContext.Posts.Where(m=>m.Title.Contains(searchStr) || m.City.Contains(city) || m.Area.Contains(area)).OrderBy(m=>m.Price).ToList();
            //var r = EContext.Books.Where(m => m.Title.Contains(searchStr) || m.Summary.Contains(searchStr)).ToList();
            //var res = EContext.Books.Where(m => m.Title.Contains(searchStr) || m.Summary.Contains(searchStr)).Where(n => n.Language.Contains(language) || n.Category.Name.Contains(category) || n.Author.Name.Contains(author)).ToList();
            var res1 = EContext.Books.Where(m => m.Title.Contains(searchStr) || m.Summary.Contains(searchStr)).Where(n => n.Language.Contains(language) && n.Category.Name.Contains(category) && n.Author.Name.Contains(author) && n.Publisher.Name.Contains(publisher)).OrderBy(x => x.Price).ToList();
            //var res2 = EContext.Books.Where(m => m.Title.Contains(searchStr) || m.Summary.Contains(searchStr)).Where(n => n.Language.Contains(language) && n.Category.Name.Contains(category)).ToList();
            //var rs = EContext.Books.ToList();
            return res1;
        }

        public IEnumerable<Book> GetSearchBooks(string publisher, string language, string category, string author, string searchStr)
        {
            //return EContext.Posts.Where(m=>m.Title.Contains(searchStr) || m.City.Contains(city) || m.Area.Contains(area)).OrderBy(m=>m.Price).ToList();
            //var r = EContext.Books.Where(m => m.Title.Contains(searchStr) || m.Summary.Contains(searchStr)).ToList();
            //var res = EContext.Books.Where(m => m.Title.Contains(searchStr) || m.Summary.Contains(searchStr)).Where(n => n.Language.Contains(language) || n.Category.Name.Contains(category) || n.Author.Name.Contains(author)).ToList();
            var res1 = EContext.Books.Where(m => m.Title.Contains(searchStr) || m.Summary.Contains(searchStr)).Where(n => n.Language.Contains(language) && n.Category.Name.Contains(category) && n.Author.Name.Contains(author) && n.Publisher.Name.Contains(publisher)).ToList();
            //var res2 = EContext.Books.Where(m => m.Title.Contains(searchStr) || m.Summary.Contains(searchStr)).Where(n => n.Language.Contains(language) && n.Category.Name.Contains(category)).ToList();
            //var rs = EContext.Books.ToList();
            return res1;
        }






    }
}