using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using ResourceManager.Models;
using ResourceManager.Providers;
using ResourceManager.Results;
using BusinessLogic;

namespace ResourceManager.Resources
{
    public class MyUserManager : UserManager<IdentityUser>
    {
        public MyUserManager()
            : base(new UserStore<IdentityUser>(new IdentityDbContext()))
        {
            this.PasswordHasher = new CustomPassword();
        }
    }
}