using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Exception
{
    public class VehicleNotFoundException : IOException
    {
        public VehicleNotFoundException(string message) : base(message)
        {
        }
    }
}