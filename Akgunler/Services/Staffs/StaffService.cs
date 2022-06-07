using Akgunler.Data;
using Akgunler.Models.Staffs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Akgunler.Services.Staffs
{
    public class StaffService : IStaffService
    {
        private readonly IRepository<Staff> mStaffRepository;

        public StaffService(IRepository<Staff> staffRepository)
        {
            mStaffRepository = staffRepository;
        }
        
        public void Insert(Staff staff) 
        {
            mStaffRepository.Add(staff);
            mStaffRepository.SaveChange();
        }

        public void Update(Staff staff) 
        {
            mStaffRepository.Update(staff);
        }

        public Staff GetFull(int staffId)
        {
            return mStaffRepository.Query()
                .Include(x => x.Department)
                .Include(x => x.DriversLicenseType)
                .Include(x => x.Nationality)
                .Include(x => x.UserRole).ThenInclude(x => x.Role)
                .Where(x => x.Id == staffId && x.DeletedOn == null)
                .AsNoTracking()
                .FirstOrDefault();
        }

        public List<Staff> GetAllFull()
        {
            return mStaffRepository.Query()
                   .Include(x => x.Department)
                   .Include(x => x.DriversLicenseType)
                   .Include(x => x.Nationality)
                   .Include(x => x.UserRole).ThenInclude(x => x.Role)
                   .Where(x => x.DeletedOn == null)
                   .AsNoTracking()
                   .ToList();
        }
    }

    public interface IStaffService
    {
        void Insert(Staff staff);
        void Update(Staff staff);
        Staff GetFull(int staffId);
        List<Staff> GetAllFull();
    }
}
