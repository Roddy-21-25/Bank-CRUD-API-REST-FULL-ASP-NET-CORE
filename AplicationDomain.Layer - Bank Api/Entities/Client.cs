using System;
using System.Collections.Generic;

namespace AplicationDomain.Layer___Bank_Api.Entities
{
    public partial class Client : BaseIdEntity
    {
        public Client()
        {
            BankAccounts = new HashSet<BankAccount>();
            ClientAccounts = new HashSet<ClientAccount>();
        }

        public string? ClientFullName { get; set; }
        public int? ClientAge { get; set; }
        public string? ClientEmail { get; set; }
        public string? ClientPassword { get; set; }
        public string? ClientNotification { get; set; }

        public virtual ICollection<BankAccount> BankAccounts { get; set; }
        public virtual ICollection<ClientAccount> ClientAccounts { get; set; }
    }
}
