using Project_ATP2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Project_ATP2.Helper
{
    public class ShoppingCart
    {
        //public List<CartData> CartList { set; get; }

        public static ShoppingCart Current
        {
            get
            {
                //HttpCookie cookie = RequestNotification.;
                //var cart = new HttpContext.Current
                var c = HttpContext.Current.Request.Cookies.Get("Cart").Value;
                var cart = JsonConvert.DeserializeObject(c);
                //r cart = 
                

                if (cart == null)
                {
                    cart = new ShoppingCart();

                    //Adding cart to cookies
                    HttpCookie cookie = new HttpCookie("Cart");
                    cookie.Value = JsonConvert.SerializeObject(cart);
                }
                return cart as ShoppingCart;
            }
        }
        
    }
}