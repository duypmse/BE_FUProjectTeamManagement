using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Models;
using TeamManagement.Repositories.AdminRepository;
using TeamManagement.Repositories.TeacherRepository;
using TeamManagement.Repositories.StudentRepository;
using System.Data;

namespace TeamManagement.Repositories.LoginRepository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IConfiguration _config;
        private readonly IAdminRepository _admin;
        private readonly ITeacherRepository _teacher;
        private readonly IStudentRepository _student;
        public LoginRepository(IConfiguration config, IAdminRepository adminRepository, ITeacherRepository teacherRepository, IStudentRepository student)
        {
            _config = config;
            _admin = adminRepository;
            _teacher = teacherRepository;
            _student = student;
        }
        public async Task<object> LoginAsync(string email)
        {
            Admin admin = await _admin.getAdminByEmail(email);
            TeacherDTO teacher = await _teacher.GetTeacherByEmailAsync(email);
            StudentDTO student = await _student.StudentFindByEmailAsync(email);
            if (admin == null && teacher == null && student == null)
            {
                return null;
            }
            else
            {
                if (admin != null)
                {
                    string role = "admin";
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, admin.AdminEmail),
                        new Claim(ClaimTypes.Role,  role)
                    };
                    var jwtToken = new JwtSecurityToken(
                        issuer: _config["JWT:Issuer"],
                        audience: _config["JWT:Audience"],
                        claims: claims,
                        expires: DateTime.UtcNow.AddMinutes(30),
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"])), SecurityAlgorithms.HmacSha256)
                    );
                    return Task.FromResult(new { token = new JwtSecurityTokenHandler().WriteToken(jwtToken), admin, role, status = 200});
                }
                else if (teacher != null)
                {
                    string role = "teacher";
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, teacher.TeacherEmail),
                        new Claim(ClaimTypes.Role,  role)
                    };
                    var jwtToken = new JwtSecurityToken(
                        issuer: _config["JWT:Issuer"],
                        audience: _config["JWT:Audience"],
                        claims: claims,
                        expires: DateTime.UtcNow.AddMinutes(30),
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"])), SecurityAlgorithms.HmacSha256)
                    );
                    return Task.FromResult(new { token = new JwtSecurityTokenHandler().WriteToken(jwtToken), teacher, role, status = 200});
                }
                else if (student != null)
                {
                    string role = "student";
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, student.StuEmail),
                        new Claim(ClaimTypes.Role,  role)
                    };
                    var jwtToken = new JwtSecurityToken(
                        issuer: _config["JWT:Issuer"],
                        audience: _config["JWT:Audience"],
                        claims: claims,
                        expires: DateTime.UtcNow.AddMinutes(30),
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"])), SecurityAlgorithms.HmacSha256)
                    );
                    return Task.FromResult(new { token = new JwtSecurityTokenHandler().WriteToken(jwtToken), student, role, status = 200});
                }
            }
            return null;
        }
    }
}
