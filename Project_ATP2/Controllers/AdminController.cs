using Project_ATP2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_ATP2.Models;
using Project_ATP2.Models.CustomModel;
using System.IO;

namespace Project_ATP2.Controllers
{
    public class AdminController : Controller
    {

        RakibBookRepository bRepo = new RakibBookRepository(new Models.ProjectDBEntities());
        RakibAuthorRepository aRepo = new RakibAuthorRepository(new Models.ProjectDBEntities());
        RakibOrderRepository oRepo = new RakibOrderRepository(new ProjectDBEntities());
        RakibOrderDataRepository odRepo = new RakibOrderDataRepository(new ProjectDBEntities());
        RakibOrderLogRepository olRepo = new RakibOrderLogRepository(new ProjectDBEntities());
        
        [Authorize(Roles = "Manager")]
        // GET: Admin
        public ActionResult Index()
        {
            RAdminDashboard dash = new RAdminDashboard();
            List<Order> oList = oRepo.GetAll().Where(o => o.AddedDate == DateTime.Today).ToList();
            List<Order> oList2 = oRepo.GetAll().ToList();
            List<Customer> cList = oRepo.EContext.Customers.ToList();
            int todayMoney = 0;
            foreach(Order o in oList)
            {
                foreach(OrderData od in o.OrderDatas)
                {
                    todayMoney += (int)od.ActualPrice;
                }
            }
            dash.TodayMoney = todayMoney;
            dash.TodayOrder = oList.Where(o=>o.Status == "Delivered").ToList().Count;
            dash.TodayOrderPlaced = oList.Where(o=>o.Status == "Pending").ToList().Count;
            dash.TotalCustomer = cList.Count;
            int totalMoney = 0;
            foreach (Order o in oList2)
            {
                foreach (OrderData od in o.OrderDatas)
                {
                    totalMoney += (int)od.ActualPrice;
                }
            }
            dash.TotalMoneyearned = totalMoney;
            dash.TotalOrderComplete = oList2.Where(o=>o.Status == "Delivered").ToList().Count;
            List<Book> blist = bRepo.GetAll().ToList();
            List<OrderData> odList = odRepo.GetAll().ToList();
            List<int> bCount = new List<int>();
            foreach(Book b in blist)
            {
                bCount.Add( odList.Where(od => od.Book == b).ToList().Count);
            }
            bCount.Sort();
            List<Book> showBook = new List<Book>();
            int i = 0, j=bCount.Count - 1;
            while(i<5)
            {
                foreach (Book b in blist)
                {
                    if(odList.Where(od => od.Book == b).ToList().Count == bCount[j])
                    {
                        showBook.Add(b);
                    }
                }
                j--;
                i++;
            }
            dash.TopBooks = showBook.Take(5).ToList();
            return View(dash);
        }

        [HttpGet]
        public ActionResult Books()
        {
            return View(bRepo.GetAll());
        }

        [HttpGet]
        public ActionResult CreateAuthor()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAuthor(Author au, HttpPostedFileBase image)
        {
            if(au.Name != null)
            {
                if(image != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(image.FileName);
                    string extnss = Path.GetExtension(image.FileName);
                    filename = filename + DateTime.Now.ToString("yymmddfff") + extnss;
                    au.Image = filename;
                    filename = Path.Combine(Server.MapPath("~/Image/AuthorPicture/"), filename);
                    image.SaveAs(filename);
                }
            }
            Repository<Author> aRepo = new Repository<Author>(new ProjectDBEntities());
            List<Author> aList = aRepo.GetAll().ToList();
            aList.Add(au);
            aRepo.SaveChanges(); // save changes hoitece na
            return RedirectToAction("Books");
        }

        [HttpGet]
        public ActionResult CreateCategory()
        {

            return View();
        }

        [HttpPost]
        public ActionResult CreateCategory(Category ct)
        {
            
            return RedirectToAction("Books");
        }

        [HttpGet]
        public ActionResult CreateBook()
        {
            Book b = new Book();
            //b.Author = aRepo.GetAll;
            return View(b);
        }

        [HttpPost]
        public ActionResult CreateBook(Book b, HttpPostedFileBase image)
        {
            if(b.Title != null && b.Price != null)
            {
                if(image != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(image.FileName);
                    string extnss = Path.GetExtension(image.FileName);
                    filename = filename + DateTime.Now.ToString("yymmddfff") + extnss;
                    b.Image = filename;
                    filename = Path.Combine(Server.MapPath("~/Image/"), filename);
                    image.SaveAs(filename);
                }
                bRepo.Insert(b);
                bRepo.SaveChanges();
            }
            return RedirectToAction("Books");
        }
        [HttpGet]
        public ActionResult EmployeeManagement()
        {
            return RedirectToAction("Index", "AdminAccount");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            return View();
        }
    }
}
