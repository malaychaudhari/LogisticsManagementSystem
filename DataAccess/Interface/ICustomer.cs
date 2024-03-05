using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
    internal interface ICustomer
    {
        void BuyProduct();
        void Orders();
        void OrderDetails(int id);
    }
}
