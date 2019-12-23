using Project_ATP2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_ATP2.Repositories
{
    public class RakibCouponRepository : Repository<Coupon>
    {
        public RakibCouponRepository(ProjectDBEntities Context) : base(Context)
        {
        }
        public ProjectDBEntities EContext
        {
            get { return Context as ProjectDBEntities; }
        }

        public Coupon GetPercentage(string couponKey)
        {
            var result = EContext.Coupons.FirstOrDefault(m => m.CouponKeyword == couponKey);
            return result;
        }
    }
}