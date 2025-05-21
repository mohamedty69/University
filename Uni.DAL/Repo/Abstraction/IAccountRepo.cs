using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Uni.DAL.Entity;

namespace Uni.DAL.Repo.Abstraction
{
    public interface IAccountRepo
    {
        Task<SignInResult> PasswordSignInAsync(Student user, string password);
        Task<SignInResult> PasswordSignInAsync(Student user, string password, bool isPersistent, bool lockoutOnFailure);

        Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync();
        Task<bool> CheckPasswordAsync(Student user, string password);
        Task<Student> FindByEmailAsync(string email);
        Task<IdentityResult> AccessFailedAsync(Student User);
        Task SignOutAsync();
        Task<IdentityResult> CreateUserAsync(Student user, string password);
        Task<Student> GetUserAsync(ClaimsPrincipal user);
		//List<Student> GetAll();
		//  Task<User> GetCurrentUser();
		Task<List<Student>> GetAll(ClaimsPrincipal user);
        Task<IdentityResult> UpdateUserAsync(Student User);
        Task<IdentityResult> UpdateUserAsyn(Student User);
		List<Course> GetCourses();
        List<Department> GetDepartment();
        List<Takes> GetTakes();
        List<Instructor> GetInstructors();
        List<Teaches> GetTeaches();
        List<Rcords> GetRecords();
        Task AddToRoleAsync(Student user, string role);





    }
}
