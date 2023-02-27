using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TeamManagement.Configurations;
using TeamManagement.Helper;
using TeamManagement.Models;
using TeamManagement.Repositories.AdminRepository;
using TeamManagement.Repositories.CourseReposiory;
using TeamManagement.Repositories.DepartmentRepository;
using TeamManagement.Repositories.LoginRepository;
using TeamManagement.Repositories.SemesterRepository;
using TeamManagement.Repositories.StudentRepository;
using TeamManagement.Repositories.SubjectRepository;
using TeamManagement.Repositories.TeacherRepository;
using TeamManagement.Repositories.TeamRepository;
using TeamManagement.Repositories.TopicRepository;
using static System.Net.WebRequestMethods;

namespace TeamManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            var firebaseApp = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("Configurations/firebase-adminsdk.json")
            });
            // Add CORS service
            services.AddCors(options =>
            {
                options.AddPolicy("apiCorsPolicy", builder =>
                {
                    builder.WithOrigins("http://localhost:5001", "https://cosmic-starship-a97dc2.netlify.app", 
                        "http://localhost:3000")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials(); 
                });
            });
            services.RegisterSwaggerModule();
            services.AddControllers().AddNewtonsoftJson();
            services.AddSingleton(firebaseApp);
          
            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<ISemesterRepository, SemesterRepository>();
            services.AddScoped<ITopicRepository, TopicRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JWT:Issuer"],
                    ValidAudience = Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"]))
                };
            });
            services.AddDbContext<FUProjectTeamManagementContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("MyDB"));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseApplicationSwagger();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("apiCorsPolicy");
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
