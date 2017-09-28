using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChicosChat.Models
{
    [NotMapped]
    public class UserDetailsModel : UserDetails
    {
        public List<FriendInfoModel> FriendList = new List<FriendInfoModel>();

        public List<MessageModel> Conversation = new List<MessageModel>();

        public MessageModel NewMessage { get; set; }




    }
}
