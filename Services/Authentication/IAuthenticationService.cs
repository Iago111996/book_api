using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApi.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BookApi.Services.Authentication
{
    public interface IAuthenticationServive
    {
        bool Authentication(string email, string password);
        Task<bool> ResgisterUser(string name, string email, string password);
        UserInterface GetUser(string email);
        Task Logout();
    }
}