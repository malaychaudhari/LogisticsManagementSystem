using DataAccess.Models;
using DataAccess.Repository;
using System.Net.Http.Headers;

namespace LogisticsManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            //Customer c = new Customer();
            //c.GetAllProducts();
            //c.GetProductDetails(8);
            //c.BuyProduct(3, 7, 2);
            //c.ViewOrders(3);
            //c.ViewOrderDetails(2, 9);

            Driver d = new Driver();
            //d.ViewAssignedOrders(2);
            d.UpdateStatusForSpecificOrder(3, 7);
        }
    }
}
