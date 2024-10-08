using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnect.Model;
using CarConnect.Utility;
using System.Data.SqlClient;
using CarConnect.Exception;

namespace CarConnect.Dao
{
    public class VehicleService : IVehicleService
    {

        public Vehicle GetVehicleById(int vehicleId)
        {
            if (vehicleId <= 0)
            {
                throw new InvalidInputException("Vehicle ID must be greater than zero.");
            }

            Vehicle vehicle = null;

            using (SqlConnection sqlConnection = new SqlConnection(DbConnUtil.GetConnString()))
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandText = "SELECT * FROM Vehicle WHERE VehicleID = @VehicleID",
                    Connection = sqlConnection
                };
                cmd.Parameters.AddWithValue("@VehicleID", vehicleId);

                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    vehicle = new Vehicle()
                    {
                        VehicleID = (int)reader["VehicleID"],
                        Model = (string)reader["Model"],
                        Make = (string)reader["Make"],
                        Year = (int)reader["Year"],
                        Color = (string)reader["Color"],
                        RegistrationNumber = (string)reader["RegistrationNumber"],
                        Availability = (bool)reader["Availability"],
                        DailyRate = (double)(decimal)reader["DailyRate"]
                    };
                }
                else
                {
                    throw new VehicleNotFoundException($"Vehicle with ID {vehicleId} not found.");
                }

