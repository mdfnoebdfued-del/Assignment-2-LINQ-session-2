using LINQ.DataSources;
using LINQ.Models;
using System;
using System.Diagnostics;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace LINQ01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // question1:
            var Top3ExpensivePrice = Source.ProductList
                .OrderByDescending(p => p.UnitPrice)
                .Take(3);
            foreach (var item in Top3ExpensivePrice)
            {
                Console.WriteLine($"{item}");
            }
            // question2:
            int pageSize = 5;
            int pageNumber = 2;

            var page2Products = Source.ProductList
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            foreach (var item in page2Products)
            {
                Console.WriteLine($"{item}");
            }
            // question3:

            var ProductsLlessThan25 = Source.ProductList.
                Where(P => P.UnitPrice < 25)
                .Take(3).
                OrderBy(p => p.UnitPrice);
            foreach (var item in ProductsLlessThan25)
            {
                Console.WriteLine($"{item}");
            }
            //question4:
            var allseaFoodInStock = Source.ProductList.Where(P => P.Category == "Seafood")
                .All(P => P.UnitsInStock > 0);
            Console.WriteLine($"All Seafood products are in stock: {allseaFoodInStock}");
            //question5:
            int[] ids = { 3, 9, 13, 18 };
            var contains9 = ids.Contains(9);
            Console.WriteLine($"The ID list contains 9: {contains9}");
            //question6:
            var GroupedProducts = Source.ProductList.GroupBy(p => p.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    Count = g.Count()
                });
            foreach (var group in GroupedProducts)
            {
                Console.WriteLine($"Category: {group.Category}, Count: {group.Count}");

            }
            //question7:
            var GroupedProducts2 = Source.ProductList.GroupBy(p => p.Category)
              .Select(g => new
              {
                  ProductName = g.Select(p => p.ProductName)
              });
            //question8:
            var CategoryWithMoreThan3Products = Source.ProductList.
                GroupBy(p => p.Category)
                .Select(g => new
                {
                    category = g.Key,
                    Count = g.Count()
                })
                .Where(g => g.Count > 3);
            foreach (var item in CategoryWithMoreThan3Products)
            {
                Console.WriteLine(item);
            }
            //question9:

            var result = from C in Source.CustomerList
                         group C by C.Country into countryGroup
                         select new
                         {
                             country = countryGroup.Key,
                             Count = countryGroup.Count(),
                             TotalOrderValue = countryGroup.Sum(c => c.Orders.Sum(o => o.Total))
                         };
            foreach (var item in result)
            {
                Console.WriteLine($"Country: {item.country}, Count: {item.Count}, Total Order Value: {item.TotalOrderValue:C2}");
            }

            //question10:
            var totalUnitInStock = Source.ProductList.Sum(p => p.UnitsInStock);
            Console.WriteLine(totalUnitInStock);
            //question11:
            var CheapestProductPrice = Source.ProductList.Min(p => p.UnitPrice);
            Console.WriteLine($"Cheapest Product Price: {CheapestProductPrice}");
            var EXPENSIVEProductPrice = Source.ProductList.Max(p => p.UnitPrice);
            Console.WriteLine($"Most Expensive Product Price: {EXPENSIVEProductPrice}");
            // question12:
            var distinctCategories = Source.ProductList
                .Select(p => p.Category)
                .Distinct();
            foreach (var category in distinctCategories)
            {
                Console.WriteLine(category);
            }
            //question13:
            int[] setA = { 1, 3, 5, 7, 9, 11, 13 };
            int[] setB = { 3, 6, 9, 12, 15, 13 };
            var result2 = setA.Except(setB);
            Console.WriteLine("Product IDs in setA but NOT in setB:");
            foreach (var id in result)
            {
                Console.WriteLine(id);
            }
            // question14:
            string[] list1 = { "Germany", "France", "UK", "Spain" };
            string[] list2 = { "france", "SPAIN", "Italy" };
            var result3 = list1.Except(list2, StringComparer.OrdinalIgnoreCase);
            foreach (var country in result3)
            {
                Console.WriteLine(country);
            }
            // question15:

            Dictionary<int, Product> productDict = Source.ProductList.ToDictionary(p => p.ProductID);


            if (productDict.TryGetValue(18, out Product product))
            {
                Console.WriteLine($"Found: ID = {product.ProductID}, Name = {product.ProductName}");
            }
            else
            {
                Console.WriteLine("Product with ID = 18 not found.");
            }
            // question16:
            var firstProductover500 = Source.ProductList.FirstOrDefault(p => p.UnitPrice > 50, new Product { UnitPrice = -1 });
            Console.WriteLine($"First product with price > $500: {firstProductover500}");
            //question17:
            var expensiveProduct = Source.ProductList.FirstOrDefault(p => p.UnitPrice > 500);

            if (expensiveProduct == null)
            {
                Console.WriteLine("Not Found Product price of it >500$");
            }
            else
            {
                Console.WriteLine(expensiveProduct.ProductName);
            }
            //question18:
            var Result5 =
           from N in Enumerable.Range(1, 10)
           select $"7 x {N} = {7 * N}";

            foreach (var Item in Result5)
            {
                Console.WriteLine(Item);
            }
            //question19:
            var Result9 =
                 from N in Enumerable.Range(1, 30)
                 where N % 2 == 0
                 select N;

            foreach (var Number in Result9)
            {
                Console.WriteLine(Number);
            }
            //question20: 
            var ProductNamesConcatWithcustomercompanynames = Source.ProductList.Select(P => P.ProductName).Take(3)
                .Concat(Source.CustomerList.Select(C => C.CustomerName).Take(3));
            //question21:
            var zipping = Source.ProductList.Select(P => P.ProductName).
                Zip(Source.CustomerList.Select(C => C.CustomerName), (ProductName, CustomerName) =>
            $"{ProductName} sold to {CustomerName}"
                );

        }
    }
}
