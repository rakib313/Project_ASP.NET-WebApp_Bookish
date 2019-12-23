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
    public class ShippingManagerController : Controller
    {
        IRepository<OrderData> orderDataRepo = new OrderDataRepository(new ProjectDBEntities());
        IRepository<Order> orderRepo = new OrderRepository(new ProjectDBEntities());
        IRepository<DeliveryMan> deliverymanRepo = new DeliverymanRepository(new ProjectDBEntities());
        IRepository<User> userRepo = new UserRepository(new ProjectDBEntities());
        IRepository<OrderLog> orderLogRepo = new OrderLogsRepository(new ProjectDBEntities());
        IRepository<DeliveryTask> taskRepo = new DeliveryTaskRepository(new ProjectDBEntities());


        [Authorize(Roles = "ShippingManager")]
        // GET: ShippingManager
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "ShippingManager")]
        [HttpGet]
        public ActionResult Dashboard()
        {
            ViewBag.Area = AvailableArea.AllArea();
            var orderlist = orderRepo.GetAll().Where(x => x.Status == "Confirmed").ToList();
            return View(orderlist);
        }

        public JsonResult GetOrderData(string s)
        {
            var list = orderRepo.GetAll().Where(x => x.Status == "Confirmed" && x.Area == s).ToList();

            var deliverman = deliverymanRepo.GetAll().Where(x => x.Area == s).FirstOrDefault();
            string delivermanEmail;
            if (deliverman != null)
            {
                delivermanEmail = userRepo.GetAll().Where(x => x.Id == deliverman.User_Id).FirstOrDefault().Email;
            }
            else
            {
                delivermanEmail = "nai";
            }

            List<Order> orderlist = new List<Order>();
            foreach (var item in list)
            {
                Order x = new Order();
                x.Id = item.Id;
                x.User_Id = item.User_Id;
                x.Name = item.Name;
                x.PhoneNumber = item.PhoneNumber;
                x.Area = item.Area;
                x.Address = item.Address;
                x.Status = item.Status;
                x.ProcessedBy = item.ProcessedBy;
                x.DeliveredBy = delivermanEmail;
                x.AddedDate = item.AddedDate;
                x.ModifiedDate = item.ModifiedDate;
                x.Coupon_Id = item.Coupon_Id;
                //x = item;
                orderlist.Add(x);
            }
            return Json(orderlist, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveOrder(string s, string del)
        {
            int orderId = Convert.ToInt32(s);
            var order = orderRepo.GetById(orderId);
            order.Status = "Assigned";

            orderRepo.Save();

            // save log
            DateTime dateTime = DateTime.Today;
            OrderLog log = new OrderLog();
            log.Order_Id = order.Id;
            log.LogDetails = "Assigned";
            log.AddedDate = dateTime;
            orderLogRepo.Insert(log);
            orderLogRepo.Save();

            //save delivery task
            DeliveryTask task = new DeliveryTask();
            int delivermanUserId = userRepo.GetAll().Where(x => x.Email == del).FirstOrDefault().Id;
            int delID = deliverymanRepo.GetAll().Where(x => x.User_Id == delivermanUserId).FirstOrDefault().Id;
            task.Order_Id = orderId;
            task.DeliveryMan_Id = delID;
            task.AddedDate = dateTime;
            task.Status = "Assigned";
            //int days =  Convert.ToInt32((dateTime - order.AddedDate).TotalDays);
            task.TimeTaken = order.AddedDate;

            taskRepo.Insert(task);
            taskRepo.Save();


            return Json("Task Assigned Successfully", JsonRequestBehavior.AllowGet);
        }




    }
}
