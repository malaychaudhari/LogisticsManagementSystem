using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    internal interface IUserManagement
    {
        void CreateUser(User user);
        void DeleteUser(User user);
    }
}
