using System;
using System.ComponentModel.DataAnnotations.Schema;
using WebAppShared.ModelsApi;

namespace WebAppShared.Models
{
    public class BankingInfo
    {
        public int Id { get; set; }
        public BankAccountType AccountType { get; set; }
        public String AccountHolderName { get; set; }
        public string AccountNumber { get; set; }
        public string IFSCCode { get; set; }
        public string BankName { get; set; }
        public string Branch { get; set; }
        public bool IsPrimary { get; set; }

        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public bool IsActive { get; set; }

    }
}
