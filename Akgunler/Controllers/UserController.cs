using Akgunler.Data;
using Akgunler.Models.Core;
using Akgunler.ViewModels.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Akgunler.Controllers
{
	[Route("api/[controller]/[action]")]
	public class UserController : Controller
    {
        private readonly IRepository<User> mUserRepository;

        public UserController(
			IRepository<User> userRepository
		)
		{
			mUserRepository = userRepository;
		}

		[HttpPost]
		public IActionResult ChangePassword([FromForm] ChangePasswordModel model)
		{
			User currentUser = null;
			List<Role> currentUserRoles = null;

			if (User.Identity == null || string.IsNullOrEmpty(User.Identity.Name))
			{
				return Unauthorized();
			}
			else
			{
				currentUser = mUserRepository.Query()
					.Include(x => x.UserRoles).ThenInclude(x => x.Role)
					.FirstOrDefault(x => x.Username == User.Identity.Name);

				if (currentUser != null && currentUser.UserRoles != null && currentUser.UserRoles.Count > 0)
				{
					currentUserRoles = currentUser.UserRoles.Select(x => x.Role).ToList();
				}
			}

			if (model == null || !(model.UserId > 0) || 
				string.IsNullOrEmpty(model.OldPassword) || 
				string.IsNullOrEmpty(model.NewPassword) || 
				string.IsNullOrEmpty(model.NewPasswordAgain) ||
				model.NewPassword != model.NewPasswordAgain ||
				currentUserRoles == null || currentUserRoles.Count == 0)
			{
				return BadRequest();
			}

			if (!(currentUserRoles.Select(x => x.Name).Contains("admin") || (currentUser.Id == model.UserId)))
			{
				return Unauthorized();
			}

			var user = mUserRepository.Query().FirstOrDefault(x => x.Id == model.UserId);

			if (user == null)
			{
				return NotFound();
			}
			else if(!user.Password.Equals(model.OldPassword))
			{
				return BadRequest();
			}

			user.Password = model.NewPassword;
			mUserRepository.Update(user);

			return Ok();
		}
	}
}
