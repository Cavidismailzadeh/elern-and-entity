using ClassPractic.Data;
using ClassPractic.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace ClassPractic.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class CourseController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CourseController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async  Task<IActionResult> Index()
        {

            IEnumerable<Course> courses = await _context.Courses.Include(m=>m.CourseImages).Include(m => m.CourseAuthor).Where(m => !m.SoftDelete).ToListAsync();
            
            return View(courses);
        }

        public async Task<IActionResult> Detail(int ? id)
        {
            if (id == null) return BadRequest();

            Course? course= await _context.Courses.Include(m => m.CourseImages).Include(m => m.CourseAuthor).Where(m=>m.Id == id).FirstOrDefaultAsync();

            if (course is null) return NotFound();
            
            return View(course);
        }




        [HttpGet]
        public async Task<IActionResult> Create()
        {
            IEnumerable<Course> categories = await _categoryService.GetAll();

            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                string fileName = Guid.NewGuid().ToString() + " " + course.Name;

                string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);

             
                course.Name = fileName;

                await _context.Courses.AddAsync(course);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));


            }
            catch (Exception)
            {

                throw;
            }


            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
