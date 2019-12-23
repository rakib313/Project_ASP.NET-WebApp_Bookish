using Project_ATP2.Models;
using Project_ATP2.Models.CustomModel;
using Project_ATP2.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_ATP2.Controllers
{
    [Authorize(Roles = "Manager")]

    public class AdminAccountController : Controller
    {
        LoginRepository repoLogin = new LoginRepository(new ProjectDBEntities());
        UserRepositoryJahan repoUser = new UserRepositoryJahan(new Models.ProjectDBEntities());
        JahanOrderRepository repoOrder = new JahanOrderRepository(new ProjectDBEntities());
        JahanRepositoryEmployeeDetails eDRepo = new JahanRepositoryEmployeeDetails(new ProjectDBEntities());
#region
        [Authorize (Roles = "Manager")]
        // GET: AdminAccounts
        public ActionResult Index()
        {
            List<User> uList = repoUser.GetAll().ToList<User>();
            List<User> ShowList = new List<User>();
            foreach(User u in  uList)
            {
                if(u.Role_Id == 2 || u.Role_Id == 4)
                {
                    ShowList.Add(u);
                }
            }

            return View(ShowList);
        }

        [HttpGet]
        public ActionResult CustomerManagement()
        {
            User u = new User();
            return View(u);
        }

        [HttpPost]
        public ActionResult CustomerManagementSearch(User c, string searchStr)
        {
            var res = repoLogin.EContext.Users.Where(m => m.Email.Contains(searchStr)).FirstOrDefault();

            return View(res);
        }

        [HttpPost]
        public JsonResult ChangeStatus(Login obj)
        {
            Login l = new Login();
            Login lo = repoLogin.GetAll().Where(m => m.Email == obj.Email).FirstOrDefault();
            lo.Status = obj.Status;
            repoLogin.EContext.SaveChanges();
            return Json("true", JsonRequestBehavior.AllowGet);
        }
#endregion
        [HttpGet]
        public ActionResult EmployeeDetails(int id)
        {
            EmployeeDetails ed = eDRepo.GetByIdSalesman(id);
            //int countDay = 0;
            //foreach (var item in o)
            //{
            //    DateTime date = item.ModifiedDate.ToString("MM/dd/yyyy");
            //    if (DateTime.Now.ToString("MM/dd/yyyy") ==)
            //    {
            //        countDay++;
            //    }

            //}
            //ViewBag.Today = countDay.ToString();
            return View(ed);
        }


        [HttpGet]
        public ActionResult AddEmployee()
        {
            return View(new SignUpEmployee());
        }

        [HttpPost]
        public ActionResult AddEmployee(SignUpEmployee su,HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                string filenameUser;

                if (Image != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(Image.FileName);
                    string extnss = Path.GetExtension(Image.FileName);
                    filename = filename + DateTime.Now.ToString("yymmddfff") + extnss;
                    filenameUser = filename;
                    filename = Path.Combine(Server.MapPath("~/Images/ProfilePicture/"), filename);
                    Image.SaveAs(filename);
                }
                else
                {
                    filenameUser = "default.jpg";
                }
                User u = new User();
                u.Name = su.Name;
                u.Email = su.Email;
                u.Address = su.Address;
                u.DOB = su.DOB.ToString();
                u.PhoneNumber = su.PhoneNumber.ToString();
                u.Role_Id = su.Role_Id;
                u.Image = filenameUser;
                u.AddedDate = DateTime.Now;
                u.ModifiedDate = DateTime.Now;
                repoUser.Insert(u);
                repoUser.EContext.SaveChanges();

                Login l = new Login();
                l.Email = su.Email;
                l.Pass = su.Password;
                l.Role_Id = su.Role_Id;
                l.Status = "APPROVED";
                l.User_Id = u.Id;
                //u = repoUser.GetByEmail(su.Email);
                //if (u != null)
                //    l.User_Id = u.Id;

                repoLogin.Insert(l);
                repoLogin.SaveChanges();

                //Add Employee
                Employee emp = new Employee();
                emp.HireDate = DateTime.Now;
                emp.Salary = su.Salary;
                emp.User_Id = u.Id;
                repoLogin.EContext.Employees.Add(emp);
                repoLogin.EContext.SaveChanges();

                if (su.Role_Id == 4)
                {
                    DeliveryMan d = new DeliveryMan();
                    d.User_Id = u.Id;
                    d.Area = su.Area;
                    repoLogin.EContext.DeliveryMans.Add(d);
                    repoLogin.EContext.SaveChanges();
                }

                //Session["UserEmail"] = su.Email;
                //return Content(su.Password);  

                return RedirectToAction("Index", "AdminAccount");
            }
            ModelState.AddModelError("", "Something went wrong! Please try again");
            return View(su);

        }
        
    }
}
