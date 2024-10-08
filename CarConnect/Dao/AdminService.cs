using CarConnect.Exception;
using CarConnect.Model;
using CarConnect.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Dao
{
    public class AdminService : IAdminService
    {
        public Admin AuthenticateAdmin(string username, string password)
        {
            Admin admin = GetAdminByUsername(username);

            if (admin == null || admin.Password != password)
            {
                throw new AuthenticationException("Invalid username or password.");
            }

            return admin; // Return the authenticated admin object if successful.
        }
        public Admin GetAdminById(int adminId)
        {
            Admin admin = null;

            using (SqlConnection sqlConnection = new SqlConnection(DbConnUtil.GetConnString()))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Admin WHERE AdminID = @AdminID";
                cmd.Connection = sqlConnection;
                cmd.Parameters.AddWithValue("@AdminID", adminId);

                try
                {
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        admin = new Admin
                        {
                            AdminID = (int)reader["AdminID"],
                            FirstName = (string)reader["FirstName"],
                            LastName = (string)reader["LastName"],
                            Email = (string)reader["Email"],
                            PhoneNumber = (string)reader["PhoneNumber"],
                            Username = (string)reader["Username"],
                            Password = (string)reader["Password"],
                            Role = (string)reader["Role"],
                            JoinDate = (DateTime)reader["JoinDate"]
                        };
                    }
                    else
                    {
                        throw new AdminNotFoundException($"Admin with ID {adminId} not found.");
                    }
                }
                catch (SqlException ex)
                {
                    throw new DatabaseConnectionException("Error retrieving admin by ID.", ex);
                }
                finally
                {
                    //sqlConnection.Close();
                }
            }

            return admin;
        }

        public Admin GetAdminByUsername(string username)
        {
            Admin admin = null;

            using (SqlConnection sqlConnection = new SqlConnection(DbConnUtil.GetConnString()))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Admin WHERE Username = @Username";
                cmd.Connection = sqlConnection;
                cmd.Parameters.AddWithValue("@Username", username);

                try
                {
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        admin = new Admin
                        {
                            AdminID = (int)reader["AdminID"],
                            FirstName = (string)reader["FirstName"],
                            LastName = (string)reader["LastName"],
                            Email = (string)reader["Email"],
                            PhoneNumber = (string)reader["PhoneNumber"],
                            Username = (string)reader["Username"],
                            Password = (string)reader["Password"],
                            Role = (string)reader["Role"],
                            JoinDate = (DateTime)reader["JoinDate"]
                        };
                    }
                    else
                    {
                        throw new AdminNotFoundException($"Admin with username '{username}' not found.");
                    }
                }
                catch (SqlException ex)
                {
                    throw new DatabaseConnectionException("Error retrieving admin by username.", ex);
                }
                finally
                {
                    //sqlConnection.Close();
                }
            }

            return admin;
        }

        public void RegisterAdmin(Admin adminData)
        {
            ValidateAdminData(adminData);
            using (SqlConnection sqlConnection = new SqlConnection(DbConnUtil.GetConnString()))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Admin (FirstName, LastName, Email, PhoneNumber, Username, Password, Role, JoinDate)
                            VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @Username, @Password, @Role, @JoinDate)";
                cmd.Connection = sqlConnection;

                cmd.Parameters.AddWithValue("@FirstName", adminData.FirstName);
                cmd.Parameters.AddWithValue("@LastName", adminData.LastName);
                cmd.Parameters.AddWithValue("@Email", adminData.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", adminData.PhoneNumber);
                cmd.Parameters.AddWithValue("@Username", adminData.Username);
                cmd.Parameters.AddWithValue("@Password", adminData.Password);
                cmd.Parameters.AddWithValue("@Role", adminData.Role);
                cmd.Parameters.AddWithValue("@JoinDate", adminData.JoinDate);

                try
                {
                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new DatabaseConnectionException("Error registering admin.", ex);
                }
                finally
                {
                    //sqlConnection.Close();
                }
            }
        }

        public void UpdateAdmin(Admin adminData)
        {
            ValidateAdminData(adminData);
            using (SqlConnection sqlConnection = new SqlConnection(DbConnUtil.GetConnString()))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE Admin SET 
                                FirstName = @FirstName, 
                                LastName = @LastName, 
                                Email = @Email, 
                                PhoneNumber = @PhoneNumber, 
                                Username = @Username, 
                                Password = @Password, 
                                Role = @Role, 
                                JoinDate = @JoinDate 
                            WHERE AdminID = @AdminID";
                cmd.Connection = sqlConnection;

                cmd.Parameters.AddWithValue("@FirstName", adminData.FirstName);
                cmd.Parameters.AddWithValue("@LastName", adminData.LastName);
                cmd.Parameters.AddWithValue("@Email", adminData.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", adminData.PhoneNumber);
                cmd.Parameters.AddWithValue("@Username", adminData.Username);
                cmd.Parameters.AddWithValue("@Password", adminData.Password);
                cmd.Parameters.AddWithValue("@Role", adminData.Role);
                cmd.Parameters.AddWithValue("@JoinDate", adminData.JoinDate);
                cmd.Parameters.AddWithValue("@AdminID", adminData.AdminID);

                try
                {
                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new DatabaseConnectionException("Error updating admin.", ex);
                }
                finally
                {
                    //sqlConnection.Close();
                }
            }
        }
        private void ValidateAdminData(Admin adminData)
        {
            if (string.IsNullOrWhiteSpace(adminData.FirstName) ||
                string.IsNullOrWhiteSpace(adminData.LastName) ||
                string.IsNullOrWhiteSpace(adminData.Username) ||
                string.IsNullOrWhiteSpace(adminData.Password))
            {
                throw new InvalidInputException("All fields are required and cannot be empty.");
            }
        }
            public void DeleteAdmin(int adminId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DbConnUtil.GetConnString()))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DELETE FROM Admin WHERE AdminID = @AdminID";
                cmd.Connection = sqlConnection;
                cmd.Parameters.AddWithValue("@AdminID", adminId);

                try
                {
                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new DatabaseConnectionException("Error deleting admin.", ex);
                }
                finally
                {
                    //sqlConnection.Close();
                }
            }
        }
    }
}
   