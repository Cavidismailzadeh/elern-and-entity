using ClassPractic.Data;
using ClassPractic.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClassPractic.Services
{
    public class Course : ICourse
    {
        private readonly AppDbContext _context;

        public Course(AppDbContext context)
        {
            _context = context;

        }

        public async Task<List<Course>> GetAll()
        {
            return _context.Courses.ToListAsync();
        }
    }
}
