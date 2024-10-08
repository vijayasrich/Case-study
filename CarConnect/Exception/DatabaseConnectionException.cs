using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Exception
{
    public class DatabaseConnectionException : IOException
    {
        public DatabaseConnectionException(string message) : base(message)
        {
        }

        public DatabaseConnectionException(string? message, System.Exception? innerException) : base(message, innerException)
        {
        }
    }
}