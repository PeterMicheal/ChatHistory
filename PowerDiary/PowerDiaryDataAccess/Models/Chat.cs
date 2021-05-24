using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PowerDiaryDataAccess.Models
{
    public enum EventTypeEnum
    {
        Enter = 1,
        Leave = 2,
        Comment = 3,
        HighFive = 4
    }

    public class Chat
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
        public EventTypeEnum EventTypeId { get; set; }
        public int UserId { get; set; }
        public int? UserToId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("UserToId")]
        public virtual User UserTo { get; set; }
    }
}
