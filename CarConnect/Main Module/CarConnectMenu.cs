using CarConnect.Dao;
using CarConnect.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.MainModule
{
    internal class CarConnectMenu
    {
        readonly ICustomerService _customerService;
        readonly IVehicleService _vehicleService;
        readonly IReservationService _reservationService;
        readonly IAdminService _adminService;
        private int choice;

        public CarConnectMenu()
        {
            _customerService = new CustomerService();
            _vehicleService = new VehicleService();
            _reservationService = new ReservationService();
            _adminService = new AdminService();
        }

        public void Run()
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Car Rental Management System");
                Console.WriteLine("1. Customer Management");
                Console.WriteLine("2. Vehicle Management");
                Console.WriteLine("3. Reservation Management");
                Console.WriteLine("4. Admin Management");
                Console.WriteLine("5. Exit");
                Console.Write("Select an option: ");

                if (int.TryParse(Console.ReadLine(), out int menu))
                {
                    switch (menu)
                    {
                        case 1:
                            ManageCustomers();
                            break;
                        case 2:
                            ManageVehicles();
                            break;
                        case 3:
                            ManageReservations();
                            break;
                        case 4:
                            ManageAdmins();
                            break;
                        case 5:
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
       

        private void ManageCustomers()
        {
            bool keepManaging = true;
            while (keepManaging)
            {
                Console.Clear();
                Console.WriteLine("Customer Management");
                Console.WriteLine("1. Get Customer by ID");
                Console.WriteLine("2. Get Customer by Username");
                Console.WriteLine("3. Register Customer");
                Console.WriteLine("4. Update Customer");
                Console.WriteLine("5. Delete Customer");
                Console.WriteLine("6. Back to Main Menu");
                Console.Write("Select an option: ");

                try
                {
                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            // Get Customer by ID
                            Console.Write("Enter Customer ID: ");
                            if (!int.TryParse(Console.ReadLine(), out int customerId)) // Validate input
                            {
                                Console.WriteLine("Invalid input. Please enter a valid numeric ID.");
                                break; // Exit the case if the input is invalid
                            }
                            var customer = _customerService.GetCustomerById(customerId);
                            Console.WriteLine(customer != null ? $"{customer.FirstName} {customer.LastName}" : "Customer not found.");
                            break;

                        case 2:
                            // Get Customer by Username
                            Console.Write("Enter Username: ");
                            string username = Console.ReadLine();
                            var customerByUsername = _customerService.GetCustomerByUsername(username);
                            Console.WriteLine(customerByUsername != null ? $"{customerByUsername.FirstName} {customerByUsername.LastName}" : "Customer not found.");
                            break;

                        case 3:
                            Console.Write("Enter First Name: ");
                            string firstName = Console.ReadLine();
                            Console.Write("Enter Last Name: ");
                            string lastName = Console.ReadLine();
                            Console.Write("Enter Email: ");
                            string email = Console.ReadLine();
                            Console.Write("Enter Phone Number: ");
                            string phoneNumber = Console.ReadLine();
                            Console.Write("Enter Address: ");
                            string address = Console.ReadLine();
                            Console.Write("Enter Username: ");
                            string newUsername = Console.ReadLine();
                            Console.Write("Enter Password: ");
                            string password = Console.ReadLine();

                            var newCustomer = new Customer
                            {
                                FirstName = firstName,
                                LastName = lastName,
                                Email = email,
                                PhoneNumber = phoneNumber,
                                Address = address,
                                Username = newUsername,
                                Password = password,
                                RegistrationDate = DateTime.Now
                            };

                            _customerService.RegisterCustomer(newCustomer);
                            Console.WriteLine("Customer registered successfully.");
                            break;

                        case 4:
                            
                            Console.Write("Enter Customer ID to update: ");
                            int updateCustomerId = int.Parse(Console.ReadLine());
                            var customerToUpdate = _customerService.GetCustomerById(updateCustomerId);

                            if (customerToUpdate != null)
                            {
                                Console.Write("Enter new First Name (leave empty to keep unchanged): ");
                                string newFirstName = Console.ReadLine();
                                Console.Write("Enter new Last Name (leave empty to keep unchanged): ");
                                string newLastName = Console.ReadLine();
                                Console.Write("Enter new Email (leave empty to keep unchanged): ");
                                string newEmail = Console.ReadLine();
                                Console.Write("Enter new Phone Number (leave empty to keep unchanged): ");
                                string newPhoneNumber = Console.ReadLine();
                                Console.Write("Enter new Address (leave empty to keep unchanged): ");
                                string newAddress = Console.ReadLine();
                                Console.Write("Enter new Password (leave empty to keep unchanged): ");
                                string newPassword = Console.ReadLine();

                                customerToUpdate.FirstName = string.IsNullOrWhiteSpace(newFirstName) ? customerToUpdate.FirstName : newFirstName;
                                customerToUpdate.LastName = string.IsNullOrWhiteSpace(newLastName) ? customerToUpdate.LastName : newLastName;
                                customerToUpdate.Email = string.IsNullOrWhiteSpace(newEmail) ? customerToUpdate.Email : newEmail;
                                customerToUpdate.PhoneNumber = string.IsNullOrWhiteSpace(newPhoneNumber) ? customerToUpdate.PhoneNumber : newPhoneNumber;
                                customerToUpdate.Address = string.IsNullOrWhiteSpace(newAddress) ? customerToUpdate.Address : newAddress;
                                customerToUpdate.Password = string.IsNullOrWhiteSpace(newPassword) ? customerToUpdate.Password : newPassword;

                                _customerService.UpdateCustomer(customerToUpdate);
                                Console.WriteLine("Customer updated successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Customer not found.");
                            }
                            break;

                        case 5:
                            Console.Write("Enter Customer ID to delete: ");
                            int deleteId = int.Parse(Console.ReadLine());
                            _customerService.DeleteCustomer(deleteId);
                            Console.WriteLine("Customer deleted successfully.");
                            break;

                        case 6:
                            keepManaging = false; // Exit the loop and return to the main menu
                            break;

                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Invalid input: {ex.Message}. Please enter the correct format.");
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }

                // Pause before clearing the screen and showing the menu again
                Console.WriteLine("\nPress any key to return to the menu...");
                Console.ReadKey(); // Pause until the user presses any key
            }
        }
        private void ManageVehicles()
        {
            bool keepManaging = true;
            while (keepManaging)
            {
                Console.Clear();
                Console.WriteLine("Vehicle Management");
                Console.WriteLine("1. Get Vehicle by ID");
                Console.WriteLine("2. Get Available Vehicles");
                Console.WriteLine("3. Add Vehicle");
                Console.WriteLine("4. Update Vehicle");
                Console.WriteLine("5. Remove Vehicle");
                Console.WriteLine("6. Back to Main Menu");
                Console.Write("Select an option: ");

                try
                {
                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {

                        case 1:
                            
                            Console.Write("Enter Vehicle ID: ");
                            if (int.TryParse(Console.ReadLine(), out int vehicleId))
                            {
                                var vehicle = _vehicleService.GetVehicleById(vehicleId);
                                Console.WriteLine(vehicle != null ? $"{vehicle.Model} {vehicle.Make}" : "Vehicle not found.");
                            }
                            else
                            {
                                Console.WriteLine("Invalid Vehicle ID format.");
                            }
                            break;

                        case 2:

                            Console.WriteLine("Available Vehicles:");
                            _vehicleService.DisplayAvailableVehicles(); 
                            break;

                        case 3:
                            Console.Write("Enter Model: ");
                            string model = Console.ReadLine();
                            Console.Write("Enter Make: ");
                            string make = Console.ReadLine();
                            Console.Write("Enter Year: ");
                            int year = int.Parse(Console.ReadLine());
                            Console.Write("Enter Color: ");
                            string color = Console.ReadLine();
                            Console.Write("Enter Registration Number: ");
                            string registrationNumber = Console.ReadLine();
                            Console.Write("Is Vehicle Available? (true/false): ");
                            bool availability = bool.Parse(Console.ReadLine());
                            Console.Write("Enter Daily Rate: ");
                            decimal dailyRate = decimal.Parse(Console.ReadLine());

                            var newVehicle = new Vehicle
                            {
                                Model = model,
                                Make = make,
                                Year = year,
                                Color = color,
                                RegistrationNumber = registrationNumber,
                                Availability = availability,
                                DailyRate = (double)dailyRate
                            };

                            _vehicleService.AddVehicle(newVehicle);
                            Console.WriteLine("Vehicle added successfully.");
                            break;

                        
                        case 4:
                            _vehicleService.DisplayAvailableVehicles();



                            Console.Write("Enter Vehicle ID to update: ");
                            if (int.TryParse(Console.ReadLine(), out int updateVehicleId))
                            {
                                var vehicleToUpdate = _vehicleService.GetVehicleById(updateVehicleId);

                                if (vehicleToUpdate != null)
                                {
                                    bool isUpdated = false;

                                    Console.Write("Enter new Model (leave empty to keep unchanged): ");
                                    string newModel = Console.ReadLine();
                                    if (!string.IsNullOrWhiteSpace(newModel))
                                    {
                                        vehicleToUpdate.Model = newModel;
                                        isUpdated = true;
                                    }

                                    Console.Write("Enter new Make (leave empty to keep unchanged): ");
                                    string newMake = Console.ReadLine();
                                    if (!string.IsNullOrWhiteSpace(newMake))
                                    {
                                        vehicleToUpdate.Make = newMake;
                                        isUpdated = true;
                                    }

                                    Console.Write("Enter new Year (leave empty to keep unchanged): ");
                                    string newYearInput = Console.ReadLine();
                                    if (!string.IsNullOrWhiteSpace(newYearInput))
                                    {
                                        vehicleToUpdate.Year = int.Parse(newYearInput);
                                        isUpdated = true;
                                    }

                                    Console.Write("Enter new Color (leave empty to keep unchanged): ");
                                    string newColor = Console.ReadLine();
                                    if (!string.IsNullOrWhiteSpace(newColor))
                                    {
                                        vehicleToUpdate.Color = newColor;
                                        isUpdated = true;
                                    }

                                    Console.Write("Enter new Registration Number (leave empty to keep unchanged): ");
                                    string newRegistrationNumber = Console.ReadLine();
                                    if (!string.IsNullOrWhiteSpace(newRegistrationNumber))
                                    {
                                        vehicleToUpdate.RegistrationNumber = newRegistrationNumber;
                                        isUpdated = true;
                                    }

                                    Console.Write("Is Vehicle Available? (true/false) (leave empty to keep unchanged): ");
                                    string newAvailabilityInput = Console.ReadLine();
                                    if (!string.IsNullOrWhiteSpace(newAvailabilityInput))
                                    {
                                        vehicleToUpdate.Availability = bool.Parse(newAvailabilityInput);
                                        isUpdated = true;
                                    }

                                    Console.Write("Enter new Daily Rate (leave empty to keep unchanged): ");
                                    string newDailyRateInput = Console.ReadLine();
                                    if (!string.IsNullOrWhiteSpace(newDailyRateInput))
                                    {
                                        vehicleToUpdate.DailyRate = double.Parse(newDailyRateInput);
                                        isUpdated = true;
                                    }

                                    // Only update and show a message if there was an actual change
                                    if (isUpdated)
                                    {
                                        _vehicleService.UpdateVehicle(vehicleToUpdate);
                                        Console.WriteLine("Vehicle updated successfully.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("No changes made to the vehicle.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Vehicle not found.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid Vehicle ID format.");
                            }
                            break;


                        case 5:
                            _vehicleService.DisplayAvailableVehicles();


                            Console.Write("Enter Vehicle ID to remove: ");
                            int removeId = int.Parse(Console.ReadLine());
                            _vehicleService.RemoveVehicle(removeId);
                            Console.WriteLine("Vehicle removed successfully.");
                            break;

                        case 6:
                            keepManaging = false; // Exit the loop and return to the main menu
                            break;

                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Invalid input: {ex.Message}. Please enter the correct format.");
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }

                // Pause before clearing the screen and showing the menu again
                Console.WriteLine("\nPress any key to return to the menu...");
                Console.ReadKey(); // Pause until the user presses any key
            }
        }

       
        private void ManageReservations()
        {
            bool keepManaging = true;
            while (keepManaging)
            {
                Console.Clear();
                Console.WriteLine("Reservation Management");
                Console.WriteLine("1. Get Reservation by ID");
                Console.WriteLine("2. Get Reservations by Customer ID");
                Console.WriteLine("3. Create Reservation");
                Console.WriteLine("4. Update Reservation");
                Console.WriteLine("5. Cancel Reservation");
                Console.WriteLine("6. Back to Main Menu");
                Console.Write("Select an option: ");

                try
                {
                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            Console.Write("Enter Reservation ID: ");
                            int reservationId = int.Parse(Console.ReadLine());
                            var reservation = _reservationService.GetReservationById(reservationId);
                            if (reservation != null)
                            {
                                Console.WriteLine("Reservation Details:");
                                Console.WriteLine($"Reservation ID: {reservation.ReservationID}");
                                Console.WriteLine($"Customer ID: {reservation.CustomerId}");
                                Console.WriteLine($"Vehicle ID: {reservation.VehicleID}");
                                Console.WriteLine($"Start Date: {reservation.StartDate:yyyy-MM-dd}");
                                Console.WriteLine($"End Date: {reservation.EndDate:yyyy-MM-dd}");
                                Console.WriteLine($"Total Cost: {reservation.TotalCost:N}");
                                Console.WriteLine($"Status: {reservation.Status}");
                            }
                            else
                            {
                                Console.WriteLine("Reservation not found.");
                            }
                            break;

                        case 2:
                            Console.Write("Enter Customer ID: ");
                            if (int.TryParse(Console.ReadLine(), out int customerId))
                            {
                                var reservations = _reservationService.GetReservationsByCustomerId(customerId);
                                _reservationService.DisplayReservations(reservations); // Call the display method
                            }
                            else
                            {
                                Console.WriteLine("Invalid Customer ID format.");
                            }
                            break;

                        case 3:
                            Console.Write("Enter Customer ID: ");
                            int newCustomerId = int.Parse(Console.ReadLine());

                            _vehicleService.DisplayAvailableVehicles();

                            Console.Write("Enter Vehicle ID: ");
                            int newVehicleId = int.Parse(Console.ReadLine());

                            Console.Write("Enter Start Date (yyyy-MM-dd): ");
                            DateTime startDate = DateTime.Parse(Console.ReadLine());

                            Console.Write("Enter End Date (yyyy-MM-dd): ");
                            DateTime endDate = DateTime.Parse(Console.ReadLine());

                            if (endDate <= startDate)
                            {
                                Console.WriteLine("End Date must be after Start Date.");
                                break;
                            }

                            var newReservation = new Reservation
                            {
                                CustomerId = newCustomerId,
                                VehicleID = newVehicleId,
                                StartDate = startDate,
                                EndDate = endDate,
                                Status = "Active"
                            };

                            _reservationService.CreateReservation(newReservation);
                            Console.WriteLine("Reservation created successfully.");
                            break;

                        case 4:
                            Console.Write("Enter Reservation ID to update: ");
                            int updateReservationId = int.Parse(Console.ReadLine());
                            var reservationToUpdate = _reservationService.GetReservationById(updateReservationId);

                            if (reservationToUpdate != null)
                            {
                                _vehicleService.DisplayAvailableVehicles();

                                Console.Write("Enter new Vehicle ID (leave empty to keep unchanged): ");
                                string newVehicleIdInput = Console.ReadLine();
                                Console.Write("Enter new Start Date (leave empty to keep unchanged): ");
                                string newStartDateInput = Console.ReadLine();
                                Console.Write("Enter new End Date (leave empty to keep unchanged): ");
                                string newEndDateInput = Console.ReadLine();

                                reservationToUpdate.SetVehicleID(string.IsNullOrWhiteSpace(newVehicleIdInput) ? reservationToUpdate.GetVehicleID() : int.Parse(newVehicleIdInput));
                                reservationToUpdate.StartDate = string.IsNullOrWhiteSpace(newStartDateInput) ? reservationToUpdate.StartDate : DateTime.Parse(newStartDateInput);
                                reservationToUpdate.EndDate = string.IsNullOrWhiteSpace(newEndDateInput) ? reservationToUpdate.EndDate : DateTime.Parse(newEndDateInput);

                                _reservationService.UpdateReservation(reservationToUpdate);
                                Console.WriteLine("Reservation updated successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Reservation not found.");
                            }
                            break;

                        case 5:
                            Console.Write("Enter Reservation ID to cancel: ");
                            int cancelId = int.Parse(Console.ReadLine());
                            _reservationService.CancelReservation(cancelId);
                            Console.WriteLine("Reservation canceled successfully.");
                            break;

                        case 6:
                            keepManaging = false; // Exit the loop and return to the main menu
                            break;

                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Invalid input: {ex.Message}. Please enter the correct format.");
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }

                // Pause before clearing the screen and showing the menu again
                Console.WriteLine("\nPress any key to return to the menu...");
                Console.ReadKey(); // Pause until the user presses any key
            }
        }

        
        private void ManageAdmins()
        {
            bool keepManaging = true;
            while (keepManaging)
            {
                Console.Clear();
                Console.WriteLine("Admin Management");
                Console.WriteLine("1. Get Admin by ID");
                Console.WriteLine("2. Get Admin by Username");
                Console.WriteLine("3. Register Admin");
                Console.WriteLine("4. Update Admin");
                Console.WriteLine("5. Delete Admin");
                Console.WriteLine("6. Back to Main Menu");
                Console.Write("Select an option: ");

                try
                {
                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            Console.Write("Enter Admin ID: ");
                            int adminId = int.Parse(Console.ReadLine());
                            var admin = _adminService.GetAdminById(adminId);
                            Console.WriteLine(admin != null ? $"{admin.FirstName} {admin.LastName}" : "Admin not found.");
                            break;

                        case 2:
                            Console.Write("Enter Username: ");
                            string username = Console.ReadLine();
                            var adminByUsername = _adminService.GetAdminByUsername(username);
                            Console.WriteLine(adminByUsername != null ? $"{adminByUsername.FirstName} {adminByUsername.LastName}" : "Admin not found.");
                            break;

                        case 3:
                            Console.Write("Enter First Name: ");
                            string newAdminFirstName = Console.ReadLine();

                            Console.Write("Enter Last Name: ");
                            string newAdminLastName = Console.ReadLine();

                            Console.Write("Enter Email: ");
                            string newAdminEmail = Console.ReadLine();

                            Console.Write("Enter Phone Number: ");
                            string newAdminPhoneNumber = Console.ReadLine();

                            Console.Write("Enter Username: ");
                            string newAdminUsername = Console.ReadLine();

                            Console.Write("Enter Password: ");
                            string newAdminPassword = Console.ReadLine();

                            Console.Write("Enter Role: ");
                            string newAdminRole = Console.ReadLine();

                            Console.Write("Enter Join Date (yyyy-mm-dd): ");
                            DateTime newAdminJoinDate;
                            while (!DateTime.TryParse(Console.ReadLine(), out newAdminJoinDate))
                            {
                                Console.Write("Invalid date format. Please enter Join Date (yyyy-mm-dd): ");
                            }


                            var newAdmin = new Admin
                            {
                                FirstName = newAdminFirstName,
                                LastName = newAdminLastName,
                                Email = newAdminEmail,
                                PhoneNumber = newAdminPhoneNumber,
                                Username = newAdminUsername,
                                Password = newAdminPassword,
                                Role = newAdminRole,
                                JoinDate = newAdminJoinDate
                            };

                            _adminService.RegisterAdmin(newAdmin);
                            Console.WriteLine("Admin registered successfully.");
                            break;

                        case 4:
                            Console.Write("Enter Admin ID to update: ");
                            int updateAdminId = int.Parse(Console.ReadLine());
                            var adminToUpdate = _adminService.GetAdminById(updateAdminId);

                            if (adminToUpdate != null)
                            {
                                Console.Write("Enter new Username (leave empty to keep unchanged): ");
                                string newAdminUsernameInput = Console.ReadLine();
                                Console.Write("Enter new Password (leave empty to keep unchanged): ");
                                string newAdminPasswordInput = Console.ReadLine();

                                adminToUpdate.Username = string.IsNullOrWhiteSpace(newAdminUsernameInput) ? adminToUpdate.Username : newAdminUsernameInput;
                                adminToUpdate.Password = string.IsNullOrWhiteSpace(newAdminPasswordInput) ? adminToUpdate.Password : newAdminPasswordInput;

                                _adminService.UpdateAdmin(adminToUpdate);
                                Console.WriteLine("Admin updated successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Admin not found.");
                            }
                            break;

                        case 5:
                            Console.Write("Enter Admin ID to delete: ");
                            int deleteId = int.Parse(Console.ReadLine());
                            _adminService.DeleteAdmin(deleteId);
                            Console.WriteLine("Admin deleted successfully.");
                            break;

                        case 6:
                            keepManaging = false; // Exit the loop and return to the main menu
                            break;

                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Invalid input: {ex.Message}. Please enter the correct format.");
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }

                // Pause before clearing the screen and showing the menu again
                Console.WriteLine("\nPress any key to return to the menu...");
                Console.ReadKey(); // Pause until the user presses any key
            }
        }

    }
}

