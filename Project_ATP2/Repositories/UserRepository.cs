using Project_ATP2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_ATP2.Repositories
{
    public class UserRepository:Repository<User>
    {
        public UserRepository(ProjectDBEntities context):base(context)
        {
                
        }
    }
}