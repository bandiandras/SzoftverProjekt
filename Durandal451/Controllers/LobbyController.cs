using BusinessLogic;
using gManagerNew.Hubs;
using ResourceManager.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ResourceManager.Controllers
{
    public class LobbyController : ApiController
    {
        /// <summary>
        /// Gets data from url, then makes a new lobby using the data
        /// api/Lobby/NewLobby/paramAray=id,creatorName,starttime,nrofplayers
        /// </summary>
        /// <param name="paramArray"></param>
        /// <returns></returns>
        [Route("api/Lobby/NewLobby")]
        [HttpGet]
        public string NewLobby([FromUri] List<string> paramArray)
        {
          if (paramArray.Capacity > 0)
            {
                String[] l = paramArray[0].Split(',');
                
                LobbyDTO lobbyobject = new LobbyDTO(Int32.Parse(l[0]), l[1], DateTime.Parse(l[2]), Int32.Parse(l[3]));
                  try
                  {
                      LobbyLogic.AddLobby(lobbyobject.GameId, lobbyobject.NrOfPlayers, lobbyobject.StartTime, lobbyobject.CreatorName);
                  }
                  catch(Exception ex)
                  {
                      return "Az error occured!" + ex;
                  }
                
                return "recieved an object!";
            }

            return "NOTHING RECIEVED...";
        }

        /// <summary>
        /// Deletes a lobby identified by id param
        /// </summary>
        /// <param name="id"></param>
        [Route("api/Lobby/DeleteLobby")]
        [HttpGet]
        public int DeleteLobby([FromUri] int id)
        {
            try
            {
                LobbyLogic.DeleteLobby(id);
                return 1;
            }
            catch(Exception ex)
            {
                return 0;
            }

        }


        /// <summary>
        /// Returns a list of with all lobbies
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<LobbyDTO> GetAllLobbies()
        {
            using (var db = new project_databaseEntities())
            {
                List<lobby> listOfLobbies = db.lobbies.ToList();
                List<LobbyDTO> allLobbies = new List<LobbyDTO>();
                if (listOfLobbies.Capacity > 0)
                {
                    foreach (var dbItem in listOfLobbies)
                    {
                        allLobbies.Add(new LobbyDTO(dbItem.ID, dbItem.game_id, dbItem.nr_of_players, dbItem.start_date, dbItem.creator_name, dbItem.currently_in_lobby));
                    }
                }
                return allLobbies;
            }
        }

        /// <summary>
        /// Join a lobby
        /// paramArray[0] : User Name
        /// paramArray[1] : Lobby ID
        /// </summary>
        /// <param name="paramArray"></param>
        [Route("api/Lobby/Joinlobby")]
        [HttpGet]
        public int JoinLobby([FromUri] List<string> paramArray)
        {
            String[] lResult = paramArray[0].Split(',');
            if (!LobbyLogic.CheckIfInLobby(lResult[0], Int32.Parse(lResult[1])))
            {
                try
                {
                    LobbyLogic.JoinLobby(lResult[0], Int32.Parse(lResult[1]));
                    List<lobby> lob = new List<lobby>();
                    lob = LobbyLogic.GetLobbyById(Int32.Parse(lResult[1]));
                    if (lob[0].currently_in_lobby == lob[0].nr_of_players)
                    {
                        MsgHub.SendMessageToGroup("The lobby you joined is full, please be at the the tables at time!", lob[0].creator_name);
                    }
                    return 1;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
            return 0;
        }

        [Route("api/Lobby/LeaveLobby")]
        [HttpGet]
        public int LeaveLobby([FromUri] List<String> paramArray)
        {
            try
            {
                String[] lResult = paramArray[0].Split(',');
                LobbyLogic.LeaveLobby(lResult[0], Int32.Parse(lResult[1]));
                List<lobby> lob = new List<lobby>();
                lob = LobbyLogic.GetLobbyById(Int32.Parse(lResult[1]));
                MsgHub.SendMessageToGroup(lResult[0] + " left the lobby, please wait for another player!", lob[0].creator_name);
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        [Route("api/Lobby/GetLobbyById")]
        [HttpGet]
        public List<lobby> GetLobbyById([FromUri] int id)
        {
            return LobbyLogic.GetLobbyById(id);
        }

    }
}