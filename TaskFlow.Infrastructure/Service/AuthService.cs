using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.DTO.Auth;
using TaskFlow.Application.IRepository;
using TaskFlow.Application.IService;
using TaskFlow.Domain.Models;
using TaskFlow.Infrastructure.Identity;

namespace TaskFlow.Infrastructure.Service
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<ApplicationUser> _signInManager; 
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IValidator<RegisterDTO> _validator;
        private readonly IValidator<LoginDTO> _Loginvalidator;
        private readonly IUnitOfWork _unit;
        public AuthService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,IValidator<RegisterDTO> validator,IValidator<LoginDTO> Loginvalidator, IUnitOfWork unit)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _validator = validator;
            _Loginvalidator = Loginvalidator;
            _unit = unit;
        }
      
        public async Task Register(RegisterDTO register)
        {
            //Sowmiya
            //Sowmi_13
            var validation = await _validator.ValidateAsync(register);
            if (!validation.IsValid)
            {
                throw new ValidationException(validation.Errors);
            }
            var appUser = new ApplicationUser
            {
                Email = register.email,
                UserName = register.email
            };
            var result = await _userManager.CreateAsync(appUser,register.password);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(",",result.Errors.Select(x => x.Description).ToList()));
            }
            var domainUser = new User
            {
                Id = Guid.Parse(appUser.Id),
                Email = register.email,
                FullName = register.fullName
            };
            var userResult = await _unit.User.AddAsync(domainUser);
            await _unit.SaveAsync();
        }

        public async Task<string> Login(LoginDTO login)
        {
            var validation = await _Loginvalidator.ValidateAsync(login);
            if (!validation.IsValid)
            {
                throw new ValidationException(validation.Errors);
            }
            var email = login.email;    
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid Credentials");
            }
            // IsPersistent = false
            //     (Session Cookie):The user is logged in for the current browser session only.Once they close all their browser tabs or restart their browser, they will be logged out and forced to log in again.
            //IsPersistent = true
            //     (Permanent / "Remember Me" Cookie):The login cookie is saved onto the user's hard drive with an expiration date (often 14 days by default). Even if they close the browser, restart their computer, or return days later, they will still be logged in when they open your app.
            // CheckPasswordSignInAsync - No ispersistant parameter 
            //PasswordSignInAsync - has ispersistant parameter but we dont use it because JWT is a stateless authentication
           
            var result = await _signInManager.CheckPasswordSignInAsync(user, login.password, true);
            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException("Account locked due to multiple failed login attempts. Try again later.");
            }
            if (result.IsLockedOut)
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }
            string token = GenerateToken(user);
            return token;
        }

        public static string GenerateToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {

            };
        }
    }

}
