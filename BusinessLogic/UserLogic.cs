using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class UserLogic
    {
        //user--GET-----------------------------------------------------------------------
        public static List<user> GetUserByName(string name)
        {
            using (var db = new project_databaseEntities())
            {
                List<user> usrl = new List<user>();
                user usr = db.users.SingleOrDefault(user => user.name == name);
                usrl.Add(new user(usr.ID, usr.name, usr.registration_date, usr.created_lobbies, usr.joined_lobbies, usr.canceled_lobbies));
                return usrl;
            }
        }
        //--------------------------------------------------------------------------------

        //user--DELETE--------------------------------------------------------------------
        public static void DeleteUser(int id)
        {
            using (var db = new project_databaseEntities())
            {
                user usr = db.users.SingleOrDefault(user => user.ID == id);
                db.users.Remove(usr);
                db.SaveChanges();
            }
        }
        //--------------------------------------------------------------------------------

        //user--ADD-----------------------------------------------------------------------
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
        //--------------------------------------------------------------------------------
    }
}
