using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySQLStoreAPI.Authentication;
using MySQLStoreAPI.Models;

namespace MySQLStoreAPI.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Read Product
        [HttpGet]
        public ActionResult<Products> GetAll()
        {

            // LINQ
            // แบบอ่านทั้งหมด
            // var allProducts = _context.Products.ToList();

            // LINQ with Condition
            // https://docs.microsoft.com/en-us/ef/core/querying/
            // var allProducts = _context.Products.Where(p => p.ProductID==1).ToList();
            // var allProducts = _context.Products
            //                 .Where(p => p.CategoryId != 0)
            //                 .OrderByDescending(p => p.UnitPrice)
            //                 .Take(2)
            //                 .ToList();

            // LINQ Raw SQL
            // https://docs.microsoft.com/en-us/ef/core/querying/raw-sql
            //  var allProducts = _context.Products
            //                 .FromSqlRaw("select * from Products order by ProductID desc")
            //                 .ToList();

            // LINQ with Join
            // https://docs.microsoft.com/en-us/ef/core/querying/complex-query-operators
            // แบบกำหนดเงื่อนไข
            // ดึงสินค้าเรียงจากราคาสูงสุด-ไปต่ำสุด มา 3 รายการแรก
            var allProducts = (
                from category in _context.Categories
                join product in _context.Products
                on category.CategoryId equals product.CategoryId
                where category.CategoryStatus == 1
                // orderby product.UnitPrice descending 
                orderby product.ProductID descending
                select new
                {
                    product.ProductID,
                    product.ProductName,
                    product.UnitPrice,
                    product.UnitInStock,
                    product.ProductPicture,
                    product.CreatedDate,
                    product.ModifiedDate,
                    category.CategoryName,
                    category.CategoryStatus
                }
            // ).Take(3).ToList();
            ).ToList();

            return Ok(allProducts);
        }


        // Get product by ID
        [HttpGet("{id}")]
        public ActionResult<Products> GetById(int id)
        {

            var Product = (
                from category in _context.Categories
                join product in _context.Products
                on category.CategoryId equals product.CategoryId
                where product.ProductID == id
                select new
                {
                    product.ProductID,
                    product.ProductName,
                    product.UnitPrice,
                    product.UnitInStock,
                    product.ProductPicture,
                    product.CreatedDate,
                    product.ModifiedDate,
                    category.CategoryName,
                    category.CategoryStatus
                }
            );

            if (Product == null)
            {
                return NotFound();
            }

            return Ok(Product);
        }

        // Create new Product
        [HttpPost]
        public ActionResult Create(Products products)
        {
            _context.Products.Add(products);
            _context.SaveChanges();
            return Ok(products.ProductID);
        }

        // Update Product
        [HttpPut]
        public ActionResult Update(Products products)
        {
            if (products == null)
            {
                return NotFound();
            }

            _context.Update(products);
            _context.SaveChanges();

            return Ok(products);
        }

        // Delete Product
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var productToDelete = _context.Products.Where(p => p.ProductID == id).FirstOrDefault();
            if (productToDelete == null)
            {
                return NotFound();
            }

            _context.Products.Remove(productToDelete);
            _context.SaveChanges();
            return NoContent();
        }

        


    }
}