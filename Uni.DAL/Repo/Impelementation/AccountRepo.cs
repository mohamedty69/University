using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Uni.DAL.Entity;
using Uni.DAL.Repo.Abstraction;
using Uni.DAL.DB;
using System.Numerics;
using Microsoft.EntityFrameworkCore;

namespace Uni.DAL.Repo.Impelementation
{
    public class AccountRepo(UserManager<Student> userManager, SignInManager<Student> signInManager,AppDbContext entity) : IAccountRepo
    {
        // repo -> used usermanager and signmanager
        private readonly UserManager<Student> userManager = userManager;
        private readonly SignInManager<Student> signInManager = signInManager;		
		public async Task<Student> FindByEmailAsync(string email) => await userManager.FindByEmailAsync(email);
        public async Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync() => await signInManager.GetExternalAuthenticationSchemesAsync();
        public async Task<bool> CheckPasswordAsync(Student user, string password) => await userManager.CheckPasswordAsync(user, password);
        public async Task<SignInResult> PasswordSignInAsync(Student User, string password, bool isPersistent, bool lockoutOnFailure) => await signInManager.PasswordSignInAsync(User, password,  isPersistent,  lockoutOnFailure);
        public async Task SignOutAsync() => await signInManager.SignOutAsync();
        public async Task<IdentityResult> AccessFailedAsync(Student User) => await userManager.AccessFailedAsync(User);

        public async Task<SignInResult> PasswordSignInAsync(Student user, string password)
        {
            return await signInManager.PasswordSignInAsync(user, password, false, false);
        }
        public async Task<IdentityResult> CreateUserAsync(Student user, string password) => await userManager.CreateAsync(user, password);

        public async Task<Student> GetUserAsync(ClaimsPrincipal user) => await userManager.GetUserAsync(user);

        //2 update
        public async Task<IdentityResult> UpdateUserAsync(Student User)
        {
            var result = await userManager.UpdateAsync(User);
            if (result.Succeeded)
            {
                await signInManager.RefreshSignInAsync(User);
            }

            return result;
        }
		//List<Student> IAccountRepo.GetAll()
		//{
		//    return entity.Students.ToList();
		//}
		public async Task<List<Student>> GetAll(ClaimsPrincipal user)
		{
            return await userManager.Users.OfType<Student>().ToListAsync();
		}
		List<Course> IAccountRepo.GetCourses()
		{
			return entity.Courses.ToList();
		}
        List<Department> IAccountRepo.GetDepartment()
        {
			return entity.Departments.ToList();
		}
        List<Takes> IAccountRepo.GetTakes()
        { 
        return entity.Takes.ToList();
		}
        List<Instructor> IAccountRepo.GetInstructors()
		{
			return entity.Instructors.ToList();
		}
        List<Teaches> IAccountRepo.GetTeaches()
		{
			return entity.Teaches.ToList();
		}
        List<Rcords> IAccountRepo.GetRecords()
        {
            return entity.Records.ToList();
		}
        public async Task AddToRoleAsync(Student user, string role)
        {
            await userManager.AddToRoleAsync(user, role);
        }
		bool IAccountRepo.EditCourses(Course course)
		{
			try
			{
				// Find by CourseCode (assuming it's unique)
				var existingCourse = entity.Courses
					.FirstOrDefault(c => c.CourseCode == course.CourseCode);

				if (existingCourse == null) return false;

				// Update only allowed fields (security best practice)
				existingCourse.CourseName = course.CourseName;
				existingCourse.CreditHours = course.CreditHours;
				existingCourse.Semester = course.Semester;

				entity.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				// Log exception
				return false;
			}
		}


		public async Task<IdentityResult> UpdateUserAsyn(Student User) => await userManager.UpdateAsync(User);
    }
   
}
