using dotnetapp.Models; using dotnetapp.Services; using Microsoft.AspNetCore.Mvc;
namespace dotnetapp.Controllers;
[ApiController] public class AuthenticationController(IAuthService auth):ControllerBase {
 [HttpPost("api/login")] public async Task<IActionResult> Login(LoginModel model){if(!ModelState.IsValid)return ValidationProblem(ModelState);var (s,b)=await auth.Login(model);return StatusCode(s,b);}
 [HttpPost("api/register")] public async Task<IActionResult> Register(User model){if(!ModelState.IsValid)return ValidationProblem(ModelState);var(s,m)=await auth.Registration(model,model.UserRole);return StatusCode(s,new{message=m});}
}
