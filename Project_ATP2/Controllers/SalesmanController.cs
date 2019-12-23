using Project_ATP2.Interfaces;
using Project_ATP2.Models;
using Project_ATP2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_ATP2.Controllers
{
    public class SalesmanController : Controller
    {
        IRepository<OrderData> orderDataRepo = new OrderDataRepository(new ProjectDBEntities());
        IRepository<Order> orderRepo = new OrderRepository(new ProjectDBEntities());
        IRepository<Book> bookRepo = new RakibBookRepository(new ProjectDBEntities());
        IRepository<Stock> stockRepo = new StocksRepository(new ProjectDBEntities());
        IRepository<OrderLog> orderLogRepo = new OrderLogsRepository(new ProjectDBEntities());

        //IRepository<Login> loginRepo = new LoginRepository(new ProjectDBEntities());

        [Authorize(Roles = "Salesman")]
        public ActionResult Index()
        {
            int count = orderDataRepo.GetAll().Where(x => x.Order.Status == "Pending" || x.Order.Status == "Returned").Count();
            if (count == 0)
            {
                TempData["ex"] = "0";
            }
            else
            {
                TempData["ex"] = count.ToString();
            }
            return View();
        }

        [Authorize(Roles = "Salesman")]
        [HttpGet]
        public ActionResult Dashboard()
        {

            Order data = orderRepo.GetAll().Where(x => x.Status == "Pending" || x.Status == "Returned").FirstOrDefault();

            double TotalPrice = 0;
            foreach (var item in data.OrderDatas)
            {
                TotalPrice += item.ActualPrice;
            }
            ViewBag.Price = TotalPrice;

            if (data == null)
            {

                return RedirectToAction("Index", "Salesman");
            }

            //var stock = stockRepo.GetAll().Where(x => x.Book_Id == data.Book_Id).FirstOrDefault();
            //ViewBag.AvailableStock = stock.OrderStock;

            //double price = Convert.ToDouble(data.Book.Price);
            //double discount = Convert.ToDouble(data.Book.DiscountRate);
            //double ActualPrice = price - ((discount * price) / 100.0);

            int CuponDiscount;
            if (data.Coupon_Id != null)
            {
                CuponDiscount = data.Coupon.Percentage;
                ViewBag.Cupon = CuponDiscount;
            }
            if (ViewBag.Cupon == null)
            {
                ViewBag.Cupon = 0;
            }
            //ViewBag.BookPrice = ActualPrice;

            //List<string> AreaList = new List<string>();
            //AreaList.Add("Gulshan");
            //AreaList.Add("Banani");
            //AreaList.Add("Dhanmondi");
            //AreaList.Add("Nikunjo");
            //AreaList.Add("Badda");
            //AreaList.Add("Basundhara");
            ViewBag.Area = AvailableArea.AllArea();

            return View(data);
        }

        [Authorize(Roles = "Salesman")]
        [HttpPost]
        public ActionResult Dashboard(FormCollection collection)
        {
            int id = Convert.ToInt32(collection["XD"]);
            string name = collection["CustomerName"];
            string phone = collection["CustomerPhone"];
            string area = collection["CustomerArea"];
            string address = collection["CustomerAddress"];


            //OrderData orderdata = orderDataRepo.GetById(id);
            //orderdata.QuantityOrdered = od.QuantityOrdered;
            //orderdata.Subtotal = od.Subtotal;
            //orderdata.ActualPrice = od.ActualPrice;
            //orderDataRepo.Save();

            //change status,process by status,modified date,
            Order order = orderRepo.GetById(id);
            order.Name = name;
            order.PhoneNumber = phone;
            order.Area = area;
            order.Address = address;

            order.ProcessedBy = User.Identity.Name;
            order.Status = "Confirmed";
            DateTime dateTime = DateTime.Today;
            order.ModifiedDate = dateTime;
            orderRepo.Save();

            // add log
            OrderLog log = new OrderLog();
            log.Order_Id = id;
            log.LogDetails = "Confirmed";
            log.AddedDate = dateTime;
            orderLogRepo.Insert(log);
            orderLogRepo.Save();


            // update book quantity
            foreach (var item in order.OrderDatas)
            {
                var stock = stockRepo.GetAll().Where(x => x.Book_Id == item.Book_Id).FirstOrDefault();
                stock.OrderStock -= item.QuantityOrdered;
                stockRepo.Save();
            }

            return RedirectToAction("Index");
        }
        public JsonResult Cancel(string s)
        {
            int id = Convert.ToInt32(s);
            var order = orderRepo.GetById(id);
            order.Status = "Rejected";
            orderRepo.Save();

            return Json("Order Canceled", JsonRequestBehavior.AllowGet);
        }

    }
}
