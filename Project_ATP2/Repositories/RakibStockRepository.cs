using Project_ATP2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_ATP2.Repositories
{
    public class RakibStockRepository : Repository<Stock>
    {
        public RakibStockRepository(ProjectDBEntities Context) : base(Context)
        {
        }
        public ProjectDBEntities EContext
        {
            get { return Context as ProjectDBEntities; }
        }

        public Stock GetStockByBookId(int bookId)
        {
            var res = EContext.Stocks.Where(m => m.Book_Id == bookId).FirstOrDefault();
            return res;
        }
    }
}