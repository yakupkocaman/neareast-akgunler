using System;
using System.Linq;
using System.Security.Claims;

namespace Akgunler.Extensions.System
{
    public static class ClaimsPrincipalExtensions
    {
        public static T GetUserId<T>(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            var loggedInUserId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

            if (typeof(T) == typeof(string))
            {
                return (T)Convert.ChangeType(loggedInUserId, typeof(T));
            }
            else if (typeof(T) == typeof(int) || typeof(T) == typeof(long))
            {
                return loggedInUserId != null ? (T)Convert.ChangeType(loggedInUserId, typeof(T)) : (T)Convert.ChangeType(0, typeof(T));
            }
            else
            {
                throw new Exception("Invalid type provided");
            }
        }

        public static string GetUserName(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirstValue(ClaimTypes.Name);
        }

        public static string GetUserFullName(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirstValue(ClaimTypes.GivenName);
        }

        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirstValue(ClaimTypes.Email);
        }

        public static long? GetUserInstitutionId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            long? loggedInUserInstitutionId = null;
            var loggedInUserInstitution = principal.FindFirstValue("UserInstitutionId");

            if(loggedInUserInstitution != null)
            {
                loggedInUserInstitutionId = (long)Convert.ChangeType(loggedInUserInstitution, typeof(long));
            }

            return loggedInUserInstitutionId;
        }

        public static string GetUserInstitutionName(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirstValue("UserInstitutionName");
        }

        public static long? GetUserDepartmentId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            long? loggedInUserDepartmentId = null;
            var loggedInUserDepartment = principal.FindFirstValue("UserDepartmentId");

            if (loggedInUserDepartment != null)
            {
                loggedInUserDepartmentId = (long)Convert.ChangeType(loggedInUserDepartment, typeof(long));
            }

            return loggedInUserDepartmentId;
        }

        public static string GetUserDepartmentName(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirstValue("UserDepartmentName");
        }

        public static string GetUserRoleNames(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            var currentUserRoles = principal.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value);
            return string.Join(", ", currentUserRoles);
        }
        
        public static bool IsUserInRoles(this ClaimsPrincipal principal, string roles)
        {
            if (principal == null || string.IsNullOrEmpty(roles))
                throw new ArgumentNullException(nameof(principal));

            var rolesArr = roles.Split(",");
            var currentUserRoles = principal.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value);
            return currentUserRoles.Any(x => rolesArr.Contains(x.Trim()));
        }
    }
}