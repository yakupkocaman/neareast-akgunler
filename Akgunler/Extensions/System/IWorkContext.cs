using Akgunler.Models.Core;
using System.Threading.Tasks;

namespace Akgunler.Extensions
{
    public interface IWorkContext
    {
        Task<User> GetCurrentUser();

        string GetBaseUrl();
    }
}
