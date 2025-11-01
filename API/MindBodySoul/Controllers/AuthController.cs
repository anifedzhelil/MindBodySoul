using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MindBodySoul.Models.DTO;
using MindBodySoul.Repositories.Interface;

namespace MindBodySoul.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        //Post: {apibaseurl/api/auth/login}
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var identityUser = await this.userManager.FindByEmailAsync(request.Email);

            if (identityUser is not null)
            {
                var checkPasswordResult = await this.userManager.CheckPasswordAsync(identityUser, request.Password);

                if (checkPasswordResult)
                {
                    var roles = await userManager.GetRolesAsync(identityUser);

                    //create a Token and response
                    var jwtToken = tokenRepository.CreateJwtToken(identityUser, roles.ToList());

                    var response = new LoginResponseDto()
                    {
                        UserId = identityUser.Id,
                        UserName = identityUser.UserName ?? "",
                        Roles = roles.ToList(),
                        Token = jwtToken
                    };

                    return Ok(response);
                }
            }
           
            ModelState.AddModelError("", "Неправилен имейл или парола");
            
            return ValidationProblem(ModelState);
        }

        //Post: {apibaseurl/api/auth/register}
        [HttpPost]
        [Route("register")] 
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var existingUser = await userManager.FindByNameAsync(request.UserName);
            if (existingUser != null)
            {
                ModelState.AddModelError("UserName", "Потребителското име е заето.");
                return ValidationProblem(ModelState);
            }

            var existingUserByEmail = await userManager.FindByEmailAsync(request.Email.Trim());
            if (existingUserByEmail != null)
            {
                ModelState.AddModelError("Email", "Съществува потребител с този имейл.");
                return ValidationProblem(ModelState);
            }

            var user = new IdentityUser
            {
                UserName = request.UserName.Trim(),
                Email = request.Email.Trim(),
            };

            var identityResult = await userManager.CreateAsync(user, request.Password);

            if (identityResult.Succeeded)
            {
                //Add Role the user ()
                await userManager.AddToRoleAsync(user, "Reader");

                if (identityResult.Succeeded)
                {
                    return Ok();
                }

                if (identityResult.Errors.Any())
                {
                    foreach (var error in identityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            else
            {
                if (identityResult.Errors.Any())
                {
                    foreach (var error in identityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return ValidationProblem(ModelState);
        }

        // POST: /api/auth/setup-admin
        [HttpPost]
        [Route("setup-admin")]
        public async Task<IActionResult> SetupAdmin([FromBody] SetupAdminRequestDto request)
        {
            // 1. Проверка на Setup Key (за security)
            var setupKey = HttpContext.Request.Headers["X-Setup-Key"].FirstOrDefault();
            var validSetupKey = "MBS_SETUP_2025"; // Временен key, после ще го сложим в environment variable

            if (setupKey != validSetupKey)
            {
                return Unauthorized(new { message = "Невалиден setup key" });
            }

            // 2. Проверка дали user съществува
            var existingUser = await userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return BadRequest(new { message = "Потребителят съществува!" });
            }

            // 3. Създаване на admin user
            var adminUser = new IdentityUser
            {
                UserName = request.UserName,
                Email = request.Email
            };

            var result = await userManager.CreateAsync(adminUser, request.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new { errors = result.Errors });
            }

            // 4. Добавяне на Reader и Writer roles
            await userManager.AddToRoleAsync(adminUser, "Reader");
            await userManager.AddToRoleAsync(adminUser, "Writer");

            return Ok(new { message = "Admin потребителят е добавен успешно.", userId = adminUser.Id });
        }
    }


}
