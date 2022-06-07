using Akgunler.Data;
using Akgunler.Models.Jobs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Akgunler.Services.Jobs
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<Account> mAccountRepository;

        public AccountService(IRepository<Account> accountRepository)
        {
            mAccountRepository = accountRepository;
        }

        public List<Account> GetByJobId(int jobId)
        {
            return mAccountRepository.Query()
                .Include(x => x.Currency)
                .Include(x => x.AccountType)
                .Where(x => x.JobId == jobId)
                .ToList();
        }
    }

    public interface IAccountService
    {
        List<Account> GetByJobId(int jobId);
    }
}
