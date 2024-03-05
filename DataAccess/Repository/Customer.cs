using DataAccess.Interface;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Customer : IProduct, ICustomer
    {
        public void BuyProduct()
        {
            throw new NotImplementedException();
        }

        //private LMSDbContext _dbContext;
        //private Customer(LMSDbContext dbContext)
        //{
        //    _dbContext = dbContext;
        //}

        // Getting the details of all the products
        public void GetAllProducts()
        {
            using (var dbContext = new LMSDbContext())
            {
                var rows = dbContext.Inventories.Select(row => row).Where(product => product.IsActive == true);

                Console.WriteLine("Product ID --> Product Name");
                foreach (var item in rows)
                {
                    Console.WriteLine($"{item.Id} --> {item.ProductName}");
                }
                Console.WriteLine(Environment.NewLine);
            }
        }

        // Getting the details of a specific product using it's id
        public void GetProductDetails(int id)
        {
            using (var dbContext = new LMSDbContext())
            {
                var desiredProduct = dbContext.Inventories.Select(row => row).Where(product => product.Id == id).First();

                Console.WriteLine("Product Details");
                Console.WriteLine($"Product Id: {desiredProduct.Id}");
                Console.WriteLine($"Product Name: {desiredProduct.ProductName}");
                Console.WriteLine($"Product Description: {desiredProduct.ProductDescription}");
                Console.WriteLine($"Product Price: {desiredProduct.Price}");
            }
        }

        public void OrderDetails(int id)
        {
            throw new NotImplementedException();
        }

        public void Orders()
        {
            throw new NotImplementedException();
        }

        //public void BuyProduct(int userId)
        //{
        //    using (var dbContext = new LMSDbContext())
        //    {
        //        // Insert into Orders table
        //        Order newOrder = new Order { UserId = userId, OrderDate = DateTime.Now };

        //        dbContext.Orders.Add(newOrder);
        //        dbContext.SaveChanges();

        //        // Retrieve the auto-generated order ID
        //        int orderId = newOrder.Id;



        //    }
        //}
    }
}
