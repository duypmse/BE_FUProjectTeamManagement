using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using System.Threading.Tasks;
using TeamManagement.Helper;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Builder.Extensions;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth;
using TeamManagement.Repositories.AdminRepository;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using Firebase.Auth.Repository;
using Firebase.Auth;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using FirebaseAdmin.Messaging;
using TeamManagement.Repository.Repositories.Notification;

namespace TeamManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;

        public AdminController(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetListAdminAsync()
        {
            var listAdmin = await _adminRepository.GetListAdminAsync();
            if (!listAdmin.Any())
            {
                return NotFound();
            }
            return Ok(listAdmin);
        }
    }
}
