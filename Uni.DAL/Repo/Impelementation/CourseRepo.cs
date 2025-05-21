using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uni.DAL.DB;
using Uni.DAL.Entity;
using Uni.DAL.Repo.Abstraction;

namespace Uni.DAL.Repo.Impelementation
{

    public class CourseRepo : ICourseRepo
    {
        private readonly AppDbContext _context;

        public CourseRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> GetAllDepartmentsAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<List<Course>> GetCoursesAsync(string department, string level, string semester)
        {
            return await _context.Courses
                .Where(c => c.Department.DeptName == department && c.Year == level && c.Semester == semester)
                .ToListAsync();
        }

        public async Task<Course?> GetCourseByCodeAsync(string code)
        {
            return await _context.Courses.FirstOrDefaultAsync(c => c.CourseCode == code);
        }

        public async Task<Student?> GetStudentByIdAsync(string id)
        {
            return await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddTakesAsync(Takes takes)
        {
            await _context.Takes.AddAsync(takes);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
		
	}
}
