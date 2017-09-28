using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChicosChat.Models
{
    public interface iMessage
    {
         int SentBy { get; set; }
         int SentTo { get; set; }
         string Message { get; set; }
         DateTime SentTime { get; set; }
         DateTime RecievedTime { get; set; }
    }
}
