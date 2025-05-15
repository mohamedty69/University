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

        public List<Course> GetCourses(string department, string semester, string year)
        {
            return _context.Courses
                .Where(c => c.DeptName == department && c.Semester == semester && c.Year == year)
                .ToList();
        }

        public List<Course> GetCoursesByCodes(List<string> codes)
        {
            return _context.Courses
                .Where(c => codes.Contains(c.CourseCode))
                .ToList();
        }

        public void AddTake(Takes take)
        {
            _context.Takes.Add(take);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
