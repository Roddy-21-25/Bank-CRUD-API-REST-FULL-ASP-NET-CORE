using System;
using System.Collections.Generic;

namespace AplicationDomain.Layer___Bank_Api.Entities
{
    public partial class ClientAccount : BaseIdEntity
    {
        public ClientAccount()
        {
            BankAccounts = new HashSet<BankAccount>();
        }

        public string? AccountName { get; set; }
        public int? AccountAmount { get; set; }
        public int? ClientIdAccount { get; set; }
        public string? AccountTransaccion { get; set; }
        public string? AccountPaymentHistory { get; set; }
        public string? AccountNotification { get; set; }
        public string? AccountCardType { get; set; }
        public string? AccountBadge { get; set; }

        public virtual Client? ClientIdAccountNavigation { get; set; }
        public virtual ICollection<BankAccount> BankAccounts { get; set; }
    }
}
