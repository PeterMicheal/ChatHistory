using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PowerDiaryDataAccess.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<Chat> Chats { get; set; }

        public virtual List<Chat> ChatsTo { get; set; }
    }
}
