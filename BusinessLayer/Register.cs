using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLayer
{

    public class Register
    {
        public string Name, Email, Password, PhoneNumber;
        public int RoleId;

        public bool RegisterUser(string name, string email, string password, string phoneNumber, int roleId)
        {
            // Validate input
            if (!IsValidUsername(name) || !IsValidEmail(email) || !IsValidPassword(password) || !IsValidPhoneNumber(phoneNumber))
            {
                return false;
            }

            using (var dbContext = new LMSDbContext())
            {
                User newUser = new User
                {
                    Name = name,
                    Email = email,
                    Password = password,
                    PhoneNumber = phoneNumber,
                    RoleId = roleId
                };

                dbContext.Users.Add(newUser);
                dbContext.SaveChanges();
            }

            return true;
        }

        private bool IsValidUsername(string username)
        {
            // Implement username validation logic (e.g., length constraints, allowed characters)
            return !string.IsNullOrWhiteSpace(username) && username.Length >= 3 && username.Length <= 50;
        }

        private bool IsValidEmail(string email)
        {
            // Implement email validation logic (e.g., format check)
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" + @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            
            if (re.IsMatch(email))
                return (true);
            else
                return (false);
            // return !string.IsNullOrWhiteSpace(email) && email.Contains("@");
        }

        private bool IsValidPassword(string password)
        {
            // Implement password validation logic (e.g., length constraints, complexity requirements)
            return !string.IsNullOrWhiteSpace(password) && password.Length >= 6;
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // Implement phone number validation logic (e.g., format check)
            // For simplicity, let's assume a valid phone number has exactly 10 digits
            return !string.IsNullOrWhiteSpace(phoneNumber) && phoneNumber.Length == 10 && phoneNumber.All(char.IsDigit);
        }
    }
}

