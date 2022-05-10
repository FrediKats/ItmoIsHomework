using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ReviewYourself.WebApi.Services;
using ReviewYourself.WebApi.Services.Implementations;
using ReviewYourself.WebApi.Tools;

namespace ReviewYourself.WebApi
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
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddScoped<IPeerReviewUserService, PeerReviewUserService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<ICourseTaskService, CourseTaskService>();
            services.AddScoped<ISolutionService, SolutionService>();
            services.AddScoped<IReviewService, ReviewService>();


            services.AddDbContext<PeerReviewContext>(options =>
                options.UseSqlServer(Configuration["ConnectionString:LocalDb"]));

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,

                        ValidateAudience = false,
                        ValidateIssuer = false
                    };
                });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}