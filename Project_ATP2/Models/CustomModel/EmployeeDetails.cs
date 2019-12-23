using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_ATP2.Models.CustomModel
{
    public class EmployeeDetails
    {
        public User Employee { get; set; }
        public int Last7 { get; set; }
        public int Today { get; set; }
        public int Total { get; set; }
    }
}