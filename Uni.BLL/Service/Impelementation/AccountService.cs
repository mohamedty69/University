using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uni.BLL.Service.Abstraction;
using Uni.DAL.Entity;
using Uni.DAL.Repo.Abstraction;
using Uni.BLL.ModelVM;
using Microsoft.AspNetCore.Identity;
using HospitalSystem.BLL.Helper;
using AutoMapper;
using System.Security.Claims;

namespace Uni.BLL.Service.Impelementation
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepo UserRepo;
        private readonly IMapper mapper;

        public AccountService(IAccountRepo UserRepo, IMapper mapper)
        {
            this.UserRepo = UserRepo;
            this.mapper = mapper;
        }
        public async Task<LoginVM> GetLoginViewModelAsync()
        {
            var schemes = await UserRepo.GetExternalAuthenticationSchemesAsync();
            return new LoginVM
            {
                Schemes = schemes
            };
        }
        public async Task<IdentityResult> UpdateUser(Student User)
        {
            return await UserRepo.UpdateUserAsync(User);
        }

        public async Task<EditVM> GetUserForEdit(ClaimsPrincipal user)
        {
            var User = await UserRepo.GetUserAsync(user);
            if (User == null)
            {
                return null;
            }
            return mapper.Map<EditVM>(User);
        }

        public Task<bool> IsLockedOut(Student User)
        {
            throw new NotImplementedException();
        }

        public async Task<Microsoft.AspNetCore.Identity.SignInResult> Login(LoginVM loginVM)
        {
            var User = await UserRepo.FindByEmailAsync(loginVM.Email);
            if (User == null)
            {
                return Microsoft.AspNetCore.Identity.SignInResult.Failed;
            }

            var passwordCheck = await UserRepo.CheckPasswordAsync(User, loginVM.Password);
            if (!passwordCheck)
            {
                await UserRepo.AccessFailedAsync(User);
                return Microsoft.AspNetCore.Identity.SignInResult.Failed;
            }

            var result = await UserRepo.PasswordSignInAsync(User, loginVM.Password, false, false);
            return result;
        }
        public async Task Logout()
        {
            await UserRepo.SignOutAsync();
        }
        public async Task<IdentityResult> RegisterUserAsync(CreateStudentVM registerVM)
        {
            // Check if user exists
            var existingUser = await UserRepo.FindByEmailAsync(registerVM.Email);
            if (existingUser != null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = "Email address is already in use."
                });
            }
            // Use AutoMapper to map RegistrationVM to User
            var newUser = mapper.Map<Student>(registerVM);
            newUser.LockoutEnabled = true;

            var userCreated = await UserRepo.CreateUserAsync(newUser, registerVM.Password);

            if (userCreated.Succeeded)
            {
                await UserRepo.PasswordSignInAsync(newUser, registerVM.Password);
            }

            return userCreated;
        }

        public Task<IdentityResult> UpdateUser(ClaimsPrincipal user, EditVM model)
        {
            throw new NotImplementedException();
        }
    }
    
}
