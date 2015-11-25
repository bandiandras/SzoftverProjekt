using BusinessLogic;
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
        [Route("api/Lobby/NewLobby")]
        [HttpGet]
        public string NewLobby([FromUri] List<string> paramArray)
        {
          if (paramArray.Capacity > 0)
            {
                String[] l = paramArray[0].Split(',');
                LobbyDTO lobbyobject = new LobbyDTO(Int32.Parse(l[0]), l[1], DateTime.Parse(l[2]), Int32.Parse(l[3]));
                LobbyLogic.AddLobby(lobbyobject.GameId, lobbyobject.NrOfPlayers, lobbyobject.StartTime, lobbyobject.CreatorName);
                return "recieved an object!";
            }

            return "NOTHING RECIEVED...";
        }

        [Route("api/Lobby/DeleteLobby")]
        [HttpGet]
        public void DeleteLobby([FromUri] int id)
        {
            LobbyLogic.DeleteLobby(id);
        }


        // GET api/<controller>
        [HttpGet]
        public List<LobbyDTO> GetAllLobbies()
        {
            using (var db = new project_databaseEntities())
            {
                List<LobbyDTO> allLobbies = new List<LobbyDTO>();
                foreach (var dbItem in db.lobbies.ToList())
                {
                    allLobbies.Add(new LobbyDTO(dbItem.ID, dbItem.game_id, dbItem.nr_of_players, dbItem.start_date, dbItem.creator_name, dbItem.currently_in_lobby));
                }
                return allLobbies;
            }
        }

    }
}