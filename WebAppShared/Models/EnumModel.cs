using System.ComponentModel.DataAnnotations;

namespace WebAppShared.Models
{

    public enum BankAccountType
    {
        [Display(Name = "Savings")]
        Savings = 1,
        [Display(Name = "Current")]
        Current = 2
    }

    public enum PlanType
    {
        [Display(Name = "Monthly")]
        Monthly = 1,
        [Display(Name = "Half Yearly")]
        Half_Yearly = 2,
        [Display(Name = "Yearly")]
        Yearly = 3
    }

    public enum ModeOfPayment
    {
        [Display(Name = "Cash")]
        Cash = 1,
        [Display(Name = "NEFT")]
        NEFT = 2
    }

}
