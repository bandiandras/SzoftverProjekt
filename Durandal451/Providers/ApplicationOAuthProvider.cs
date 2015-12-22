using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using System.Web;
using BusinessLogic;
using ResourceManager.Models;
using ResourceManager.Resources;

namespace ResourceManager.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;
        //private readonly Func<UserManager<IdentityUser>> _userManagerFactory;
        private readonly Func<MyUserManager> _userManagerFactory;

        //public ApplicationOAuthProvider(string publicClientId, Func<UserManager<IdentityUser>> userManagerFactory)
        public ApplicationOAuthProvider(string publicClientId, Func<MyUserManager> userManagerFactory)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            if (userManagerFactory == null)
            {
                throw new ArgumentNullException("userManagerFactory");
            }

            _publicClientId = publicClientId;
            _userManagerFactory = userManagerFactory;
        }

        public IdentityUser FindUser(string UserName, string UserPassword)
        {
            using (var db = new project_databaseEntities())
            {
                CustomPassword pwd = new CustomPassword();
                string pwdHash = pwd.HashPassword(UserPassword);
                AspNetUser checkLoginData = db.AspNetUsers.SingleOrDefault(user => user.UserName == UserName && user.PasswordHash == pwdHash);
                if (checkLoginData != null)
                {
                    IdentityUser usr = new IdentityUser(UserName);
                    usr.PasswordHash = pwdHash;
                    usr.SecurityStamp = checkLoginData.SecurityStamp;                   
                    return usr;
                }
                return null;
            }
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            using (MyUserManager userManager = new MyUserManager())
            {
                IdentityUser user = new IdentityUser();
                if (FindUser(context.UserName, context.Password) != null)
                {
                    user = userManager.FindByName(context.UserName);
                }
                //IdentityUser user2 = await userManager.FindAsync(context.UserName, context.Password);
               
                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }

                ClaimsIdentity oAuthIdentity = await userManager.CreateIdentityAsync(user,
                    context.Options.AuthenticationType);
                ClaimsIdentity cookiesIdentity = await userManager.CreateIdentityAsync(user,
                    CookieAuthenticationDefaults.AuthenticationType);
                AuthenticationProperties properties = CreateProperties(user);
                AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
                context.Validated(ticket);
                context.Request.Context.Authentication.SignIn(cookiesIdentity);
            }
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }


        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = FullRootUri(HttpContext.Current);

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        private Uri FullRootUri(HttpContext context)
        {
            var appPath = string.Empty;

            if (context != null)
            {
                appPath = string.Format("{0}://{1}{2}{3}",
                                   context.Request.Url.Scheme,
                                   context.Request.Url.Host,
                                   context.Request.Url.Port == 80
                                       ? string.Empty
                                       : ":" + context.Request.Url.Port,
                                   context.Request.ApplicationPath);
            }

            if (!appPath.EndsWith("/"))
            {
                appPath += "/";
            }

            return new Uri(appPath);
        }

        public static AuthenticationProperties CreateProperties(IdentityUser user)
        {
            var roles = string.Join(",",user.Roles.Select(iur => iur.Role.Name));        

            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", user.UserName },
                { "userRoles", roles }
            };
            return new AuthenticationProperties(data);
        }
    }
}