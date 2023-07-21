using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationDomain.Layer___Bank_Api.DTOs
{
    public class ClientAccountDTO
    {
        public string? AccountName { get; set; }
        public int? AccountAmount { get; set; }
        public string? AccountTransaccion { get; set; }
        public string? AccountPaymentHistory { get; set; }
        public string? AccountNotification { get; set; }
        public string? AccountBadge { get; set; }
    }
}
