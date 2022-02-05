using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAppShared.ModelsApi
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string RoleName { get; set; }
        public string RoleDesc { get; set; }
        public int SortNo { get; set; }
        public bool IsActive { get; set; }

        public ICollection<User> Users { get; set; }
    }
}