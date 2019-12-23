using Project_ATP2.Models;
using Project_ATP2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_ATP2.Models.CustomModel;
using Newtonsoft.Json;
using Project_ATP2.Helper;

namespace Project_ATP2.Controllers
{
    public class HomeController : Controller
    {
        RakibBookRepository repoRakibBook = new RakibBookRepository(new ProjectDBEntities());
        RakibAuthorRepository repoRakibAuthor = new RakibAuthorRepository(new ProjectDBEntities());
        RakibCategoryRepository repoRakibCategory = new RakibCategoryRepository(new ProjectDBEntities());
        RakibPublisherRepository repoRakibPublisher = new RakibPublisherRepository(new ProjectDBEntities());
        RakibReportRepository repoRakibReport = new RakibReportRepository(new ProjectDBEntities());
        RakibRakingRepository repoRakibRating = new RakibRakingRepository(new ProjectDBEntities());
        RakibUserRepository repoRakibUser = new RakibUserRepository(new ProjectDBEntities());
        RakibCartRepository repoRakibCart = new RakibCartRepository(new ProjectDBEntities());
        RakibOrderRepository repoRakibOrder = new RakibOrderRepository(new ProjectDBEntities());

        // GET: Home
        public ActionResult Index()
        {
            CustomRakibBook CRB = new CustomRakibBook(repoRakibAuthor.GetAll(), repoRakibPublisher.GetAll(), repoRakibCategory.GetAll(), repoRakibBook.GetAll());
            if (User.Identity.IsAuthenticated)
            {
                var user = repoRakibUser.GetUserId(User.Identity.Name);
                ViewBag.User_Id = user.Id;
            }

            return View(CRB);
        }

        public JsonResult GetBooks(string searchStr, string orderBy, string category, string author, string language, string publisher)
        {

            if (orderBy == "Price: Hight to low")
            {
                var result = repoRakibBook.GetSearchBooksPriceHighToLow(publisher, language, category, author, searchStr);
                var res = result.Select(m => new
                {
                    m.Id,
                    m.Price,
                    m.Title,
                    m.Image,
                    m.Publisher.Name,
                    m.Publisher_Id,
                    m.Category_Id,
                    m.Author_Id,
                    m.Country,
                    m.Language,
                    m.Edition
                });
                var json = JsonConvert.SerializeObject(res);
                return Json(json, JsonRequestBehavior.AllowGet);

            }
            else if (orderBy == "Price: Low to high")
            {
                var result = repoRakibBook.GetSearchBooksPriceLowToHigh(publisher, language, category, author, searchStr);
                var res = result.Select(m => new
                {
                    m.Id,
                    m.Price,
                    m.Title,
                    m.Image,
                    m.Publisher.Name,
                    m.Publisher_Id,
                    m.Category_Id,
                    m.Author_Id,
                    m.Country,
                    m.Language,
                    m.Edition
                });
                var json = JsonConvert.SerializeObject(res);
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = repoRakibBook.GetSearchBooks(publisher, language, category, author, searchStr);
                var res = result.Select(m => new
                {
                    m.Id,
                    m.Price,
                    m.Title,
                    m.Image,
                    m.Publisher.Name,
                    m.Publisher_Id,
                    m.Category_Id,
                    m.Author_Id,
                    m.Country,
                    m.Language,
                    m.Edition
                });
                var json = JsonConvert.SerializeObject(res);
                return Json(json, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Details(int id)
        {
            var book = repoRakibBook.GetById(id);
            //var ratings = 
            //CustomRakibDetail ratings = new CustomRakibDetail();
            if (User.Identity.IsAuthenticated)
            {
                var user = repoRakibUser.GetUserId(User.Identity.Name);
                var rating = repoRakibRating.EContext.Ratings.FirstOrDefault(m => m.User_Id == user.Id && m.Book_Id == book.Id);
                if (rating == null)
                {
                    ViewBag.Star = "";
                    ViewBag.Review = "";
                }
                else if (rating.Stars > 0)
                {
                    ViewBag.Star = rating.Stars;
                    ViewBag.Review = "";
                }
                else if (rating.Review != "")
                {
                    ViewBag.Review = rating.Review;
                    ViewBag.Star = "";
                }
                else
                {
                    ViewBag.Star = "";
                    ViewBag.Review = "";
                }
                ViewBag.User_Id = user.Id;

            }
            return View(book);
        }

        [HttpPost]
        public JsonResult AddReport(Report obj)
        {
            repoRakibReport.AddReport(obj);
            return Json("true", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddReview(Rating obj)
        {
            //repoRakibReport.AddReport(obj);
            repoRakibRating.AddRating(obj);

            return Json("true", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddCart(Cart c)
        {
            //if (User.Identity.IsAuthenticated)
            //{

            //}
            //else
            //{
            //    var shoppingCart = ShoppingCart.Current;
            //}
            if (User.Identity.IsAuthenticated)
            {
                if (c != null)
                {
                    repoRakibCart.AddCart(c);
                }
            }

            return Json("true", JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Customer")]
        public ActionResult MyOrder()
        {

            var user = repoRakibUser.GetUserId(User.Identity.Name);
            var orderList = repoRakibOrder.GetOrderByUserId(user.Id);

            return View(orderList);
        }

        
        [Authorize(Roles = "Customer")]
        public ActionResult MyOrderDelete(int id)
        {

            var orde = repoRakibOrder.GetAll().Where(m => m.Id == id).FirstOrDefault() ;
            orde.Status = "Canceled";            
                repoRakibOrder.EContext.SaveChanges();

            return RedirectToAction("MyOrder");
        }

    }
}
