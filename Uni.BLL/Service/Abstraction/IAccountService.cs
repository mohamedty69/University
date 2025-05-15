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

namespace Uni.BLL.Service.Abstraction
{
    public interface IAccountService 
    {
        Task<SignInResult> Login(LoginVM loginVM);
        Task Logout();
        Task<bool> IsLockedOut(Student User);
        Task<LoginVM> GetLoginViewModelAsync();
        Task<IdentityResult> RegisterUserAsync(CreateStudentVM registerVM);
        Task<EditVM> GetUserForEdit(ClaimsPrincipal user);
        Task<IdentityResult> UpdateUser(ClaimsPrincipal user, EditVM model);
        Task<GetStudentDataVM> GetAllStudent(ClaimsPrincipal user);


    }
}
