using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Model
{
    public class Admin
    {
        public int AdminID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime JoinDate { get; set; }


        public Admin() { }


        public Admin(int adminId, string firstName, string lastName, string email,
                     string phoneNumber, string username, string password,
                     string role, DateTime joinDate)
        {
            AdminID = adminId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Username = username;
            Password = password;
            Role = role;
            JoinDate = joinDate;
        }
        public bool Authenticate(string password)
        {
            return Password == password;
        }


        public void UpdateDetails(string firstName, string lastName, string email, string phoneNumber, string username, string password, string role)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Username = username;
            Password = password;
            Role = role;
        }
    }
}
