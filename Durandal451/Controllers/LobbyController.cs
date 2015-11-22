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
        [HttpPost]
        public void NewLobby([FromUri] LobbyDTO lobbyobject)
        {
            LobbyLogic.AddLobby(lobbyobject.GameId, lobbyobject.NrOfPlayers, lobbyobject.StartTime, lobbyobject.CreatorName);
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