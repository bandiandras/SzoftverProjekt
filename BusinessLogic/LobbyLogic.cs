using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class LobbyLogic
    {
        public static void AddLobby(int games_id, int nr_of_players, DateTime start_date, string creator_name)
        {
            using (var db = new project_databaseEntities())
            {
                int id;
                try
                {
                    id = db.lobbies.Max(lobby => lobby.ID) + 1;
                }
                catch (System.InvalidOperationException)
                {
                    id = 1;
                }

                lobby newlobby = new lobby(id, games_id, nr_of_players, start_date, creator_name, 1);

                int inlobbyid;
                try
                {
                    inlobbyid = db.in_lobby.Max(in_lobby => in_lobby.pk_id) + 1;
                }
                catch (System.InvalidOperationException)
                {
                    inlobbyid = 1;
                }
                //user usr = db.users.SingleOrDefault(user => user.name == creator_name);
                //in_lobby newinlobby = new in_lobby(usr.id, id, inlobbyid);
                //++usr.created_lobbies;
                db.lobbies.Add(newlobby);
                //db.in_lobby.Add(newinlobby);
                //db.users.Attach(usr);
                //db.Entry(usr).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        //-------------------------------------------------------------------------------------
    }
}
