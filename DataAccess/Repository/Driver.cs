using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Driver
    {
        public void ViewAssignedOrders(int userId)
        {
            try
            {
                using (var dbContext = new LMSDbContext())
                {
                    var assignedOrderes = dbContext.Resources
                        .Where(r => r.UserId == userId)
                        .Include(r => r.ResourceMappings)
                            .SelectMany(r => r.ResourceMappings.Select(rm => rm.OrderDetailsId))
                            .ToList();

                    Console.WriteLine("Assigned Orders for you");
                    Console.WriteLine();

                    foreach (var order in assignedOrderes)
                    {
                        Console.WriteLine($"Order Details Number: {order}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void UpdateStatusForSpecificOrder(int userId, int orderDetailsId)
        {
            try
            {
                using (var dbContext = new LMSDbContext())
                {
                    var ordersForUser = dbContext.Resources
                         .Where(r => r.UserId == userId)
                         .Include(r => r.ResourceMappings)
                             .ThenInclude(rm => rm.OrderDetails)
                         .SelectMany(r => r.ResourceMappings.Select(rm => rm.OrderDetails))
                         .ToList();


                    foreach (var order in ordersForUser)
                    {
                        if (order.Id == orderDetailsId)
                        {
                            if (order.OrderStatus == OrderStatuses.Pending.ToString())
                            {
                                order.OrderStatus = OrderStatuses.Dispatched.ToString();
                            } else if (order.OrderStatus == OrderStatuses.Dispatched.ToString())
                            {
                                order.OrderStatus = OrderStatuses.Delivered.ToString();
                            } 
                            dbContext.SaveChanges();
                        }
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
