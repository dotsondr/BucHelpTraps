﻿// ---------------------------------------------------------------------------
// Creator’s name and email: Stephen Maurer Maurers@etsu.edu
// Creation Date: 2/14/2023
// Last Modified: 2/21/2023
// ---------------------------------------------------------------------------
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace BucHelp.Data
{
    /// <summary>
    /// Login page model for verification
    /// </summary>
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential Credential { get; set; }
        private bool loggedIn = false;
        public void SetLogin() 
        {
            loggedIn = true; 
        }
        public void OnGet()
        {

        }
        
        public void OnPost() 
        {

        }

        public LoginModel()
        {
            Credential = new Credential();
        }

        public bool IsLoggedIn()
        {
            return loggedIn;
        }

        public User GetUser(string email)
        {
            List<User> users = UserObjService.GetUserList();

            //LoginPractice.CreateUsers();  Leave as comment

            foreach (User user in users)
            {
                if (user.Email.ToLower().Equals(email.ToLower()))
                {
                    return user;
                }
            }

            return null;
        }
        
    }

    /// <summary>
    /// Credential class used for verification.
    /// </summary>
    public class Credential
    {

        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public Credential()
        {
            Email = "";
            Password = "";
        }

        public bool isValidEmail()
        {
            try
            {
                MailAddress m = new MailAddress(Email);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public class AppState
    {
        private bool _loggedIn;
        public event Action OnChange;
        public bool LoggedIn
        {
            get { return _loggedIn; }
            set
            {
                if (_loggedIn != value)
                {
                    _loggedIn = value;
                    NotifyStateChanged();
                }
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }

    /// <summary>
    /// Created input for users csv file
    /// </summary>
    public class LoginPractice
    {
        private static List<User> UsersList = new List<User>();

        public static void CreateUsers()
        {
            UsersList.Add(new User(1, "1234", "Duck@yahoo.com", "student"));
            UsersList.Add(new User(2, "password", "YoungDuck@yahoo.com", "student"));
            UsersList.Add(new User(3, "123", "Chick@yahoo.com", "student"));
            UsersList.Add(new User(4, "Leader", "DuckMaster@yahoo.com", "faculty"));

            int i = 0;

            foreach (User user in UsersList)
            {
                Console.WriteLine(UsersList[i].UserId);
                UserObjService.Write(user);
                i++;
            }

            DatabaseServices.Drivers.GetDefaultDriver().Commit();

            
        }
    }
}
