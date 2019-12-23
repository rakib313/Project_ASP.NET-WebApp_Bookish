using Project_ATP2.Models;
using Project_ATP2.Models.CustomModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_ATP2.Repositories
{
    public class JahanRepositoryEmployeeDetails : Repository<EmployeeDetails>
    {
        public JahanRepositoryEmployeeDetails(ProjectDBEntities Context) : base(Context)
        {
        }
        public ProjectDBEntities EContext
        {
            get { return Context as ProjectDBEntities; }
        }

        public EmployeeDetails GetByIdSalesman(int id)
        {
            UserRepositoryJahan uRepo = new UserRepositoryJahan(new ProjectDBEntities());
            User u = uRepo.GetById(id);
            EmployeeDetails ed = new EmployeeDetails();
            ed.Employee = u;
            int last7 = 0, today = 0;
            JahanOrderRepository oRepo = new JahanOrderRepository(new ProjectDBEntities());
            List<Order> oList;
            if(u.Role_Id == 2)
            {
                oList = oRepo.GetAll().Where(m => m.ProcessedBy == u.Email).ToList();
                ed.Total = oList.Count;
                foreach (Order o in oList)
                {
                    if (o.ModifiedDate >= DateTime.Today.AddDays(-7))
                        last7++;
                    if (o.ModifiedDate == DateTime.Today)
                        today++;
                }
                
            }
            else
            {
                oList = oRepo.GetAll().Where(m => m.Status == "Delivered").ToList();
                ed.Total = oList.Count;
                foreach(Order o in oList)
                {
                    OrderLog ol = o.OrderLogs.OrderByDescending(m => m.Id).FirstOrDefault();
                    if (ol.AddedDate >= DateTime.Today.AddDays(-7))
                        last7++;
                    if (ol.AddedDate == DateTime.Today.Date)
                        today++;
                }
            }
            ed.Last7 = last7;
            ed.Today = today;
            return ed;
        }

        
    }
}