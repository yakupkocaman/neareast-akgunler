using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Akgunler.Models
{
	public class PageBaseModel : PageModel
    {
		public ISession Session => HttpContext.Session;
		public IConfiguration Configuration => (IConfiguration)HttpContext.RequestServices.GetService(typeof(IConfiguration));

		public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
		{
			var connectionString = Configuration.GetConnectionString("DefaultConnection");
			EntityBase.InitConnection(connectionString);
		}

		public void SetFlashMessage(string message, string type = "error")
        {
			TempData["FlashMessage.Text"] = message;
			TempData["FlashMessage.Type"] = type;
		}

		public void SetFlashMessage(ModelStateDictionary modelStateDictionary)
		{
			var errors = ModelState.Values.SelectMany(m => m.Errors)
								 .Select(e => e.ErrorMessage)
								 .ToList();
			SetFlashMessage(string.Join(", ", errors));
		}
	}
}
