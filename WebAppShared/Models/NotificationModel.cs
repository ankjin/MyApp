using System;
using System.ComponentModel.DataAnnotations.Schema;
using WebAppShared.ModelsApi;

namespace WebAppShared.Models
{
    public class NotificationModel
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }


        public string SourceId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string SourceType { get; set; }
        public string Type { get; set; }
        public bool IsRead { get; set; }
        public bool IsTrash { get; set; }


    }
}
