using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Model
{
    public class Reservation
    {
        private object vehicleID;

        public int ReservationID { get; set; }
        public int CustomerId { get; set; }

        private int vehicleID1;

        public int GetVehicleID()
        {
            return vehicleID1;
        }

        public void SetVehicleID(int value)
        {
            vehicleID1 = value;
        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TotalCost { get; set; }
        public string Status { get; set; }
        public object VehicleID { get => vehicleID; set => vehicleID = value; }


        public Reservation() { }


        public Reservation(int reservationId, int customerId, int vehicleId, DateTime startDate,
                           DateTime endDate, double totalCost, string status)
        {
            ReservationID = reservationId;
            CustomerId = customerId;
            VehicleID=vehicleId;
            StartDate = startDate;
            EndDate = endDate;
            TotalCost = totalCost;
            Status = status;
        }

        public double CalculateTotalCost(double dailyRate)
        {
            double TotalCost = (EndDate - StartDate).Days * dailyRate;
            return TotalCost;
        }
        public void UpdateDetails(int customerId, int vehicleId, DateTime startDate, DateTime endDate, double totalCost, string status)
        {
            CustomerId = customerId;
            VehicleID=vehicleId;
            StartDate = startDate;
            EndDate = endDate;
            TotalCost = totalCost;
            Status = status;
        }
    }
}