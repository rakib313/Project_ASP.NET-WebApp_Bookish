using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project_ATP2.Models;
using Project_ATP2.Models.CustomModel;

namespace Project_ATP2.Repositories
{
    public class UserRepositoryJahan: Repository<User>
    {
        public UserRepositoryJahan(ProjectDBEntities Context) : base(Context)
        {
        }

        //Guys please copy paste the EContext property in all your created repositories
        public ProjectDBEntities EContext
        {
            get { return Context as ProjectDBEntities; }
        }


        

        public User GetByEmail(string email)
        {
            IEnumerable<User> uList;
            uList = GetAll();
            foreach (User u in uList)
            {
                if (u.Email == email)
                    return u;
            }
            return null;
        }
    }
}