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
                user usr = db.users.SingleOrDefault(user => user.name == creator_name);
                in_lobby newinlobby = new in_lobby(usr.ID, id, inlobbyid);
                ++usr.created_lobbies;
                db.lobbies.Add(newlobby);
                db.in_lobby.Add(newinlobby);
                db.users.Attach(usr);
                db.Entry(usr).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        //Lobby--JOIN--------------------------------------------------------------------------
        // SZEMAFOR, HOGY NE LEHESSEN EGYSZERRE KETTEN BIRIZGALNI AZ ADATBAZIST
        public static void JoinLobby(string username, int lobbyid)
        {
            using (var db = new project_databaseEntities())
            {
                //csak akkor kell megengedjem, ha a jatekosszam kisebb, mint a games tablaban deinialt max
                lobby gameid = db.lobbies.SingleOrDefault(lobby => lobby.ID == lobbyid);
                game players = db.games.SingleOrDefault(game => game.ID == gameid.game_id);
                user usr = db.users.SingleOrDefault(user => user.name == username);
                if (players.max_players > gameid.currently_in_lobby)
                {
                    int pk_id = db.in_lobby.Max(in_lobby => in_lobby.pk_id) + 1;
                    in_lobby newinlobby = new in_lobby(usr.ID, lobbyid, pk_id);
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

    }
}
