using Project_ATP2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_ATP2.Repositories
{
    public class RakibUserRepository : Repository<User>
    {
        public RakibUserRepository(ProjectDBEntities Context) : base(Context)
        {
        }
        public ProjectDBEntities EContext
        {
            get { return Context as ProjectDBEntities; }
        }

        public User GetUserId(string email)
        {
            var user = EContext.Users.Where(m => m.Email == email).FirstOrDefault();
            return user;
        }

    }
}