                //sqlConnection.Close();
            }

            return vehicle;
        }
        public List<Vehicle> GetAllVehicles()
        {
            var vehicles = new List<Vehicle>();

            using (SqlConnection sqlConnection = new SqlConnection(DbConnUtil.GetConnString()))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Vehicle", sqlConnection);

                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var vehicle = new Vehicle
                    {
                        VehicleID = (int)reader["VehicleID"],
                        Model = (string)reader["Model"],
                        Make = (string)reader["Make"],
                        DailyRate = (double)(decimal)reader["DailyRate"],
                        // Add other properties as needed
                    };
                    vehicles.Add(vehicle);
                }
            }

            return vehicles;
        }
        public void DisplayAllVehicles()
        {
            var vehicles = GetAllVehicles(); // Call the GetAllVehicles method

            if (vehicles.Count > 0)
            {
                const int idWidth = 5;
                const int modelWidth = 20;
                const int makeWidth = 20;
                const int dailyRateWidth = 12;

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"| {"ID",idWidth} | {"Model",modelWidth} | {"Make",makeWidth} | {"Daily Rate",dailyRateWidth} |");
                Console.WriteLine(new string('-', idWidth + modelWidth + makeWidth + dailyRateWidth + 14));
                Console.ResetColor();

                foreach (var vehicle in vehicles)
                {
                    Console.WriteLine($"| {vehicle.VehicleID.ToString().PadRight(idWidth)} | {vehicle.Model.PadRight(modelWidth)} | {vehicle.Make.PadRight(makeWidth)} | {vehicle.DailyRate.ToString("N2").PadRight(dailyRateWidth)} |");
                }

                Console.WriteLine(new string('-', idWidth + modelWidth + makeWidth + dailyRateWidth + 14));
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No vehicles found.");
            }

            Console.ResetColor();
        }
        public void DisplayAvailableVehicles()
        {
            List<Vehicle> availableVehicles = GetAvailableVehicles(); // Fetch available vehicles

            if (availableVehicles.Count > 0)
            {
                const int idWidth = 5;
                const int modelWidth = 15;
                const int makeWidth = 10;
                const int yearWidth = 5;
                const int colorWidth = 10;
                const int regNumWidth = 10;
                const int dailyRateWidth = 10;

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"| {"ID",idWidth} | {"Model",modelWidth} | {"Make",makeWidth} | {"Year",yearWidth} | {"Color",colorWidth} | {"Reg Num",regNumWidth} | {"Daily Rate",dailyRateWidth} |");
                Console.WriteLine(new string('-', idWidth + modelWidth + makeWidth + yearWidth + colorWidth + regNumWidth + dailyRateWidth + 14));
                Console.ResetColor();

                foreach (var v in availableVehicles)
                {
                    Console.WriteLine($"| {v.VehicleID.ToString().PadRight(idWidth)} | {v.Model.PadRight(modelWidth)} | {v.Make.PadRight(makeWidth)} | {v.Year.ToString().PadRight(yearWidth)} | {v.Color.PadRight(colorWidth)} | {v.RegistrationNumber.PadRight(regNumWidth)} | {v.DailyRate.ToString("N").PadRight(dailyRateWidth)} |");
                }

                Console.WriteLine(new string('-', idWidth + modelWidth + makeWidth + yearWidth + colorWidth + regNumWidth + dailyRateWidth + 14)); // Final separator
            }
            else
            {
                Console.WriteLine("No available vehicles found.");
            }
        }

        public List<Vehicle> GetAvailableVehicles()
        {
            List<Vehicle> availableVehicles = new List<Vehicle>();

            using (SqlConnection sqlConnection = new SqlConnection(DbConnUtil.GetConnString()))
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandText = "SELECT * FROM Vehicle WHERE Availability = 1",
                    Connection = sqlConnection
                };

                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Vehicle vehicle = new Vehicle()
                    {
                        VehicleID = (int)reader["VehicleID"],
                        Model = (string)reader["Model"],
                        Make = (string)reader["Make"],
                        Year = (int)reader["Year"],
                        Color = (string)reader["Color"],
                        RegistrationNumber = (string)reader["RegistrationNumber"],
                        Availability = reader["Availability"].ToString().ToLower() == "yes",
                        DailyRate = (double)(decimal)reader["DailyRate"]
                    };

                    availableVehicles.Add(vehicle);
                }
            }

            return availableVehicles;
        }

        public void AddVehicle(Vehicle vehicleData)
        {
            if (vehicleData == null)
            {
                throw new InvalidInputException("Vehicle data cannot be null.");
            }

            // Additional validation can be added here

            using (SqlConnection sqlConnection = new SqlConnection(DbConnUtil.GetConnString()))
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandText = @"INSERT INTO Vehicle (Model, Make, Year, Color, RegistrationNumber, Availability, DailyRate) 
                                VALUES (@Model, @Make, @Year, @Color, @RegistrationNumber, @Availability, @DailyRate)",
                    Connection = sqlConnection
                };

                cmd.Parameters.AddWithValue("@Model", vehicleData.Model);
                cmd.Parameters.AddWithValue("@Make", vehicleData.Make);
                cmd.Parameters.AddWithValue("@Year", vehicleData.Year);
                cmd.Parameters.AddWithValue("@Color", vehicleData.Color);
                cmd.Parameters.AddWithValue("@RegistrationNumber", vehicleData.RegistrationNumber);
                cmd.Parameters.AddWithValue("@Availability", vehicleData.Availability);
                cmd.Parameters.AddWithValue("@DailyRate", vehicleData.DailyRate);

                sqlConnection.Open();
                cmd.ExecuteNonQuery();
                //sqlConnection.Close();
            }
        }
        

    


    public void UpdateVehicle(Vehicle vehicleData)
        {
            if (vehicleData == null)
            {
                throw new InvalidInputException("Vehicle data cannot be null.");
            }

            if (vehicleData.VehicleID <= 0)
            {
                throw new InvalidInputException("Vehicle ID must be greater than zero.");
            }

            using (SqlConnection sqlConnection = new SqlConnection(DbConnUtil.GetConnString()))
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandText = @"UPDATE Vehicle SET 
                                Model = @Model, 
                                Make = @Make, 
                                Year = @Year, 
                                Color = @Color, 
                                RegistrationNumber = @RegistrationNumber, 
                                Availability = @Availability, 
                                DailyRate = @DailyRate 
                                WHERE VehicleID = @VehicleID",
                    Connection = sqlConnection
                };

                cmd.Parameters.AddWithValue("@Model", vehicleData.Model);
                cmd.Parameters.AddWithValue("@Make", vehicleData.Make);
                cmd.Parameters.AddWithValue("@Year", vehicleData.Year);
                cmd.Parameters.AddWithValue("@Color", vehicleData.Color);
                cmd.Parameters.AddWithValue("@RegistrationNumber", vehicleData.RegistrationNumber);
                cmd.Parameters.AddWithValue("@Availability", vehicleData.Availability);
                cmd.Parameters.AddWithValue("@DailyRate", vehicleData.DailyRate);
                cmd.Parameters.AddWithValue("@VehicleID", vehicleData.VehicleID);

                sqlConnection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new VehicleNotFoundException($"Vehicle with ID {vehicleData.VehicleID} not found.");
                }

                //sqlConnection.Close();
            }
        }

        public void RemoveVehicle(int vehicleId)
        {
            if (vehicleId <= 0)
            {
                throw new InvalidInputException("Vehicle ID must be greater than zero.");
            }

            using (SqlConnection sqlConnection = new SqlConnection(DbConnUtil.GetConnString()))
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandText = "DELETE FROM Vehicle WHERE VehicleID = @VehicleID",
                    Connection = sqlConnection
                };
                cmd.Parameters.AddWithValue("@VehicleID", vehicleId);

                sqlConnection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new VehicleNotFoundException($"Vehicle with ID {vehicleId} not found.");
                }

               // sqlConnection.Close();
            }
        }
    }
}
