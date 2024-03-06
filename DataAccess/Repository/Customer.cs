using DataAccess.Interface;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    // Enum 
    public enum OrderStatuses
    {
        // For Manager + Customer + Driver
        Pending,
        Processing,
        Delivering,
        Dispatched,
        Delivered,
        Returned,
        Cancelled,
    }

    public class Customer : IProduct
    {
        // Getting the details of all the products
        public void GetAllProducts()
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // Getting the details of a specific product using it's id
        public void GetProductDetails(int productId)
        {
            try
            {
                using (var dbContext = new LMSDbContext())
                {
                    var desiredProduct = dbContext.Inventories.Select(row => row).Where(product => product.Id == productId).First();

                    Console.WriteLine("Product Details");
                    Console.WriteLine($"Product Id: {desiredProduct.Id}");
                    Console.WriteLine($"Product Name: {desiredProduct.ProductName}");
                    Console.WriteLine($"Product Description: {desiredProduct.ProductDescription}");
                    Console.WriteLine($"Product Price: {desiredProduct.Price}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void BuyProduct(int userId, int inventoryId, int quantity)
        {
            try
            {
                using (var dbContext = new LMSDbContext())
                {

                    // Insert into Orders table
                    Order newOrder = new Order { UserId = userId, OrderDate = DateTime.Now };

                    dbContext.Orders.Add(newOrder);
                    dbContext.SaveChanges();
                    // Retrieve the auto-generated order ID
                    int orderId = newOrder.Id;
                    //var product = GetProductDetails(inventoryId);
                    var product = dbContext.Inventories.Select(row => row).Where(p => p.Id == inventoryId).First();


                    OrderDetail orderDetail = new OrderDetail
                    {
                        OrderId = orderId,
                        InventoryId = product.Id,
                        Quantity = quantity,
                        TotalAmount = (decimal)product.Price * quantity,
                        OrderStatus = OrderStatuses.Pending.ToString()
                    };

                    dbContext.OrderDetails.Add(orderDetail);
                    dbContext.SaveChanges();

                    // Change the quantity of that product
                    ChangeProductQuantity(inventoryId, quantity);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ChangeProductQuantity(int productId, int orderedQuantity)
        {
            try
            {
                using (var dbContext = new LMSDbContext())
                {

                    var product = dbContext.Inventories.Select(row => row).Where(p => p.Id == productId).First();
                    product.ProductQuantity -= orderedQuantity;

                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ViewOrders(int userId)
        {
            try
            {
                using (var dbContext = new LMSDbContext())
                {
                    var orderData = dbContext.Orders
                        .Where(o => o.UserId == userId) // Specify the specific user ID
                        .Include(o => o.OrderDetails)
                            .ThenInclude(od => od.Inventory)
                        .SelectMany(o => o.OrderDetails.Select(od => new
                        {
                            ProductId = od.Inventory.Id,
                            ProductName = od.Inventory.ProductName,
                            OrderDate = od.Order.OrderDate
                        }))
                        .ToList();
                    Console.WriteLine("Product Id --> Product Name --> Order Date");
                    foreach (var item in orderData)
                    {
                        Console.WriteLine($"{item.ProductId} --> {item.ProductName} --> {item.OrderDate.ToString("dd/MM/yyyy")}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ViewOrderDetails(int userId, int orderId)
        {
            try
            {
                using (var dbContext = new LMSDbContext())
                {
                    var orderDetails = dbContext.Orders
                      .Where(o => o.UserId == userId && o.Id == orderId)
                      .Include(o => o.OrderDetails)
                          .ThenInclude(od => od.Inventory)
                      .SelectMany(o => o.OrderDetails.Select(od => new
                      {
                          ProductId = od.InventoryId,
                          ProductName = od.Inventory.ProductName,
                          ProductDescription = od.Inventory.ProductDescription,
                          OrderedQuantity = od.Quantity,
                          OrderDate = o.OrderDate,
                          TotalAmount = od.TotalAmount,
                          OrderStatus = od.OrderStatus
                      }))
                      .ToList();


                    Console.WriteLine("Order Details");
                    foreach (var item in orderDetails)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Product Id: {item.ProductId}");
                        Console.WriteLine($"Product Name: {item.ProductName}");
                        Console.WriteLine($"Product Description: {item.ProductDescription}");
                        Console.WriteLine($"Product Quantity: {item.OrderedQuantity}");
                        Console.WriteLine($"Total Amount: {item.TotalAmount}");
                        Console.WriteLine($"Order Date: {item.OrderDate.ToString("dd/MM/yyyy")}");
                        Console.WriteLine($"Order Status: {item.OrderStatus}");
                    }
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

