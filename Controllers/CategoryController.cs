using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySQLStoreAPI.Models;

namespace MySQLStoreAPI.Controllers
{
    [Authorize]  // ใส่ Authorize ด้วยคำสั่งเดียว จบ
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Read Categories
        // [Authorize]
        [HttpGet]
        // [HttpGet("get-all-category")]
        public ActionResult<Category> GetAll()
        {

            var allCategory = _context.Categories.ToList();
            return Ok(allCategory);
        }

        // Get Category by ID
        [HttpGet("{id}")]
        public ActionResult<Category> GetById(int id)
        {

            var Category = _context.Categories.Where(c => c.CategoryId == id);

            if (Category == null)
            {
                return NotFound();
            }

            return Ok(Category);
        }

        // Create new Category
        [HttpPost]
        public ActionResult Create(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return Ok(category.CategoryId);
        }

        // Update Category
        [HttpPut]
        public ActionResult Update(Category category)
        {
            if (category == null)
            {
                return NotFound();
            }

            _context.Update(category);
            _context.SaveChanges();

            return Ok(category);
        }

        // Delete Category
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var categoryToDelete = _context.Categories.Where(c => c.CategoryId == id).FirstOrDefault();
            if (categoryToDelete == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(categoryToDelete);
            _context.SaveChanges();
            return NoContent();
        }

    }
}