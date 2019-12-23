using Project_ATP2.Interfaces;
using Project_ATP2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_ATP2.Repositories
{
    public class LoginRepository : Repository<Login>
    {
        public LoginRepository(ProjectDBEntities Context) : base(Context)
        {
        }

        //Guys please copy paste the EContext property in all your created repositories
        public ProjectDBEntities EContext
        {
            get { return Context as ProjectDBEntities; }
        }

        public bool VarifyLogin(string email, string pass)
        {
            bool isValid = EContext.Logins.Any(m=>m.Email == email && m.Pass == pass);
            return isValid;
        }
        public string GetUserId(string email)
        {
            string id = EContext.Users.Where(m => m.Email == email).Select(m => m.Id).FirstOrDefault().ToString();
            return id;
        }
        public string[] GetUserRole(string username)
        {
            var result = EContext.Users.Where(m => m.Email == username).Select(m => m.Role.Name).ToArray();
            return result;
        }

    }
}