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

namespace Uni.DAL.Repo.Impelementation
{
    public class AccountRepo(UserManager<Student> userManager, SignInManager<Student> signInManager) : IAccountRepo
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

        public Task<SignInResult> PasswordSignInAsync(Student user, string password)
        {
            throw new NotImplementedException();
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
        List<Student> IAccountRepo.GetAll()
        {
            return userManager.Users.ToList();
        }   

        public async Task<IdentityResult> UpdateUserAsyn(Student User) => await userManager.UpdateAsync(User);
    }
}
