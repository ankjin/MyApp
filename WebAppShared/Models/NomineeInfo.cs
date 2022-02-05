using System;
using System.ComponentModel.DataAnnotations.Schema;
using WebAppShared.ModelsApi;

namespace WebAppShared.Models
{
    public class NomineeInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Relationship { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string IDType1 { get; set; }
        public string IDNumber1 { get; set; }
        public string IDType2 { get; set; }
        public string IDNumber2 { get; set; }
        public string NomineeAddress { get; set; }

        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

    }
}
