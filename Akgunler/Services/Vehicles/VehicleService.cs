using Akgunler.Data;
using Akgunler.Models.Vehicles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Akgunler.Services.Vehicles
{
    public class VehicleService : IVehicleService
    {
        private readonly IRepository<Vehicle> mVehicleRepository;

        public VehicleService(IRepository<Vehicle> vehicleRepository)
        {
            mVehicleRepository = vehicleRepository;
        }

        public void Insert(Vehicle vehicle) 
        {
            mVehicleRepository.Add(vehicle);
            mVehicleRepository.SaveChange();
        }

        public void Update(Vehicle vehicle) 
        {
            mVehicleRepository.Update(vehicle);
        }

        public Vehicle GetFull(int vehicleId)
        {
            return mVehicleRepository.Query()
                .Include(x => x.VehicleType)
                .Include(x => x.VehicleGroup)
                .Include(x => x.Color)
                .Include(x => x.Make)
                .Include(x => x.Transmission)
                .Include(x => x.Fuel)
                .Where(x => x.Id == vehicleId && x.DeletedOn == null)
                .AsNoTracking()
                .FirstOrDefault();
        }

        public List<Vehicle> GetAllFull()
        {
            return mVehicleRepository.Query()
                .Include(x => x.VehicleType)
                .Include(x => x.VehicleGroup)
                .Include(x => x.Color)
                .Include(x => x.Make)
                .Include(x => x.Transmission)
                .Include(x => x.Fuel)
                .Where(x => x.DeletedOn == null)
                .AsNoTracking()
                .ToList();
        }

        public List<Vehicle> GetVehiclesByRegistrationNo(string registrationNo)
        {
            if (!string.IsNullOrWhiteSpace(registrationNo))
            {
                registrationNo = Regex.Replace(registrationNo, @"\s+", String.Empty);

                var sql = @"SELECT * 
                    FROM Vehicle.Vehicle 
                    WHERE REPLACE(REPLACE(RegistrationNo,' ', ''), char(9), '') LIKE '%"+ registrationNo + "%'";

                return Vehicle.All<Vehicle>(sql, new { });
            }

            return new List<Vehicle>();
        }
    }

    public interface IVehicleService
    {
        void Insert(Vehicle vehicle);
        void Update(Vehicle vehicle);
        Vehicle GetFull(int vehicleId);
        List<Vehicle> GetAllFull();
        List<Vehicle> GetVehiclesByRegistrationNo(string registrationNo);
    }
}
