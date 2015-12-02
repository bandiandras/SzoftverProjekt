using BusinessLogic;
using ResourceManager.DTOs;
using ResourceManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ResourceManager.Models;

namespace ResourceManager.Controllers
{
    public class UserController : ApiController
    {
        [Route("api/User/AddUser/")]
        [HttpGet]
        public void AddUser([FromUri] string name)
        {
            UserLogic.AddUser(name);
            //HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            //return response;
        }

        [Route("api/User/CheckUser/")]
        [HttpGet]
        public string CheckUser([FromUri] List<string> userData)
        {
            using (var db = new project_databaseEntities())
            {
                if (userData.Capacity > 0)
                {
                    String[] userDatas = userData[0].Split(',');
                    string userName = userDatas[0];
                    CustomPassword pwdmanage = new CustomPassword();
                    string hashedPassword = pwdmanage.HashPassword(userDatas[1]);
                    
                    AspNetUser myUser = db.AspNetUsers.SingleOrDefault(user => user.UserName == userName);

                    if (myUser.UserName == userName && myUser.PasswordHash == hashedPassword)
                    {
                        return "1";
                    }
                }
                return "0";
            }
        }

	}
}