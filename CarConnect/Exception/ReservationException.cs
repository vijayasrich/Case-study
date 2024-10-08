using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Exception
{
    public class ReservationException : IOException
    {
        public ReservationException(string message) : base(message)
        {
        }
    }
}