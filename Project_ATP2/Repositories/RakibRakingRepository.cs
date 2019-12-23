using Project_ATP2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project_ATP2.Repositories
{
    public class RakibRakingRepository : Repository<Rating>
    {
        public RakibRakingRepository(ProjectDBEntities Context) : base(Context)
        {
        }
        public ProjectDBEntities EContext
        {
            get { return Context as ProjectDBEntities; }
        }

        public void AddRating(Rating obj)
        {
            //EContext.Ratings.Add(obj);
            //EContext.SaveChanges();
            //EContext.Entry(user).State = EntityState.Modified;
            //EContext.SaveChanges();
            bool isValid = EContext.Ratings.Any(m=>m.User_Id == obj.User_Id && m.Book_Id == obj.Book_Id);

            if(isValid)
            {
                var r = EContext.Ratings.Where(m => m.User_Id == obj.User_Id && m.Book_Id == obj.Book_Id).FirstOrDefault();

                r.Review = obj.Review;
                r.Stars = obj.Stars;
                
                EContext.SaveChanges();
            }
            else
            {
                EContext.Ratings.Add(obj);
                EContext.SaveChanges();
            }
        }

        

        public IEnumerable<Rating> GetRateLitByBookId(int bookId)
        {
            var res = EContext.Ratings.Where(m=>m.Book_Id == bookId).ToList();
            return res;
        }
    }
}