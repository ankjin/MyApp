using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppShared.Models
{
    public class InvestmentPayout
    {
        public Guid Id { get; set; }
        public Guid InvestmentModelId { get; set; }
        [ForeignKey("InvestmentModelId")]
        public virtual InvestmentModel InvestmentModel { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime PayoutDate { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal PayoutAmount { get; set; }
        public ModeOfPayment ModeOfPayment { get; set; }


        public int BankingInfoId { get; set; }
        //[ForeignKey("BankingInfoId")]
        //public virtual BankingInfo BankingInfo { get; set; }


        public bool IsActive { get; set; }

    }
}
