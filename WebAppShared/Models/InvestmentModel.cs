using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebAppShared.ModelsApi;

namespace WebAppShared.Models
{
    public class InvestmentModel
    {
        public Guid Id { get; set; }

        /// <summary>
        /// I change the from UserId to UserModel because of this error (Introducing FOREIGN KEY constraint may cause cycles or multiple cascade paths)
        /// UserId already been use in public virtual BankingInfo BankingInfo { get; set; }
        /// </summary>
        public Guid UserModelId { get; set; }

        /// <summary> Comment Out the [ForeignKey]
        /// Introducing FOREIGN KEY constraint 'FK_InvestmentModels_Users_UserModelId' on table 'InvestmentModels' may cause cycles or multiple cascade paths. Specify ON DELETE NO ACTION or ON UPDATE NO ACTION, or modify other FOREIGN KEY constraints.
        /// Could not create constraint or index.See previous errors.
        /// </summary>
        [ForeignKey("UserModelId")]
        public virtual User User { get; set; }

        public DateTime CreatedDate { get; set; }

        public string RefNumber { get; set; }
        public PlanType PlanType { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal InvestAmount { get; set; }
        public DateTime InvestDate { get; set; }
        public DateTime InvestDateMaturity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal RateReturn { get; set; }

        public int BankingInfoId { get; set; }
        [ForeignKey("BankingInfoId")]
        public virtual BankingInfo BankingInfo { get; set; }

        public string SourceBankInfo { get; set; }
        public bool Status { get; set; }
        public bool IsActive { get; set; }


        public ICollection<InvestmentPayout> InvestmentPayouts { get; set; }

    }
}
