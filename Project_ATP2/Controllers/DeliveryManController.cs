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
    public class DeliveryManController : Controller
    {
        IRepository<OrderData> orderDataRepo = new OrderDataRepository(new ProjectDBEntities());
        IRepository<Order> orderRepo = new OrderRepository(new ProjectDBEntities());
        IRepository<DeliveryMan> deliverymanRepo = new DeliverymanRepository(new ProjectDBEntities());
        IRepository<User> userRepo = new UserRepository(new ProjectDBEntities());
        IRepository<OrderLog> orderLogRepo = new OrderLogsRepository(new ProjectDBEntities());
        IRepository<DeliveryTask> taskRepo = new DeliveryTaskRepository(new ProjectDBEntities());
        IRepository<Stock> stockRepo = new StocksRepository(new ProjectDBEntities());

        [Authorize(Roles = "DeliveryMan")]
        // GET: DeliveryMan
        public ActionResult Index()
        {
            string email = User.Identity.Name;
            int dID = userRepo.GetAll().Where(x => x.Email == email).FirstOrDefault().Id;
            int deliveryManID = deliverymanRepo.GetAll().Where(x => x.User_Id == dID).FirstOrDefault().Id;
            ViewBag.Task = taskRepo.GetAll().Where(x => x.DeliveryMan_Id == deliveryManID).Count();

            return View();
        }

        [Authorize(Roles = "DeliveryMan")]
        [HttpGet]
        public ActionResult Dashboard()
        {
            string email = User.Identity.Name;
            int dID = userRepo.GetAll().Where(x => x.Email == email).FirstOrDefault().Id;
            int deliveryManID = deliverymanRepo.GetAll().Where(x => x.User_Id == dID).FirstOrDefault().Id;

            var tasklist = taskRepo.GetAll().Where(x => x.DeliveryMan_Id == deliveryManID).ToList();

            if (tasklist.Count() == 0)
            {
                return RedirectToAction("index");
            }
            return View(tasklist);

        }
        public ActionResult OrderBookList(int id)
        {
            var list = orderRepo.GetById(id).OrderDatas;
            return View(list);
        }

        //check book stock
        public JsonResult RecieveOrderBook(string s)
        {
            int orderID = Convert.ToInt32(s);

            var list = orderRepo.GetById(orderID).OrderDatas;

            //book recieved and update physical stock
            foreach (var item in list)
            {
                var stock = stockRepo.GetAll().Where(x => x.Book_Id == item.Book_Id).FirstOrDefault();
                stock.PhysicalStock -= item.QuantityOrdered;
                stockRepo.Save();
            }
            // task status
            var task = taskRepo.GetAll().Where(x => x.Order_Id == orderID).FirstOrDefault();
            task.Status = "Recieved";
            taskRepo.Save();

            // add log
            OrderLog log = new OrderLog();
            log.LogDetails = "Recieved";
            log.Order_Id = orderID;
            log.AddedDate = DateTime.Today;
            orderLogRepo.Insert(log);
            orderDataRepo.Save();
            return Json("Book Recieved", JsonRequestBehavior.AllowGet);
        }

        public JsonResult OrderDelivered(string s)
        {
            int orderID = Convert.ToInt32(s);

            //change order status
            var order = orderRepo.GetById(orderID);
            order.DeliveredBy = User.Identity.Name;
            order.Status = "Delivered";
            orderRepo.Save();

            // add log
            OrderLog log = new OrderLog();
            log.LogDetails = "Delivered";
            log.Order_Id = orderID;
            log.AddedDate = DateTime.Now;
            orderLogRepo.Insert(log);
            orderLogRepo.Save();

            // delete task
            DeliveryTask task = taskRepo.GetAll().Where(x => x.Order_Id == orderID).FirstOrDefault();
            taskRepo.Delete(task);
            taskRepo.Save();

            return Json("Order Delivered", JsonRequestBehavior.AllowGet);
        }
        public JsonResult ReturnOrder(string s)
        {
            // change order status
            int orderID = Convert.ToInt32(s);
            var order = orderRepo.GetById(orderID);
            order.Status = "Returned";
            order.DeliveredBy = "";
            orderRepo.Save();

            //delete delivery task
            DeliveryTask task = taskRepo.GetAll().Where(x => x.Order_Id == orderID).FirstOrDefault();
            taskRepo.Delete(task);
            taskRepo.Save();

            //order log update 
            OrderLog log = new OrderLog();
            log.LogDetails = "Returned";
            log.Order_Id = orderID;
            log.AddedDate = DateTime.Now;
            orderLogRepo.Insert(log);
            orderLogRepo.Save();

            var list = orderRepo.GetById(orderID).OrderDatas;
            //book returned and update physical stock
            foreach (var item in list)
            {
                var stock = stockRepo.GetAll().Where(x => x.Book_Id == item.Book_Id).FirstOrDefault();
                stock.PhysicalStock += item.QuantityOrdered;
                stockRepo.Save();
            }

            return Json("Order Returned", JsonRequestBehavior.AllowGet);

        }
        public JsonResult CheckStatus(string s)
        {
            int id = Convert.ToInt32(s);
            var task = taskRepo.GetAll().Where(x => x.Order_Id == id).FirstOrDefault();

            return Json(task.Status.ToString(), JsonRequestBehavior.AllowGet);
        }

    }
}
