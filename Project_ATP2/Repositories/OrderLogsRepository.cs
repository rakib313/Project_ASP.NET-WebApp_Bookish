using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project_ATP2.Models;
namespace Project_ATP2.Repositories
{
    
    public class OrderLogsRepository : Repository<OrderLog>
    {
        public OrderLogsRepository(ProjectDBEntities Context) : base(Context)
        {

        }
    }
}