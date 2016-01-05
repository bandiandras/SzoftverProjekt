using BusinessLogic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using ResourceManager.DTOs;
using ResourceManager.Models;
using ResourceManager.Providers;
using ResourceManager.Results;
using ResourceManager.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Security.Cryptography;

namespace ResourceManager.Controllers
{
    public class UserController : ApiController
    {
        public MyUserManager UserManager { get; private set; }
        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }
        private const string DefaultUserRole = "RegisteredUsers";

                public UserController()
            : this(Startup.UserManagerFactory(), Startup.OAuthOptions.AccessTokenFormat)
        {
        }

        public UserController(MyUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            //UserManager.PasswordHasher = new CustomPassword();
            
            //password validator
            AccessTokenFormat = accessTokenFormat;
        }


        [Route("api/User/AddUser/")]
        [HttpGet]
        public void AddUser([FromUri] string name)
        {
            UserLogic.AddUser(name);
        }

        /// <summary>
        /// Register user, using GET request. 
        /// 
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        [Route("api/User/RegisterUser")]
        [HttpGet]
        public async Task<int> RegisterUser([FromUri] List<string> userData)
        {
            if (userData.Capacity > 0)
            {
                String[] userDatas = userData[0].Split(',');
                string userName = userDatas[0];
                string userPass = userDatas[1];

                IdentityUser user = new IdentityUser
                {
                    UserName = userName
                };
                
                IdentityResult result = await UserManager.CreateAsync(user, userPass);

                result = await UserManager.AddToRoleAsync(user.Id, DefaultUserRole);
                UserLogic.AddUser(userName);
                return 1;
            }
            return 0;
        }

        [Route("api/User/ChangePassword")]
        [HttpGet]
        // api/User/ChangePassword/?userData=username,oldpass,newpass
        public async Task<int> ChangePassword([FromUri] List<string> userData)
        {
            if (userData.Capacity > 0)
            {
                String[] userDatas = userData[0].Split(',');
                string userName = userDatas[0];
                string userPass = userDatas[1];
                string newUserPass = userDatas[2];

                var usr = await UserManager.FindByNameAsync(userName);

                IdentityResult result = await UserManager.ChangePasswordAsync(usr.Id, userPass, newUserPass);
                if (result.Succeeded)
                {
                    return 1;
                }
                return 0;
            }
            return 0;
        }


        [Route("api/User/CheckUser/")]
        [HttpGet]
        public int CheckUser([FromUri] List<string> userData)
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
                        return 1;
                    }
                }
                return 0;
            }
        }

	}
}