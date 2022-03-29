using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApi.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BookApi.Services.Authentication
{
    public class AuthenticationService : IAuthenticationServive
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthenticationService(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public bool Authentication(string email, string password)
        {

          try
          {
                // var user = await _userManager.FindByEmailAsync(email);

            // if (user != null)
            // {
            //     var result = await _signInManager.PasswordSignInAsync(email, password, false, true);

            //     return true;

            // }
            // return false;
            //Get the matched email user details
            var SingleUser = _userManager.Users.SingleOrDefault(x => x.Email == email);

            var verfiyPassword = _userManager.PasswordHasher.VerifyHashedPassword(SingleUser, SingleUser.PasswordHash, password);

            //If the verification failed.
            if (verfiyPassword != PasswordVerificationResult.Success)
            {
                return false;
            };

            //If the verification success.
            if (verfiyPassword == PasswordVerificationResult.Success)
            {
                return true;
            };

            return false;
          }
          catch
          {
              throw;
          }
        }

        public UserInterface GetUser(string email)
        {
            try
            {
                 var user = _userManager.Users.SingleOrDefault(x => x.Email == email);
                 
                 return new UserInterface { name = user.UserName, email = user.Email,};
            }
            catch 
            {
                
                throw;
            }
        }

        public async Task Logout()
        {
            try
            {
                 await _signInManager.SignOutAsync();
            }
            catch
            {
                throw;
            }
           
        }

        public async Task<bool> ResgisterUser(string name, string email, string password)
        {
            try
            {
                var appUser = new IdentityUser
            {
                UserName = name,
                Email = email,
            };

            var result = await _userManager.CreateAsync(appUser, password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(appUser, false);
            };

            return result.Succeeded;
            }
            catch
            {
                throw;
            }
        }
    }
}