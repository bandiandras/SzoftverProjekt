using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceManager.DTOs
{
    public class LobbyDTO
    {
        public LobbyDTO()
        {

        }

        public LobbyDTO(int GameId, string CreatorName, DateTime StartTime,  int NrOfPlayers)
        {
            this.GameId = GameId;
            this.NrOfPlayers = NrOfPlayers;
            this.StartTime = StartTime;
            this.CreatorName = CreatorName;
        }

        public LobbyDTO(int Id, int GameId, int NrOfPlayers, DateTime StartTime, string CreatorName, int CurrentlyInLobby)
        {
            this.Id = Id;
            this.GameId = GameId;
            this.NrOfPlayers = NrOfPlayers;
            this.StartTime = StartTime;
            this.CreatorName = CreatorName;
            this.CurrentlyInLobby = CurrentlyInLobby;
        }

        public int Id { get; set; }
        public int GameId { get; set; }
        public int NrOfPlayers { get; set; }
        public System.DateTime StartTime { get; set; }
        public string CreatorName { get; set; }
        public int CurrentlyInLobby { get; set; }
    }
}