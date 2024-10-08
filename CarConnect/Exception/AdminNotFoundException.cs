using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Exception
{
    public class AdminNotFoundException : IOException
    {
        public AdminNotFoundException(string message) : base(message)
        {
        }
    }
}
