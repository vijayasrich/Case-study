using CarConnect.Dao;
using CarConnect.Model;
using NUnit.Framework;
using System.Collections.Generic;

namespace CarConnect.Tests
{
    [TestFixture]
    public class CarConnectTests
    {
        private ICustomerService customerService;
        private IVehicleService vehicleService;

        [SetUp]
        public void Setup()
        {
            // Initialize your services here, possibly with mock data or a test database
            customerService = new CustomerService(); // Replace with actual initialization
            vehicleService = new VehicleService();   // Replace with actual initialization
        }

        [Test]
        public void Test_Customer_Authentication_Invalid_Credentials()
        {
            string invalidUsername = "wrongUser";
            string invalidPassword = "wrongPass";

            var result = customerService.Authenticate(invalidUsername, invalidPassword);
            Assert.That(result, Is.False); // Check for false instead of null
        }
        

        [Test]
        
        public void Test_Updating_Customer_Information()
        {
            // Arrange
            ICustomerService customerService = new CustomerService();
            var existingCustomer = customerService.GetCustomerById(1); // Assuming customer with ID 1 exists

            // Act
            existingCustomer.FirstName = "UpdatedFirstName";
            existingCustomer.LastName = "UpdatedLastName";
            customerService.UpdateCustomer(existingCustomer);

            var updatedCustomer = customerService.GetCustomerById(1);

            // Assert
            Assert.That(updatedCustomer.FirstName, Is.EqualTo("UpdatedFirstName"));
            Assert.That(updatedCustomer.LastName, Is.EqualTo("UpdatedLastName"));
        }

        #region
        /* [Test]
         public void Test_Add_New_Vehicle()
         {
             var newVehicle = new Vehicle
             {
                 // Make sure to include all required fields
                 Model = "Tesla Model S",
                 Make = "Tesla",
                 Year = 2022, 
                 Color="White",
                 RegistrationNumber="cvc234",
                 Availability = true,
                 DailyRate = 120.00
             };

             // Try adding the new vehicle
             Assert.DoesNotThrow(() => vehicleService.AddVehicle(newVehicle)); // Ensure no exception is thrown

             // Verify that the vehicle was added by querying the database
             var addedVehicle = vehicleService.GetVehicleById(newVehicle.VehicleID); // Make sure to retrieve the correct ID here
             Assert.That(addedVehicle, Is.Not.Null); // Check if the vehicle exists
             Assert.That(addedVehicle.Model, Is.EqualTo(newVehicle.Model));
             Assert.That(addedVehicle.Make, Is.EqualTo(newVehicle.Make));
             Assert.That(addedVehicle.Year, Is.EqualTo(newVehicle.Year));
             Assert.That(addedVehicle.Color, Is.EqualTo(newVehicle.Color));
             Assert.That(addedVehicle.RegistrationNumber, Is.EqualTo(newVehicle.RegistrationNumber));
             Assert.That(addedVehicle.Availability, Is.EqualTo(newVehicle.Availability));
             Assert.That(addedVehicle.DailyRate, Is.EqualTo(newVehicle.DailyRate));
         }*/
        #endregion

        [Test]
        public void AddVehicle()
        {
            // Arrange: Create a new vehicle object with the required properties
            var vehicle = new Vehicle
            {
                Model = "Honda",
                Make = "Civic",
                Year = 2023,
                Color = "Blue",
                RegistrationNumber = "ND9ew876", // Change this before running the test
                Availability = true,
                DailyRate = 60.00
            };

            // Act: Add the vehicle to the database using the service
            vehicleService.AddVehicle(vehicle);

            // Act: Retrieve all vehicles from the database
            List<Vehicle> allVehicles = vehicleService.GetAllVehicles();

            // Get the latest vehicle (assuming it's the last one added)
            Vehicle latestVehicle = allVehicles.Last();

            // Assert: Check if the latest vehicle matches the expected properties
            Assert.That(latestVehicle.Model, Is.EqualTo(vehicle.Model));
            Assert.That(latestVehicle.Make, Is.EqualTo(vehicle.Make));
            Assert.That(latestVehicle.Year, Is.EqualTo(vehicle.Year));
            Assert.That(latestVehicle.Color, Is.EqualTo(vehicle.Color));
            Assert.That(latestVehicle.RegistrationNumber, Is.EqualTo(vehicle.RegistrationNumber));
            Assert.That(latestVehicle.Availability, Is.EqualTo(vehicle.Availability));
            Assert.That(latestVehicle.DailyRate, Is.EqualTo(vehicle.DailyRate));
        }


        [Test]
        public void Test_Update_Vehicle_Details()
        {
            // Arrange
            IVehicleService vehicleService = new VehicleService();
            var existingVehicle = vehicleService.GetVehicleById(1); // Assuming vehicle with ID 1 exists

            // Act
            existingVehicle.DailyRate = 130.00;
            vehicleService.UpdateVehicle(existingVehicle);

            var updatedVehicle = vehicleService.GetVehicleById(1);

            // Assert
            Assert.That(updatedVehicle.DailyRate, Is.EqualTo(130.00));
        }


        [Test]
        public void Test_Get_Available_Vehicles()
        {
            var availableVehicles = vehicleService.GetAvailableVehicles();

            Assert.That(availableVehicles, Is.Not.Null);
            Assert.That(availableVehicles.Count, Is.GreaterThan(0)); // Assuming there are available vehicles
        }

        [Test]
        public void Test_Get_All_Vehicles()
        {
            var allVehicles = vehicleService.GetAllVehicles();

            Assert.That(allVehicles, Is.Not.Null);
            Assert.That(allVehicles.Count, Is.GreaterThan(0)); // Assuming there are vehicles in the database
        }
    }
}

