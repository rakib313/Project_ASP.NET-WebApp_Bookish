using Project_ATP2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_ATP2.Repositories
{
    public class RakibCartRepository : Repository<Cart>
    {
        public RakibCartRepository(ProjectDBEntities Context) : base(Context)
        {
        }
        public ProjectDBEntities EContext
        {
            get { return Context as ProjectDBEntities; }
        }

        public void AddCart(Cart cart)
        {
            bool result = EContext.Carts.Any(m => m.Book_Id == cart.Book_Id && m.User_Id == cart.User_Id);

            if (!result)
            {
                EContext.Carts.Add(cart);
                EContext.SaveChanges();
            }
        }

        public IEnumerable<Cart> GetCartByUserId(int userId)
        {
            var result = EContext.Carts.Where(m => m.User_Id == userId).ToList();
            return result;
        }

        
    }
}