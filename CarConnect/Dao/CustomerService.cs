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
    public class CustomerService : ICustomerService
    {
        public bool Authenticate(string username, string password)
        {
            // Default to false, assuming user is not authenticated
            bool isAuthenticated = false;

            // Establish a database connection
            using (SqlConnection sqlConnection = new SqlConnection(DbConnUtil.GetConnString()))
            {
                // Prepare SQL command to check for existing user with given credentials
                SqlCommand cmd = new SqlCommand
                {
                    CommandText = "SELECT COUNT(1) FROM Customer WHERE Username = @Username AND Password = @Password",
                    Connection = sqlConnection
                };
                // Parameterize the query to prevent SQL injection
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                // Open the connection and execute the command
                sqlConnection.Open();
                int userExists = (int)cmd.ExecuteScalar();
                //sqlConnection.Close();

                // If userExists is greater than zero, the user is authenticated
                isAuthenticated = userExists > 0;
            }

            // Return the authentication status
            return isAuthenticated;
        }

        public Customer GetCustomerById(int customerId)
        {
            Customer customer = null;

            using (SqlConnection sqlConnection = new SqlConnection(DbConnUtil.GetConnString()))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Customer WHERE CustomerID = @CustomerID";
                cmd.Connection = sqlConnection;
                cmd.Parameters.AddWithValue("@CustomerID", customerId);

                try
                {
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        customer = new Customer()
                        {
                            CustomerID = (int)reader["CustomerID"],
                            FirstName = (string)reader["FirstName"],
                            LastName = (string)reader["LastName"],
                            Email = (string)reader["Email"],
                            PhoneNumber = (string)reader["PhoneNumber"],
                            Address = (string)reader["Address"],
                            Username = (string)reader["Username"],
                            Password = (string)reader["Password"],
                            RegistrationDate = Convert.ToDateTime(reader["RegistrationDate"])
                        };
                    }
                }
                catch (SqlException ex)
                {
                    throw new DatabaseConnectionException("Error retrieving customer by ID.", ex);
                }
                finally
                {
                    //sqlConnection.Close();
                }
            }

            return customer;
        }

        public Customer GetCustomerByUsername(string username)
        {
            Customer customer = null;

            using (SqlConnection sqlConnection = new SqlConnection(DbConnUtil.GetConnString()))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Customer WHERE Username = @Username";
                cmd.Connection = sqlConnection;
                cmd.Parameters.AddWithValue("@Username", username);

                try
                {
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        customer = new Customer()
                        {
                            CustomerID = (int)reader["CustomerID"],
                            FirstName = (string)reader["FirstName"],
                            LastName = (string)reader["LastName"],
                            Email = (string)reader["Email"],
                            PhoneNumber = (string)reader["PhoneNumber"],
                            Address = (string)reader["Address"],
                            Username = (string)reader["Username"],
                            Password = (string)reader["Password"],
                            RegistrationDate = Convert.ToDateTime(reader["RegistrationDate"])
                        };
                    }
                }
                catch (SqlException ex)
                {
                    throw new DatabaseConnectionException("Error retrieving customer by username.", ex);
                }
                finally
                {
                    //sqlConnection.Close();
                }
            }

            return customer;
        }

        public void RegisterCustomer(Customer customerData)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DbConnUtil.GetConnString()))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Customer (FirstName, LastName, Email, PhoneNumber, Address, Username, Password, RegistrationDate) 
                            VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @Address, @Username, @Password, @RegistrationDate)";
                cmd.Connection = sqlConnection;

                cmd.Parameters.AddWithValue("@FirstName", customerData.FirstName);
                cmd.Parameters.AddWithValue("@LastName", customerData.LastName);
                cmd.Parameters.AddWithValue("@Email", customerData.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", customerData.PhoneNumber);
                cmd.Parameters.AddWithValue("@Address", customerData.Address);
                cmd.Parameters.AddWithValue("@Username", customerData.Username);
                cmd.Parameters.AddWithValue("@Password", customerData.Password);
                cmd.Parameters.AddWithValue("@RegistrationDate", customerData.RegistrationDate);

                try
                {
                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (IOException ex)
                {
                    throw new DatabaseConnectionException("Error registering customer.", ex);
                }
                finally
                {
                    //sqlConnection.Close();
                }
            }
        }

        public void UpdateCustomer(Customer customerData)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DbConnUtil.GetConnString()))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE Customer SET 
                                FirstName = @FirstName, 
                                LastName = @LastName, 
                                Email = @Email, 
                                PhoneNumber = @PhoneNumber, 
                                Address = @Address, 
                                Username = @Username, 
                                Password = @Password 
                            WHERE CustomerID = @CustomerID";
                cmd.Connection = sqlConnection;

                cmd.Parameters.AddWithValue("@FirstName", customerData.FirstName);
                cmd.Parameters.AddWithValue("@LastName", customerData.LastName);
                cmd.Parameters.AddWithValue("@Email", customerData.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", customerData.PhoneNumber);
                cmd.Parameters.AddWithValue("@Address", customerData.Address);
                cmd.Parameters.AddWithValue("@Username", customerData.Username);
                cmd.Parameters.AddWithValue("@Password", customerData.Password);
                cmd.Parameters.AddWithValue("@CustomerID", customerData.CustomerID);

                try
                {
                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new DatabaseConnectionException("Error updating customer.", ex);
                }
                finally
                {
                    //sqlConnection.Close();
                }
            }
        }

        public void DeleteCustomer(int customerId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DbConnUtil.GetConnString()))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DELETE FROM Customer WHERE CustomerID = @CustomerID";
                cmd.Connection = sqlConnection;
                cmd.Parameters.AddWithValue("@CustomerID", customerId);

                try
                {
                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new DatabaseConnectionException("Error deleting customer.", ex);
                }
                finally
                {
                    //sqlConnection.Close();
                }
            }
        }
    }
}