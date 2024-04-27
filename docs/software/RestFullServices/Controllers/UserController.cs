using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Validation;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public UserController(ApplicationDbContext db)
    {
        _db = db;
    }
    
    [HttpPost("registration")]
    public async Task<IActionResult> Registration([FromBody]UserDTO reUserDto)
    {
        var user = new Users(reUserDto.login, reUserDto.first_name, reUserDto.last_name, reUserDto.password, reUserDto.email);
        var testUserLogin = _db.Users.FirstOrDefault(u => u.login == user.login);
        var testUserEmail = _db.Users.FirstOrDefault(u => u.email == user.email);
        if (testUserLogin != null)
        {
            return BadRequest("This login is already taken");
        }
        if (testUserEmail != null)
        {
            return BadRequest("This email is already taken");
        }
        RegisterValidator validator = new RegisterValidator();
        ValidationResult result = await validator.ValidateAsync(user);
        if (!result.IsValid)
        {
            return BadRequest(result.Errors[0].ErrorMessage);
        }

        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();
        
        return Ok("user is register");
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]UserLogin userLogin)
    {
        var user = new Users(userLogin.login, userLogin.password);
        var existingUser = _db.Users.FirstOrDefault(u => u.login == user.login && u.password == user.password);
        LoginValidator validator = new LoginValidator();
        ValidationResult result = await validator.ValidateAsync(user);
        
        if (!result.IsValid)
        {
            return BadRequest(result.Errors[0].ErrorMessage);
        }
        
        if (existingUser != null && user.login == existingUser.login && user.password == existingUser.password)
        {
            return Ok("You have successfully logged into your account");
        }
        
        return BadRequest("You must first register");
        
    }
}