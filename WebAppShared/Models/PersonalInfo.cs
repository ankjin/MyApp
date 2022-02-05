using System;
using System.ComponentModel.DataAnnotations.Schema;
using WebAppShared.ModelsApi;

namespace WebAppShared.Models
{
    public class PersonalInfo
    {
        public int Id { get; set; }
        public string SoDo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string AlternateNumber { get; set; }
        public string PanCard { get; set; }
        public string AadharCard { get; set; }
        public string Address { get; set; }

        public string ProfileImg { get; set; }
        public string Base64 { get; set; }

        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
