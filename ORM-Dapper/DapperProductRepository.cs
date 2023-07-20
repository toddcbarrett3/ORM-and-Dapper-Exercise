using Dapper;
using IntroSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Dapper
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;

        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public void CreateProduct(string name, double price, int categoryID)
        {
            _connection.Execute("INSERT INTO PRODUCTS (Name, Price, categoryID) VALUES (@productName, @productPrice, @productCategoryID);",
             new { productName = name, productPrice = price, productCategoryID = categoryID });
        }
        public void UpdateProduct(int productID, string name, double price, int onSale, string stockLevel)
        {
            _connection.Execute("UPDATE PRODUCTS SET Name = @productName, Price = @productPrice, OnSale = @productOnSale, StockLevel = @productStockLevel WHERE ProductID = @existProductID;",
             new { existProductID = productID, productName = name, productPrice = price, productOnSale = onSale, productStockLevel = stockLevel });
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _connection.Query<Product>("SELECT * FROM Products;");
        }               
    }
}
