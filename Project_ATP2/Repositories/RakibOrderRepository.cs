using Project_ATP2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_ATP2.Repositories
{
    public class RakibOrderRepository : Repository<Order>
    {
        public RakibOrderRepository(ProjectDBEntities Context) : base(Context)
        {
        }
        public ProjectDBEntities EContext
        {
            get { return Context as ProjectDBEntities; }
        }

        public Order GetLastEntryOfUser(int userId)
        {
            var result = EContext.Orders.Where(m => m.User_Id == userId).OrderByDescending(m=>m.Id).FirstOrDefault();
            return result;
        }

        internal IEnumerable<Order> GetOrderByUserId(int userId)
        {
            var result = EContext.Orders.Where(m => m.User_Id == userId).ToList();
            return result;
        }
    }
}