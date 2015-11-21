using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceManager.DTOs
{
    public class GameDTO
    {
        public int ID { get; set; }
        public string name { get; set; }
        public int max_players { get; set; }
        public int game_count { get; set; }
        public int max_playing_time { get; set; }

        public GameDTO(int ID, string name, int max_players, int game_count, int max_playing_time)
        {
            this.ID = ID;
            this.name = name;
            this.max_players = max_players;
            this.game_count = game_count;
            this.max_playing_time = max_playing_time;
        }
    }
}