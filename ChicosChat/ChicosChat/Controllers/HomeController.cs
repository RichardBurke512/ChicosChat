using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChicosChat.Models;
using static ChicosChat.Services.UserDetails_adapter;
using ChicosChat.Services;
using Newtonsoft.Json;

namespace ChicosChat.Controllers
{
    public class HomeController : Controller
    {

        //Login Page
        #region "Login Page"

        [HttpGet]
        public IActionResult Login()
        {
            ViewData["Message"] = "Chicos Chat Login";
            var model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            UserDetails_adapter UserRequest = new UserDetails_adapter();
            UserDetails RecievedUser = UserRequest.LoginUser(model.Username, model.Password);

            if (RecievedUser != null)
            {
                TempData["UserID"] = RecievedUser.UserID.ToString();
                TempData["Username"] = RecievedUser.UserName.ToString();
                return RedirectToAction("Chat");
            }
            else
            {
                ViewData["LoginError"] = "Incorrect Username or Password";
                return View();
            }
        }

        #endregion


        //Register Page
        #region "Register Page"

        [HttpGet]
        public IActionResult Register()
        {
            ViewData["Message"] = "Chicos Chat Login";
            var model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Register(LoginViewModel model)
        {
            if (model.Password == model.RetypePassword)
            {

                UserDetails_adapter UserRequest = new UserDetails_adapter();
                string result = UserRequest.CreateUser(model.Username, model.Email, model.Password);

                if (result != null)
                {
                    ViewData["LoginError"] = result;
                    return View();
                }
                else
                {
                    TempData["Username"] = model.Username;
                    return RedirectToAction("Chat");
                }                
            }
            else
            {
                ViewData["LoginError"] = "Passwords do not match.";
                return View();
            }
        }

        #endregion


        //Chat Page
        #region "Chat Page"

        public IActionResult Chat(UserDetailsModel model)
        {
            string mUserID = TempData["UserID"] as string;
            string mUsername = TempData["Username"] as string;

            if (mUserID != null)
            {
                model.UserID = Convert.ToInt32(mUserID);
                if (model.FriendList == null || model.FriendList.Count == 0)
                {
                    UserDetails_adapter UserRequest = new UserDetails_adapter();
                    model.FriendList = UserRequest.GetFriendList();                
                }

                if (model.NewMessage == null)
                {
                    model.NewMessage = new MessageModel();
                }
        
                Messages_adapter MessageAdp = new Messages_adapter(model.UserID, mUsername);
                model.Conversation = MessageAdp.GetConversationBetweenUsers(0);

                TempData.Keep();               
                return View("Chat", model);
            }
            else
            {
                TempData["Username"] = null;
                return RedirectToAction("Login");
            }
        }

        //[HttpPost]
        //public IActionResult Chat()
        //{
        //    int mUserID = Convert.ToInt32(TempData["UserID"] as string);

        //    model.NewMessage.SentTo = 2; //Hard code Tom

        //    Messages_adapter MessageAdp = new Messages_adapter(mUserID);
        //    MessageAdp.SendMessagetoFriend(model.NewMessage.SentTo, model.NewMessage.Message);


        //    TempData.Keep();
        //    return View(model);
        //}

        public IActionResult Message_Send(UserDetailsModel model)
        {
            int mUserID = Convert.ToInt32(TempData["UserID"] as string);
            string mUsername = TempData["Username"] as string;

            model.NewMessage.SentTo = 2; //Hard code Tom

            Messages_adapter MessageAdp = new Messages_adapter(mUserID, mUsername);
            MessageAdp.SendMessagetoFriend(model.NewMessage);

            TempData.Keep();
            return Chat(model);
        }

        [HttpPost]
        public IActionResult Friend_Search(UserDetailsModel model)
        {
            TempData.Keep();
            return View();
        }


        #endregion




        public IActionResult Index()
        {

            return View();
        }


        public IActionResult Error()
        {
            return View();
        }
    }
}
