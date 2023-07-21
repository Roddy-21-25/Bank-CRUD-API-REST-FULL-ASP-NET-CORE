using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationDomain.Layer___Bank_Api.Entities
{
    public class PasswordOptionsValues
    {
        public int SaltSize { get; set; }
        public int KeySize { get; set; }
        public int Iterations { get; set; }
    }
}
