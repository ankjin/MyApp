using System;

namespace WebAppShared.Models
{
    public class CalculatorModel
    {
        public int Id { get; set; }



        public PlanType PlanType { get; set; }


        public decimal InvestmentAmount { get; set; }
        public DateTime InvestDate { get; set; }
        public DateTime InvestDateMaturity { get; set; }
        public int RateReturn { get; set; }


        public DateTime FirstPayout { get; set; }
        public int TermsPerAnnum { get; set; }
        public decimal PayoutAmount { get; set; }


        public DateTime LastPayoutDate { get; set; }
        public int TermsPerThreeYears { get; set; }

        public DateTime FirstWithdrawalSlab { get; set; }
        public DateTime SecondWithdrawalSlab { get; set; }
        public DateTime FinalWithdrawalSlab { get; set; }

    }
}
