using Project_ATP2.Models;
using Project_ATP2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Data.Entity;

namespace Project_ATP2.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CartController : Controller
    {
        RakibCartRepository repoRakibCart = new RakibCartRepository(new ProjectDBEntities());
        RakibUserRepository repoRakibUser = new RakibUserRepository(new ProjectDBEntities());
        RakibCouponRepository repoRakibCoupon = new RakibCouponRepository(new ProjectDBEntities());
        RakibOrderRepository repoRakibOrder = new RakibOrderRepository(new ProjectDBEntities());
        RakibOrderDataRepository repoRakibOrderData = new RakibOrderDataRepository(new ProjectDBEntities());
        RakibStockRepository repoRakibStock = new RakibStockRepository(new ProjectDBEntities());
        RakibOrderLogRepository repoRakibOrderLog = new RakibOrderLogRepository(new ProjectDBEntities());

        [Authorize(Roles = "Customer")]
        public ActionResult Index()
        {
            var user = repoRakibUser.GetUserId(User.Identity.Name);
            ViewBag.User_Id = user.Id;
            return View();
        }

        public ActionResult CartGrid()
        {
            var user = repoRakibUser.GetUserId(User.Identity.Name);
            var list = repoRakibCart.GetCartByUserId(user.Id);
            return PartialView("_PartialCartGrid", list);
        }

        public JsonResult GetCart()
        {
            var user = repoRakibUser.GetUserId(User.Identity.Name);
            var list = repoRakibCart.GetCartByUserId(user.Id);
            var obj = list.Select(m => new
            {
                m.Book_Id,
                m.Book.Title,
                m.Book.Author.Name,
                m.QuantityOrdered,
                m.Book.Price
            });
            var json = JsonConvert.SerializeObject(obj);

            //if ()
            //{
            //    return Json("false", JsonRequestBehavior.AllowGet);
            //}
            //var json = JsonConvert.SerializeObject(obj);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SetQuantity(Cart obj)
        {
            var data = repoRakibCart.GetAll().Where(
                        m => m.Book_Id == obj.Book_Id &&
                        m.User_Id == obj.User_Id
            ).FirstOrDefault();

            data.QuantityOrdered = obj.QuantityOrdered;

            repoRakibCart.EContext.SaveChanges();
            return Json("true", JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckCoupon(string couponKey)
        {
            Coupon coupon = repoRakibCoupon.GetPercentage(couponKey);
            Coupon c = new Coupon();
            c.CouponKeyword = coupon.CouponKeyword;
            c.ExpireDate = coupon.ExpireDate;
            c.Id = coupon.Id;
            c.Orders = null;
            c.Percentage = coupon.Percentage;
            

            if (coupon != null)
            {
                DateTime d = DateTime.Now;
                DateTime e = coupon.ExpireDate;

                int result = DateTime.Compare(d, e);

                if (result <= 0)
                {
                    var json = JsonConvert.SerializeObject(c);
                    return Json(json, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("expired", JsonRequestBehavior.AllowGet);
                }
            }

            return Json("false", JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public ActionResult Order(string CouponKey, string CouponPercentage)
        {
            var user = repoRakibUser.GetUserId(User.Identity.Name);
            ViewBag.User_Id = user.Id;
            ViewBag.userName = user.Name;
            ViewBag.couponKey = CouponKey;
            ViewBag.phoneNumber = user.PhoneNumber;
            ViewBag.couponPercentage = CouponPercentage;

            return View();
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public ActionResult OrderConfirm(Order obj,string CouponKey)
        {
            List<Cart> cartList = repoRakibCart.GetCartByUserId(obj.User_Id).ToList();
            if (cartList.Count == 0)
            {
                return RedirectToAction("Index","Home");
            }
            float p = 0;
            if (CouponKey != "")
            {
                Coupon c = repoRakibCoupon.GetPercentage(CouponKey);
                obj.Coupon_Id = c.Id;
                p = (float)c.Percentage;
            }

            repoRakibOrder.Insert(obj);
            repoRakibOrder.EContext.SaveChanges();

            var lastOrder = repoRakibOrder.GetLastEntryOfUser(obj.User_Id);

            //var cartList = repoRakibCart.GetCartByUserId(obj.User_Id);
            foreach (var item in cartList)
            {
                OrderData orderData = new OrderData();
                orderData.Book_Id = item.Book_Id;
                orderData.Order_Id = lastOrder.Id;
                orderData.QuantityOrdered = item.QuantityOrdered;
                orderData.Subtotal = item.QuantityOrdered * item.Book.Price;
                
                if (CouponKey != "")
                {
                    orderData.ActualPrice = Math.Ceiling(orderData.Subtotal - ((orderData.Subtotal * p) / 100));
                }
                else
                {
                    orderData.ActualPrice = Math.Ceiling(orderData.Subtotal);
                }
                repoRakibOrderData.Insert(orderData);
                repoRakibOrderData.EContext.SaveChanges();

                Stock s = repoRakibStock.GetStockByBookId(item.Book_Id);
                s.OrderStock -= item.QuantityOrdered;
                repoRakibStock.EContext.Entry(s).State = EntityState.Modified;
                repoRakibStock.EContext.SaveChanges();
                
            }

            repoRakibCart.RemoveRange(cartList);
            repoRakibCart.EContext.SaveChanges();
            OrderLog o = new OrderLog();
            o.AddedDate = DateTime.Now;
            o.LogDetails = "Pending for comfirmation";
            o.Order_Id = lastOrder.Id;

            repoRakibOrderLog.Insert(o);
            repoRakibOrderLog.EContext.SaveChanges();

            return RedirectToAction("OrderS");
        }

        [Authorize(Roles = "Customer")]
        public ActionResult OrderS()
        {

            return View();
        }

        [Authorize(Roles = "Customer")]
        public ActionResult CartRemove(int? id)
        {
            var user = repoRakibUser.GetUserId(User.Identity.Name);
            var res = repoRakibCart.GetAll().FirstOrDefault(m=>m.Book_Id == id && m.User_Id == user.Id);
            if (res != null)
            {
                repoRakibCart.Delete(res);
                repoRakibCart.EContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }

    }


}
