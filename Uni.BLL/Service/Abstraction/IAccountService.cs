using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uni.DAL.Entity;
using Uni.BLL.ModelVM;
using System.Security.Claims;
using Uni.BLL.ModelVM.Data;
using Uni.BLL.ModelVM.Admin;
using Uni.BLL.ModelVM.Account;
using Uni.BLL.ModelVM.GetDataVM;


namespace Uni.BLL.Service.Abstraction
{
    public interface IAccountService 
    {
        Task<SignInResult> Login(LoginVM loginVM);
        Task Logout();
        Task<bool> IsLockedOut(Student User);
        Task<LoginVM> GetLoginViewModelAsync();
        Task<IdentityResult> RegisterUserAsync(CreateStudentVM registerVM);
        Task<IdentityResult> RegisterUserAsync(RegistrationVM registerVM);
        Task<EditVM> GetUserForEdit(ClaimsPrincipal user);
        Task<IdentityResult> UpdateUser(ClaimsPrincipal user, EditVM model);
        Task<List<GetStudentDataVM>> GetAllStudent(ClaimsPrincipal user);
		List<CourseVM> GetAllCourses();
        List<DepartmentVM> GetAllDepartments();
        List<TakesVM> GetAllTakes();
        List<InstructorVM> GetAllInstructors();
        List<TeachesVM> GetAllTeaches();
        List<RcordsVM> GetAllRecords();


	}
}
