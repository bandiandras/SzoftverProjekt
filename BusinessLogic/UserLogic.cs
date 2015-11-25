using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class UserLogic
    {
        public static void AddUser(string username)
        {
            using (var db = new project_databaseEntities())
            {
                try
                {
                    int userid = db.users.Max(user => user.ID) + 1;
                    user felhasznalo = new user(userid, username, DateTime.Now, 0, 0, 0);
                    db.users.Add(felhasznalo);
                    db.SaveChanges();
                }
                catch (System.InvalidOperationException)
                {
                    user felhasznalo = new user(1, username, DateTime.Now, 0, 0, 0);
                    db.users.Add(felhasznalo);
                    db.SaveChanges();
                }

            }
        }
    }
}
