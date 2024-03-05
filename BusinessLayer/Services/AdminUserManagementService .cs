using BusinessLayer.Interface;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    internal class AdminUserManagementService : IUserManagement
    {
        public void CreateUser(User user)
        {
            Console.WriteLine($"Admin created user '{user.Name}' with role ID '{user.RoleId}'.");
        }

        public void DeleteUser(User user)
        {
            Console.WriteLine($"Admin deleted user '{user.Name}'.");
        }
    }
}
