using System;

namespace WebAppShared.ModelsApi
{
    public partial class DeletedUser
    {
        public int Id { get; set; }
        public DateTime DeletedDate { get; set; }
        public Guid DeletedBy { get; set; }


        public Guid UserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserImg64 { get; set; }

        public int SortNo { get; set; }
        public bool IsActive { get; set; }

        public Guid RoleId { get; set; }

        public int SourcePartnerId { get; set; }

        public string ReferredById { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }

}