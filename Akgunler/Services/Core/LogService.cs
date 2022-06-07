using Akgunler.Data;
using Akgunler.Models.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Akgunler.Services.Core
{
    public class LogService : ILogService
    {
        private readonly IRepository<Log> mLogRepository;

        public LogService(IRepository<Log> logRepository)
        {
            mLogRepository = logRepository;
        }

        public List<Log> GetLogs(string username)
        {
            return mLogRepository.Query()
                .Where(x => x.Username == username)
                .OrderByDescending(x => x.CreatedOn)
                .AsNoTracking()
                .ToList();
        }

        public (int, List<Log>) GetPagedLogs(DateTime start, DateTime end, int page, int count)
        {
            start = start.Date;
            end = end.Date.AddDays(1).AddTicks(-1);
            if (page < 1) page = 1;

            var query = mLogRepository
                .Query()
                .Where(x => x.CreatedOn > start)
                .OrderByDescending(x => x.CreatedOn)
                .AsNoTracking();

            var total = query.Count();
            var items = query
                .Skip((page - 1) * count)
                .Take(count)
                .ToList();

            return (total, items);
        }

        public async Task<List<Log>> GetLogsAsync(DateTime start, DateTime end)
        {
            start = start.Date;
            end = end.Date.AddDays(1).AddTicks(-1);

            return await mLogRepository
                .Query()
                .Where(x => x.CreatedOn > start && x.CreatedOn < end)
                .OrderByDescending(x => x.CreatedOn)
                .AsNoTracking()
                .ToListAsync();
        }

        public void Add(Log value)
        {
            mLogRepository.Add(value);
            mLogRepository.SaveChange();
        }
    }

    public interface ILogService
    {
        List<Log> GetLogs(string username);
        (int, List<Log>) GetPagedLogs(DateTime start, DateTime end, int page, int count);
        Task<List<Log>> GetLogsAsync(DateTime start, DateTime end);
        void Add(Log value);
    }
}
