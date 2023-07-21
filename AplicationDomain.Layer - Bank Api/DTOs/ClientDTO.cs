using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationDomain.Layer___Bank_Api.DTOs
{
    public class ClientDTO
    {
        public string? ClientFullName { get; set; }
        public int? ClientAge { get; set; }
        public string? ClientEmail { get; set; }
        public string? ClientNotification { get; set; }
    }
}
