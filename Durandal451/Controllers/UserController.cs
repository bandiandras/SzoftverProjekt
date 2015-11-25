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

	}
}