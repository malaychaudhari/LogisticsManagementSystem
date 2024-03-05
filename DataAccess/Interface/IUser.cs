using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
    internal interface IUser
    {
        string Name { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        string PhoneNumber { get; set; }
    }
}
