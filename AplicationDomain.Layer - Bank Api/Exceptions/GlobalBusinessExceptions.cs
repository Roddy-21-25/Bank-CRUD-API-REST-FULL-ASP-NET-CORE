using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationDomain.Layer___Bank_Api.Exceptions
{
    public class GlobalBusinessExceptions : Exception
    {
        public GlobalBusinessExceptions(string message) : base(message)
        {
            
        }
    }
}
