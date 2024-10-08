using CarConnect.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Dao
{
    public interface IVehicleService
    {
        Vehicle GetVehicleById(int vehicleId);
        List<Vehicle> GetAvailableVehicles();
        void AddVehicle(Vehicle vehicle);
        void UpdateVehicle(Vehicle vehicle);
        void RemoveVehicle(int vehicleId);
        List<Vehicle> GetAllVehicles();
        void DisplayAvailableVehicles();
        void DisplayAllVehicles();
    }
}