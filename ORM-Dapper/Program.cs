using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using MySqlX.XDevAPI.CRUD;

namespace ORM_Dapper
{
    public class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            
            var connString = config.GetConnectionString("DefaultConnection");
            
            IDbConnection conn = new MySqlConnection(connString);
            var repo = new DapperDepartmentRepository(conn);
            var repoProduct = new DapperProductRepository(conn);

        //CREATE NEW DEPARTMENT:
            Console.WriteLine("Type a new Department name");

            var newDepartment = Console.ReadLine();

            repo.InsertDepartment(newDepartment);

            var departments = repo.GetAllDepartments();

            foreach (var dept in departments)
            {
                Console.WriteLine(dept.Name);
            }

        //CREATE NEW PRODUCT:
            Console.WriteLine("Type a new Product name");
            var name = Console.ReadLine();

            Console.WriteLine("Enter the Price of the new Product");
            var price = double.Parse(Console.ReadLine());

            Console.WriteLine("Enter the Category ID of the new Product");
            var categoryID = int.Parse(Console.ReadLine());

            repoProduct.CreateProduct(name, price, categoryID);

            var products = repoProduct.GetAllProducts();

            foreach (var prod in products)
            {
                Console.WriteLine($"Product: {prod.Name}, Unit Price: ${prod.Price}, Category ID: {prod.CategoryID}");
            }

        //UPDATE AN EXISTING PRODUCT:
            Console.WriteLine("To update an existing Product, enter a valid Product ID");
            var productID = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the new name of the Product");
            name = Console.ReadLine();

            Console.WriteLine("Enter the new Price of the Product");
            price = double.Parse(Console.ReadLine());

            Console.WriteLine("Enter 1 if the product on On Sale or 0 if it is NOT On Sale");
            var onSale = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the new Stock Level of the Product");
            var stockLevel = Console.ReadLine();

            repoProduct.UpdateProduct(productID, name, price, onSale, stockLevel);

            products = repoProduct.GetAllProducts();

            foreach (var prod in products)
            {
                Console.WriteLine($"Product: {prod.Name}, Unit Price: ${prod.Price}, Category ID: {prod.CategoryID}");
            }
        }
    }
}
