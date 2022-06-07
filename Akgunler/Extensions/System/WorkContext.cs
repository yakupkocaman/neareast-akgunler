using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Akgunler.Data;
using Akgunler.Models.Core;

namespace Akgunler.Extensions
{
    public class WorkContext : IWorkContext
    {
        private const string UserGuidCookiesName = "AkgunlerUserGuid";
        private const long GuestRoleId = 3;

        private User mCurrentUser;
        private readonly IConfiguration mConfiguration;
        private UserManager<User> mUserManager;
        private HttpContext mHttpContext;
        private readonly IHttpClientFactory mClientFactory;

        public WorkContext(
            UserManager<User> userManager,
            IConfiguration configuration,
            IHttpContextAccessor contextAccessor,
            IHttpClientFactory clientFactory)
        {
            mConfiguration = configuration;
            mUserManager = userManager;
            mHttpContext = contextAccessor.HttpContext;
            mClientFactory = clientFactory;
        }

        public string GetBaseUrl()
        {
            return mHttpContext.Request.Scheme + "://" + mHttpContext.Request.Host.Host + ":" + mHttpContext.Request.Host.Port.ToString() + "/";
        }

        public async Task<User> GetCurrentUser()
        {
            if (mCurrentUser != null)
            {
                return mCurrentUser;
            }

            // On external login callback Identity.IsAuthenticated = true. But it's an external claim principal
            // Login by google, get _userManager.GetUserAsync from ClaimsPrincipal throw exception becasue the UserIdClaimType has value but too big.
            if (mHttpContext.User.Identity.AuthenticationType == "Identity.Application" || mHttpContext.User.Identity.AuthenticationType == "Cookies")
            {
                var contextUser = mHttpContext.User;
                mCurrentUser = await mUserManager.GetUserAsync(contextUser);
            }
            else if(mHttpContext.User.Identity.AuthenticationType == "AuthenticationTypes.Federation")
            {
				var contextUser = mHttpContext.User;
                if(contextUser != null && contextUser.Claims != null && contextUser.FindFirst(ClaimTypes.Name) != null)
                mCurrentUser = await mUserManager.FindByNameAsync(contextUser.FindFirst(ClaimTypes.Name).Value);
            }

            if (mCurrentUser != null)
            {
                return mCurrentUser;
            }
            else
            {
                return null;
            }
        }

        private Guid? GetUserGuidFromCookies()
        {
            if (mHttpContext.Request.Cookies.ContainsKey(UserGuidCookiesName))
            {
                return Guid.Parse(mHttpContext.Request.Cookies[UserGuidCookiesName]);
            }

            return null;
        }

        private void SetUserGuidCookies(Guid userGuid)
        {
            mHttpContext.Response.Cookies.Append(UserGuidCookiesName, mCurrentUser.Id.ToString(), new CookieOptions
            {
                Expires = DateTime.UtcNow.AddYears(5),
                HttpOnly = true
            });
        }
    }
}
