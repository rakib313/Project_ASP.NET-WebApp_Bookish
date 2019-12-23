using Project_ATP2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_ATP2.Repositories
{
    public class RakibReportRepository : Repository<Report>
    {
        public RakibReportRepository(ProjectDBEntities Context) : base(Context)
        {
        }
        public ProjectDBEntities EContext
        {
            get { return Context as ProjectDBEntities; }
        }

        public void AddReport(Report rep)
        {
            EContext.Reports.Add(rep);
            EContext.SaveChanges();
        }
    }
}