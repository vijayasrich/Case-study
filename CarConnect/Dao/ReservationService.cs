using CarConnect.Exception;
using CarConnect.Model;
using CarConnect.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Dao
{
    public class ReservationService : IReservationService
    {
        public Reservation GetReservationById(int reservationId)
        {
            Reservation reservation = null;

            using (SqlConnection sqlConnection = new SqlConnection(DbConnUtil.GetConnString()))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Reservation WHERE ReservationID = @ReservationID";
                cmd.Connection = sqlConnection;
                cmd.Parameters.AddWithValue("@ReservationID", reservationId);

                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    reservation = new Reservation
                    {
                        ReservationID = (int)reader["ReservationID"],
                        CustomerId = (int)reader["CustomerID"],
                        VehicleID = (int)reader["VehicleID"],
                        StartDate = (DateTime)reader["StartDate"],
                        EndDate = (DateTime)reader["EndDate"],
                        TotalCost = (double)(decimal)reader["TotalCost"],
                        Status = (string)reader["Status"]
                    };
                }
                else
                {
                    throw new ReservationException("Reservation not found.");
                }

               // sqlConnection.Close();
            }

            return reservation;
        }

        public List<Reservation> GetReservationsByCustomerId(int customerId)
        {
            List<Reservation> reservations = new List<Reservation>();

            using (SqlConnection sqlConnection = new SqlConnection(DbConnUtil.GetConnString()))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Reservation WHERE CustomerID = @CustomerID";
                cmd.Connection = sqlConnection;
                cmd.Parameters.AddWithValue("@CustomerID", customerId);

                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Reservation reservation = new Reservation
                    {
                        ReservationID = (int)reader["ReservationID"],
                        CustomerId = (int)reader["CustomerID"],
                        VehicleID = (int)reader["VehicleID"],
                        StartDate = (DateTime)reader["StartDate"],
                        EndDate = (DateTime)reader["EndDate"],
                        TotalCost = (double)(decimal)reader["TotalCost"],
                        Status = (string)reader["Status"]
                    };

                    reservations.Add(reservation);
                }

                //sqlConnection.Close();
            }

            return reservations;
        }
        public void DisplayReservations(List<Reservation> reservations)
        {
            if (reservations.Count > 0)
            {
                const int idWidth = 15;
                const int customerIdWidth = 10;
                const int vehicleIdWidth = 10;
                const int startDateWidth = 13;
                const int endDateWidth = 13;
                const int totalCostWidth = 10;
                const int statusWidth = 10;

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"| {"Reservation ID",idWidth} | {"Customer ID",customerIdWidth} | {"Vehicle ID",vehicleIdWidth} | {"Start Date",startDateWidth} | {"End Date",endDateWidth} | {"Total Cost",totalCostWidth} | {"Status",statusWidth} |");
                Console.WriteLine(new string('-', idWidth + customerIdWidth + vehicleIdWidth + startDateWidth + endDateWidth + totalCostWidth + statusWidth + 16));
                Console.ResetColor();

                foreach (var reservation in reservations)
                {
                    Console.WriteLine($"| {reservation.ReservationID.ToString().PadRight(idWidth)} | {reservation.CustomerId.ToString().PadRight(customerIdWidth)} | {reservation.VehicleID.ToString().PadRight(vehicleIdWidth)} | {reservation.StartDate.ToShortDateString().PadRight(startDateWidth)} | {reservation.EndDate.ToShortDateString().PadRight(endDateWidth)} | {reservation.TotalCost.ToString("N2").PadRight(totalCostWidth)} | {reservation.Status.PadRight(statusWidth)} |");
                }

                Console.WriteLine(new string('-', idWidth + customerIdWidth + vehicleIdWidth + startDateWidth + endDateWidth + totalCostWidth + statusWidth + 16));
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No reservations found for this customer.");
            }

            Console.ResetColor();
        }

        public void CreateReservation(Reservation reservationData)
        {
            if (!DoesVehicleExist((int)reservationData.VehicleID))
            {
                throw new VehicleNotFoundException("The specified VehicleID does not exist.");
            }

            using (SqlConnection sqlConnection = new SqlConnection(DbConnUtil.GetConnString()))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Reservation (CustomerID, VehicleID, StartDate, EndDate, TotalCost, Status)
                        VALUES (@CustomerID, @VehicleID, @StartDate, @EndDate, @TotalCost, @Status)";
                cmd.Connection = sqlConnection;

                cmd.Parameters.AddWithValue("@CustomerID", reservationData.CustomerId);
                cmd.Parameters.AddWithValue("@VehicleID", reservationData.VehicleID);
                cmd.Parameters.AddWithValue("@StartDate", reservationData.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", reservationData.EndDate);
                cmd.Parameters.AddWithValue("@TotalCost", reservationData.TotalCost);
                cmd.Parameters.AddWithValue("@Status", reservationData.Status);

                sqlConnection.Open();
                cmd.ExecuteNonQuery();
                //sqlConnection.Close();
            }
        }

        private bool DoesVehicleExist(int vehicleId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DbConnUtil.GetConnString()))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Vehicle WHERE VehicleID = @VehicleID", sqlConnection);
                cmd.Parameters.AddWithValue("@VehicleID", vehicleId);

                sqlConnection.Open();
                int count = (int)cmd.ExecuteScalar();
                //sqlConnection.Close();

                return count > 0; // Returns true if vehicle exists
            }
        }

        public void UpdateReservation(Reservation reservationData)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DbConnUtil.GetConnString()))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE Reservation SET 
                            CustomerID = @CustomerID, 
                            VehicleID = @VehicleID, 
                            StartDate = @StartDate, 
                            EndDate = @EndDate, 
                            TotalCost = @TotalCost, 
                            Status = @Status 
                        WHERE ReservationID = @ReservationID";
                cmd.Connection = sqlConnection;

                cmd.Parameters.AddWithValue("@CustomerID", reservationData.CustomerId);
                cmd.Parameters.AddWithValue("@VehicleID", reservationData.VehicleID);
                cmd.Parameters.AddWithValue("@StartDate", reservationData.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", reservationData.EndDate);
                cmd.Parameters.AddWithValue("@TotalCost", reservationData.TotalCost);
                cmd.Parameters.AddWithValue("@Status", reservationData.Status);
                cmd.Parameters.AddWithValue("@ReservationID", reservationData.ReservationID);

                sqlConnection.Open();
                cmd.ExecuteNonQuery();
                //sqlConnection.Close();
            }
        }

        public void CancelReservation(int reservationId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DbConnUtil.GetConnString()))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DELETE FROM Reservation WHERE ReservationID = @ReservationID";
                cmd.Connection = sqlConnection;
                cmd.Parameters.AddWithValue("@ReservationID", reservationId);

                sqlConnection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                //sqlConnection.Close();

                if (rowsAffected == 0)
                {
                    throw new ReservationException("No reservation found with the specified ID to cancel.");
                }
            }
        }
    }
}
