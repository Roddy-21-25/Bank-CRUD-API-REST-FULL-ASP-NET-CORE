using System;
using System.Collections.Generic;

namespace AplicationDomain.Layer___Bank_Api.Entities
{
    public partial class BankAccount : BaseIdEntity
    {
        public string? BankUserAdmin { get; set; }
        public string? BankPasswordAdmin { get; set; }
        public int? ClientId { get; set; }
        public int? AccountId { get; set; }
        public int? BankAmount { get; set; }

        public virtual ClientAccount? Account { get; set; }
        public virtual Client? Client { get; set; }
    }
}
