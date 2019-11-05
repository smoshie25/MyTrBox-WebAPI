using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MyTrBox_WebAPI.Interfaces;
using MyTrBox_WebAPI.Model;
using MyTrBox_WebAPI.ModelViewHolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTrBox_WebAPI.Services
{
    public class RegisterService : IAccount
    {

        private readonly UserManager<User> userManager;
        public RegisterService(UserManager<User> _userManager)
        {
            userManager = _userManager;
        }
        public async Task<(bool succeeded, string errorMessage)> CreatUserAsync(AccountViewHolder.UserViewModel model)
        {
            var entity = new User
            {
                Email = model.Email,
                UserName = model.Username,
                Country = model.Country,
                CreatedAt = DateTimeOffset.UtcNow
            };

            var result = await userManager.CreateAsync(entity, model.Password);

            if (!result.Succeeded) {
                var error = result.Errors.FirstOrDefault()?.Description;
                return (false, error);
            }

            return (true, null);
        }
    }
}
