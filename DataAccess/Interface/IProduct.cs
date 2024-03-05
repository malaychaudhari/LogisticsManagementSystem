using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
    internal interface IProduct
    {
        void GetAllProducts();
        void GetProductDetails(int id);
    }
}
