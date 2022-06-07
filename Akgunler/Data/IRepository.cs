using Akgunler.Models;

namespace Akgunler.Data
{
    public interface IRepository<T> : IRepositoryWithTypedId<T, int> where T : IEntityWithTypedId<int>
    {
    }
}
