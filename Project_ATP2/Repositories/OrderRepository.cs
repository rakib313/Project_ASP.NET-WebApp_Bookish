using Project_ATP2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_ATP2.Repositories
{
    public class OrderRepository : Repository<Order>
    {
        public OrderRepository(ProjectDBEntities Context) : base(Context)
        {

        }
    }
}