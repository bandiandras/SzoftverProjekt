using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceManager.Models
{
    public class UserDTO
    {
        string userName;
        string passwordHash;

        public UserDTO()
        {

        }

        public UserDTO(string userName, string passwordHash)
        {
            this.userName = userName;
            this.passwordHash = passwordHash;
        }
    }
}