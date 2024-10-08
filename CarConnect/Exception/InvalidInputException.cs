using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Exception
{
    public class InvalidInputException : IOException
    {
        public InvalidInputException(string message) : base(message)
        {
        }
    }
}