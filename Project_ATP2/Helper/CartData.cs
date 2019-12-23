using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_ATP2.Helper
{
    public class CartData
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public int Book_Id { get; set; }
        public int QuantityOrdered { get; set; }
    }
}