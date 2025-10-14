using Microsoft.EntityFrameworkCore;
using Task10.Data;
using Task10.Models;

namespace Task10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ApplicationDbContext db = new();
            var customers = db.Customers.AsQueryable();
            var orders = db.Orders.AsQueryable();
            var categories = db.Categories.AsQueryable();
            var products = db.Products.AsQueryable();
            var orderItems = db.OrderItems.AsQueryable();
            var stores = db.Stores.AsQueryable();
            var stocks = db.Stocks.AsQueryable();
            var staffs = db.Staffs.AsQueryable();
            var brands = db.Brands.AsQueryable();

            #region Q1
            //var filterCustomers = customers.Select(c => new {
            //c.FirstName,
            //c.LastName,
            //c.Email
            //}).ToList();
            //foreach (var item in filterCustomers)
            //{
            //    Console.WriteLine($"First Name: {item.FirstName}  Last Name: {item.LastName}  Email: {item.Email}");
            //}
            #endregion

            #region Q2
            //var filterOrders = orders.Where(o => o.StaffId == 3);
            //foreach (var item in filterOrders)
            //{
            //    Console.WriteLine($"Order ID: {item.OrderId} Staff ID: {item.StaffId}");
            //}

            #endregion

            #region Q3
            //var filteredCat = categories.Where(c => c.CategoryName == "Mountain Bikes");
            //var joiningCatWithProd = products.Join(filteredCat,p => p.CategoryId, c => c.CategoryId , (p, c) => new
            //{
            //    p.ProductId,
            //    p.ProductName,
            //    c.CategoryId,
            //    c.CategoryName
            //});
            //foreach (var item in joiningCatWithProd)
            //{
            //    Console.WriteLine($"Product: {item.ProductName}, Category: {item.CategoryName}");
            //}

            #endregion

            #region Q4
            //var countOrders = orders.GroupBy(o => o.StoreId).Select(o => new
            //{
            //    o.Key,
            //    count = o.Count()
            //});
            //foreach (var item in countOrders)
            //{
            //    Console.WriteLine(item.count);
            //}
            #endregion

            #region Q5
            //    var notShippedYet = orders.Where(o=> o.ShippedDate == null).ToList();
            //    foreach (var item in notShippedYet)
            //    {
            //    Console.WriteLine($"Order {item.OrderId} is not shipped yet");
            //    }
            #endregion

            #region Q6
            //var groupedOrders = orders.GroupBy(o => o.CustomerId).Select(c => new
            //{
            //    c.Key,
            //    count = c.Count()
            //});
            //var customerOrders = groupedOrders.Join(db.Customers, o => o.Key, c => c.CustomerId, (o, c) => new
            //{
            //    c.FirstName,
            //    c.LastName,
            //    o.count
            //});

            //foreach (var item in customerOrders)
            //{
            //    Console.WriteLine($"Customer name: {item.FirstName} {item.LastName} \n Num of Orders: {item.count}");
            //}
            #endregion

            #region Q7
            //var productsInOrderItems = orderItems.Select(o => o.ProductId);
            //var productsNotInOrderItems = products.Where(p => !productsInOrderItems.Contains(p.ProductId)).ToList();
            //foreach (var item in productsNotInOrderItems)
            //{
            //    Console.WriteLine($"Product: {item.ProductName} is not ordered yet");
            //}
            #endregion

            #region Q8
            ////method 1
            //var ProductsIds = stocks.Where(q => q.Quantity < 5).Select(s => s.ProductId);
            //var stockProducts = products.Where(p => ProductsIds.Contains(p.ProductId));
            //foreach (var item in stockProducts)
            //{
            //    Console.WriteLine($"Product {item.ProductName} has quantity less than 5");

            //}
            ////method 2
            //var qLessThan5 = stocks.Where(q => q.Quantity < 5);
            //var stockProductsJoinStock = qLessThan5.Join(products, s => s.ProductId, sp => sp.ProductId, (s, sp) => new
            //{
            //    s.ProductId,
            //    s.Quantity,
            //    sp.ProductName
            //});
            //foreach (var item in stockProductsJoinStock)
            //{
            //    Console.WriteLine($"Product {item.ProductName} has quantity {item.Quantity}");
            //}

            #endregion

            #region Q9
            //var firstProduct = products.First();
            //Console.WriteLine(firstProduct.ProductName);
            #endregion

            #region Q10
            //var productsModelGrouping = products.GroupBy(p => p.ModelYear).Select(p => new
            //{
            //    p.Key,
            //    Count = p.Count()
            //});
            //foreach (var item in productsModelGrouping)
            //{
            //    Console.WriteLine($"Products count: {item.Count}, Product model year: {item.Key}");
            //}
            #endregion

            #region Q11
            //var timesProductOrdered = orderItems.Join(db.Products, oi => oi.ProductId, p => p.ProductId, (oi, p) => new
            //{
            //    oi.Quantity,
            //    p.ProductId,
            //    p.ProductName
            //}).GroupBy(p => p.ProductId).Select(p => new
            //{
            //    p.Key,
            //    Count = p.Count()
            //});
            //foreach (var item in timesProductOrdered)
            //{
            //    Console.WriteLine($"Product Ordered: {item.Count} times, Product ID: {item.Key}");
            //}
            #endregion

            #region Q12
            //var numOfProdInCat = products.GroupBy(p => p.CategoryId).Select(p => new
            //{
            //    p.Key,
            //    Count = p.Count()
            //});

            //foreach (var item in numOfProdInCat)
            //{
            //    Console.WriteLine($"Category ID: {item.Key}, Num of Products: {item.Count}");
            //}
            #endregion

            #region Q13
            //var avgListPrice = products.Select(p => p.ListPrice).Average();
            //Console.WriteLine($"Avarage List Price is: {avgListPrice}");
            #endregion

            #region Q14
            //var specificProduct = products.Where(p => p.ProductId == 9);
            //foreach (var item in specificProduct)
            //{
            //    Console.WriteLine(item.ProductName);
            //}
            #endregion

            #region Q15
            //var orderedProductsInOrderItems = orderItems.Where(op => op.Quantity > 3).Select(op => op.ProductId).ToList();
            //var orderedProducts = products.Where(p => orderedProductsInOrderItems.Contains(p.ProductId));
            //foreach (var item in orderedProducts)
            //{
            //    Console.WriteLine($"Product {item.ProductName} has quantity more than 3");
            //}
            //                                   !!NOTE!!
            // There is no quantities in order items greater than 3, so i modified th db manually
            #endregion

            #region Q16
            //var staffOrders = orders.GroupBy(o => o.StaffId).Select(s => new
            //{
            //    Id = s.Key,
            //    count = s.Count()
            //});
            //var staffMembers = staffOrders.Join(db.Staffs, so => so.Id, s => s.StaffId, (so, s) => new
            //{
            //    s.FirstName,
            //    s.LastName,
            //    so.count
            //});

            //foreach (var item in staffMembers)
            //{
            //    Console.WriteLine($"Member Name: {item.FirstName} {item.LastName}, Number of orders: {item.count}");
            //}
            #endregion

            #region Q17
            //var activeStaffMembers = staffs.Where(s => s.Active == 1).Select(s => new
            //{
            //    s.FirstName,
            //    s.LastName,
            //    s.Phone,
            //    s.Active
            //});
            //foreach (var item in activeStaffMembers)
            //{
            //    Console.WriteLine($"Member Name: {item.FirstName} {item.LastName}, Phone Num: {item.Phone}, Activity Status: {item.Active}");
            //}
            #endregion

            #region Q18
            //var listOfProducts = products.Select(p => new
            //{
            //    p.BrandId,
            //    p.CategoryId
            //});

            //var listOfProductsWithNames = listOfProducts.Join(brands, lop => lop.BrandId, b => b.BrandId, (lop, b) => new
            //{
            //    b.BrandId,
            //    lop.CategoryId,
            //    b.BrandName
            //}).Join(categories, lopwn => lopwn.CategoryId, c => c.CategoryId, (lopwn, c) => new
            //{
            //    c.CategoryName,
            //    lopwn.BrandName
            //});

            //foreach (var item in listOfProductsWithNames)
            //{
            //    Console.WriteLine($"Product Brand Name: {item.BrandName}, Product Category Name: {item.CategoryName}");
            //}
            #endregion

            #region Q19
            //var completedOrders = orders.Where(o => o.ShippedDate != null);

            //foreach (var item in completedOrders)
            //{
            //    Console.WriteLine($"Order ID: {item.OrderId}, Order Shipping Date: {item.ShippedDate}");
            //}
            #endregion

            #region Q20
            var totalQuantityOfProductSold = orderItems.GroupBy(p => p.ProductId).Select(oi => new
            {
                ProductId = oi.Key,
                totalQuantitySold = oi.Sum(o => o.Quantity)
            }).OrderBy(p => p.ProductId);
            foreach (var item in totalQuantityOfProductSold)
            {
                Console.WriteLine($"Product ID: {item.ProductId}, Total Quantity Sold: {item.totalQuantitySold}");
            }
            #endregion
        }
    }
}
