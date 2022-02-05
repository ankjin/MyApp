using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAppShared.ModelsApi;

namespace WebAppShared.Models
{
    public class BankModelSample
    {
        public int Id { get; set; }




        ///// <summary>
        ///// This is necessary in order for DateTime validation to function.
        ///// </summary>
        //protected DateTimeConverter converter = new DateTimeConverter();

        /// <summary>
        /// Year-Month-Day
        /// </summary>
        [Range(typeof(DateTime), "1978-01-04", "2025-04-05", ErrorMessage = "Date for Submit must be between 04-Jan-1978 and 05-Apr-2025.")]
        public DateTime DateSubmit { get; set; }

        [Required]
        [Range(typeof(DateTime), "2000-1-1", "2020-12-31", ErrorMessage = "Date of Birth must between 1-Jan-2000 and 31-Dec-2020")]
        public DateTime DateOfBirth { get; set; }
        [Range(10, 50, ErrorMessage = "Value for Age must be between 10 and 50.")]
        public int Age { get; set; }


        [EnumDataType(typeof(BankAccountType))]
        public BankAccountType AccountType { get; set; }


        [Required(ErrorMessage = "Account name is required")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Account name cannot have less than 4 characters and more than 20 characters in length")]
        public String AccountHolderName { get; set; }
        [Required]
        [StringLength(12, ErrorMessage = "Account number must be less than 12 characters.")]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "Bank name is required")]
        [StringLength(20, ErrorMessage = "Bank name must be less than 20 characters.")]
        public string BankName { get; set; }
        public string Branch { get; set; }
        public bool IsPrimary { get; set; }
        public string IFSCCode { get; set; }

        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public bool IsActive { get; set; }

    }
}
