using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceManager.DTOs
{
    public class JoinDTO
    {
        public JoinDTO()
        {

        }

        public JoinDTO(string UserName, int LobbyId)
        {
            this.UserName = UserName;
            this.LobbyId = LobbyId;
        }

        public string UserName;
        public int LobbyId;
    }
}