﻿using bookshop.webapi.Contexts;
using bookshop.webapi.Models;
using bookshop.webapi.Models.CartFolder;
using bookshop.webapi.Models.OrderFolder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace bookshop.webapi.Controllers
{
    [ApiController]
    [Route("")]
    public class AuthController
        (UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, AppDbContext db) : ControllerBase
    {
        public record RegisterRequest(string Username, string Email, string Password);
        public record LoginRequest(string Email, string Password);

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterRequest request)
        {
            AppUser user = await userManager.FindByEmailAsync(request.Email);

            if(user is not null)
            {
                return BadRequest("User exists.");
            }

            AppUser newUser = new AppUser()
            {
                UserName = request.Username,
                Email = request.Email,
                EmailConfirmed = true,
                Cart = new Cart
                {
                    Items = new List<CartItem>(),
                },
                Comments = new List<Comment>(),
                Orders = new List<Order>()
            };

            IdentityResult result = await userManager.CreateAsync(newUser, request.Password);
            await db.SaveChangesAsync();
            if (result.Succeeded)
            {
                return Ok("User created.");
            }
            return BadRequest("User couldn't created.");
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest request)
        {
            AppUser user = await userManager.FindByEmailAsync(request.Email)!;

            if (user is null)
                return NotFound();

            var result = await signInManager.PasswordSignInAsync(user, request.Password, true, false);

            if (result.Succeeded)
                return Ok(new { Email = user.Email, Username = user.UserName});
            else
                return BadRequest("Sign in unsuccessfull.");
        }

        [HttpGet]
        [Route("signout")]
        public async Task<ActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return Ok("Signout successfull.");
        }
    }
}
