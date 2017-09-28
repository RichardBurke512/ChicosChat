using ChicosChat.Models;
using System;//
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ChicosChat.Services
{
    public class Messages_adapter
    {
        private int cUserID = 0;
        private string cUsername = string.Empty;

        public class EmpDBContext : DbContext
        {
            public EmpDBContext()
            {
                string conn = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
                Database.Connection.ConnectionString = conn;
            }

            public DbSet<MessageModel> Message { get; set; }
        }

        private EmpDBContext db = new EmpDBContext();

        public Messages_adapter(int mUserID, string mUsername)
        {
            cUserID = mUserID;
            cUsername = mUsername;
        }

   

        public List<MessageModel> GetConversationBetweenUsers(int FriendID)
        {

            //var Conversation = (from e in db.Message
            //                    where e.SentBy == cUserID && e.SentTo == FriendID || e.SentBy == FriendID && e.SentTo == cUserID
            //                    orderby e.SentTime
            //                    select e).ToList();

            var Conversation = (from e in db.Message
                                orderby e.SentTime
                                select e).ToList();

            //var Conversation = (from dbMessage in db.Message
            //                     join dbUserDetails in db.UserDetails on dbMessage.SentBy equals dbUserDetails.UserID
            //                     orderby dbMessage.SentTime
            //                     select new
            //                     {
            //                         SentUsername = dbUserDetails.UserName,
            //                         Message = dbMessage.Message,
            //                         SentTime = dbMessage.SentTime
            //                     }).Take(10);



            return Conversation;
        }

        public void SendMessagetoFriend(MessageModel NewMessage)
        {
       

            //User definded data
            NewMessage.SentBy = cUserID;
            NewMessage.SentByUsername = cUsername;
            NewMessage.SentTime = DateTime.Now;

            //Add user to database
            db.Message.Add(NewMessage);
            db.SaveChanges();

        }




      


    }
}
