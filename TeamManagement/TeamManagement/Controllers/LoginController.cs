using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System;
using TeamManagement.Models;
using TeamManagement.Repositories.AdminRepository;
using TeamManagement.Repositories.TeacherRepository;
using TeamManagement.DTO;
using TeamManagement.Repositories.LoginRepository;

namespace TeamManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository _loginRepository;

        public LoginController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        [HttpPost]
        public async Task<IActionResult> LoginWithGoogle([FromBody] EmailDTO email)
        {
            var loginUser = await _loginRepository.LoginAsync(email.Email);
            if (loginUser == null)
            {
                return NotFound();
            }
            return  Ok(loginUser);
        }
    }
}
