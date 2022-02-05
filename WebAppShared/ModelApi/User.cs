using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAppShared.Models;

namespace WebAppShared.ModelsApi
{
    public partial class User
    {
        public User()
        {
            RefreshTokens = new HashSet<RefreshToken>();
        }

        public Guid Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public string Mobile { get; set; }

        [Required(ErrorMessage = "First is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last is required")]
        public string LastName { get; set; }

        public string UserImg64 { get; set; }

        public int SortNo { get; set; }
        public bool IsActive { get; set; }


        [Required(ErrorMessage = "Role is required")]
        public Guid RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }


        public int SourcePartnerId { get; set; }
        [ForeignKey("SourcePartnerId")]
        public virtual SourcePartner SourcePartner { get; set; }

        public string ReferredById { get; set; }


        public ICollection<RefreshToken> RefreshTokens { get; set; }
        public ICollection<InvestmentModel> InvestmentModels { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }

}