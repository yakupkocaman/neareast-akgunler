using Microsoft.EntityFrameworkCore;
using Akgunler.Models;

namespace Akgunler.Data
{
    public class Repository<T> : RepositoryWithTypedId<T, int>, IRepository<T>
       where T : class, IEntityWithTypedId<int>
    {
        public Repository(AkgunlerContext context) : base(context)
        {

        }

       
    }

}
