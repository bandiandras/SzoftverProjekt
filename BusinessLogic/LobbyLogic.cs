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
                catch (Exception)
                {
                    id = 1;
                }

                lobby newlobby = new lobby(id, games_id, nr_of_players, start_date, creator_name, 1);

                int inlobbyid;
                try
                {
                    inlobbyid = db.in_lobby.Max(in_lobby => in_lobby.pk_id) + 1;
                }
                catch (Exception)
                {
                    inlobbyid = 1;
                }
                user usr = db.users.FirstOrDefault(user => user.name == creator_name);
                in_lobby newinlobby = new in_lobby(inlobbyid, usr.ID, id);
                ++usr.created_lobbies;
                db.lobbies.Add(newlobby);
                db.in_lobby.Add(newinlobby);
                db.users.Attach(usr);
                db.Entry(usr).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        //Lobby--JOIN--------------------------------------------------------------------------

        public static void JoinLobby(string username, int lobbyid)
        {
            using (var db = new project_databaseEntities())
            {
                lobby lobbyToJoin = db.lobbies.SingleOrDefault(lobby => lobby.ID == lobbyid);
                game gameOfLobby = db.games.SingleOrDefault(game => game.ID == lobbyToJoin.game_id);
                user usr = db.users.SingleOrDefault(user => user.name == username);
                if (gameOfLobby.max_players > lobbyToJoin.currently_in_lobby)
                {
                    int pk_id = db.in_lobby.Max(in_lobby => in_lobby.pk_id) + 1;
                    in_lobby newinlobby = new in_lobby(pk_id, usr.ID, lobbyid);
                    db.in_lobby.Add(newinlobby);
                    lobby lob = db.lobbies.SingleOrDefault(lobby => lobby.ID == lobbyid);
                    ++lob.currently_in_lobby;
                    db.lobbies.Attach(lob);
                    db.Entry(lob).State = System.Data.Entity.EntityState.Modified;           
                    ++usr.joined_lobbies;
                    db.users.Attach(usr);
                    db.Entry(usr).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }
        //-------------------------------------------------------------------------------------

        //Lobby--LEAVE--------------------------------------------------------------------------
        public static void LeaveLobby(string username, int lobbyid)
        {
            using (var db = new project_databaseEntities())
            {
                lobby lob = db.lobbies.SingleOrDefault(lobby => lobby.ID == lobbyid);
                user usr = db.users.SingleOrDefault(user => user.name == username);
                in_lobby newinlobby = db.in_lobby.SingleOrDefault(in_lobby => ((in_lobby.lobbyid == lob.ID) && (in_lobby.userid == usr.ID)));
                if (lob.currently_in_lobby < 2)
                {
                    db.lobbies.Remove(lob);
                    foreach (var inlobbyObject in db.in_lobby)
                    {
                        if (inlobbyObject.lobbyid == lob.ID)
                        {
                            db.in_lobby.Remove(inlobbyObject);
                        }
                    }
                    db.SaveChanges();
                }
                else
                {
                    --lob.currently_in_lobby;
                    db.lobbies.Attach(lob);
                    db.Entry(lob).State = System.Data.Entity.EntityState.Modified;
                    db.in_lobby.Remove(newinlobby);
                    db.SaveChanges();
                }
            }
        }
        //-------------------------------------------------------------------------------------
        
        /// <summary>
        /// Checks if a user identified by username param is in lobby idetified by lobbyid
        /// </summary>
        /// <param name="username"></param>
        /// <param name="lobbyid"></param>
        /// <returns></returns>
        public static bool CheckIfInLobby(string username, int lobbyid)
        {
            using (var db = new project_databaseEntities())
            {
                lobby lob = db.lobbies.SingleOrDefault(lobby => lobby.ID == lobbyid);
                user usr = db.users.SingleOrDefault(user => user.name == username);
                in_lobby newinlobby = db.in_lobby.SingleOrDefault(in_lobby => ((in_lobby.lobbyid == lob.ID) && (in_lobby.userid == usr.ID)));
                if(newinlobby != null)
                {
                    return true;
                }
            }
            return false;
        }

        //Lobby--DELETE--------------------------------------------------------------------------
        public static void DeleteLobby(int id)
        {
            using (var db = new project_databaseEntities())
            {
                lobby lob = db.lobbies.SingleOrDefault(lobby => lobby.ID == id);
                db.lobbies.Remove(lob);
                foreach (var inlobbyObject in db.in_lobby)
                {
                    if (inlobbyObject.lobbyid == id)
                    {
                        db.in_lobby.Remove(inlobbyObject);
                    }
                }
                db.SaveChanges();
            }
        }
        //-------------------------------------------------------------------------------------       

        //exceptiont lekezelni - mi tortenik, ha a keresett id nem letezik
        //Lobby--GET----------------------------------------------------------------------------
        public static List<lobby> GetLobbyById(int id)
        {
            List<lobby> Lobbies = new List<lobby>();
            using (var db = new project_databaseEntities())
            {
                try
                {
                    lobby lob = db.lobbies.SingleOrDefault(lobby => lobby.ID == id);
                    Lobbies.Add(new lobby(lob.ID, lob.game_id, lob.nr_of_players, lob.start_date, lob.creator_name, lob.currently_in_lobby));
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }

            }
            return Lobbies;
        }

    }
}
