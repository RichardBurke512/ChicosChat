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
    public class UserDetails_adapter
    {
        private EmpDBContext db = new EmpDBContext();

        public void GetUserDetails()
        {

        }

        public UserDetails LoginUser(string mUser, string mPassword)
        {
            var requestUser = (from e in db.UserDetails
                               where e.UserName == mUser && e.Password == mPassword || e.Email == mUser && e.Password == mPassword
                               orderby e.UserID
                               select e).ToList();

            if (requestUser.Count != 0)
            {
                //If username and password is correct
                UserDetails RecievedtUser = new UserDetails();
                RecievedtUser = requestUser[0];
                return RecievedtUser;
            }
            else
            {
                //If username and or password is incorrect
                return null;
            }




 
        }

        public List<FriendInfoModel> GetFriendList()
        {
            List<FriendInfoModel> FriendList = new List<FriendInfoModel>();

            FriendInfoModel recievedFriend1 = new FriendInfoModel();
            recievedFriend1.FriendUserID = 1;
            recievedFriend1.FriendUserName = "John";
            FriendList.Add(recievedFriend1);
            FriendInfoModel recievedFriend2 = new FriendInfoModel();
            recievedFriend2.FriendUserID = 2;
            recievedFriend2.FriendUserName = "Alan";
            FriendList.Add(recievedFriend2);
            FriendInfoModel recievedFriend3 = new FriendInfoModel();
            recievedFriend3.FriendUserID = 3;
            recievedFriend3.FriendUserName = "Adam";
            FriendList.Add(recievedFriend3);


            return FriendList;
        }

        public string CreateUser(string mUsername, string mEmail, string mPassword)
        {
            if(!CheckforExistinUsername(mUsername))
            {
                if (!CheckforExistingEmail(mEmail))
                {
                    UserDetails NewUser = new UserDetails();

                    //User definded data
                    NewUser.UserName = mUsername;
                    NewUser.Password = mPassword;
                    NewUser.Email = mEmail;

                    //Default data
                    NewUser.CreationDate = DateTime.Now;
                    NewUser.PasswordAttempts = 0;
                    NewUser.Active = true;

                    //Add user to database
                    db.UserDetails.Add(NewUser);
                    db.SaveChanges();

                    return null;
                }
                else
                {
                    return "Email is already registered";
                }
            }
            else
            {
                return "Username is not available";
            }




        }

        private bool CheckforExistinUsername(string mUsername)
        {
            var requestUser = (from e in db.UserDetails
                               where e.UserName == mUsername 
                               orderby e.UserID
                               select e).ToList();

            if (requestUser.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckforExistingEmail(string mEmail)
        {
            var requestUser = (from e in db.UserDetails
                               where e.Email == mEmail
                               orderby e.UserID
                               select e).ToList();

            if (requestUser.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void DataRequest()
        {

        }

        public class EmpDBContext : DbContext
        {
            public EmpDBContext()
            {
                string conn = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
                Database.Connection.ConnectionString = conn;
            }

            public DbSet<UserDetails> UserDetails { get; set; }            
        }



        //Notes
        //object[] xparams = 
        //    {
        //        new SqlParameter("@UserName",mUsername),
        //        new SqlParameter("@Password", mPassword)
        //    };

        //var result = db.Database
        //    .SqlQuery<UserDetails>("LoginUser @UserName, @Password", xparams)
        //    .ToList();

    }
}
