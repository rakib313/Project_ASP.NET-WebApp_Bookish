using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_ATP2.Models.CustomModel
{
    public class RAdminDashboard
    {
        public int TotalCustomer { get; set; }
        public int TotalOrderComplete { get; set; }
        public int TotalMoneyearned { get; set; }
        public int TodayOrder { get; set; }
        public int TodayMoney { get; set; }
        public int TodayOrderPlaced { get; set; }
        public List<Book> TopBooks { get; set; }
    }
}