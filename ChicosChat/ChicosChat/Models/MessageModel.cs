using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChicosChat.Models
{
    public class MessageModel
    {
        [Key]
        public int MessageID { get; set; }
        public int SentBy { get; set; }
        public string SentByUsername { get; set; }
        public int SentTo { get; set; }
        public string Message { get; set; }
        public DateTime SentTime { get; set; }
        public DateTime? RecievedTime { get; set; }

    }
}
