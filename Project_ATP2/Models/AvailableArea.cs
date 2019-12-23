using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_ATP2.Models
{
    public class AvailableArea
    {

        static public List<string> AllArea()
        {
            List<string> AreaList = new List<string>();
            AreaList.Add("gulshan");
            AreaList.Add("banani");
            AreaList.Add("dhanmondi");
            AreaList.Add("kuril");
            AreaList.Add("mirpur");
            AreaList.Add("uttara");
            AreaList.Add("mohammadpur");

            return AreaList;
        }
        
    }
}

