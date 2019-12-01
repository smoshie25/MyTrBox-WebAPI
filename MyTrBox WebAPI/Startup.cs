using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Primitives;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyTrBox_WebAPI.Filter;
using MyTrBox_WebAPI.Infrastructure;
using MyTrBox_WebAPI.Interfaces;
using MyTrBox_WebAPI.Model;
using MyTrBox_WebAPI.ModelViewHolder;
using MyTrBox_WebAPI.Services;
using OpenIddict.Validation;

namespace MyTrBox_WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMvc(
                options => {
                    options.Filters.Add<JsonExceptionFilter>();
                    options.Filters.Add<LinkRewritingFilter>();
                }
                );


            services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                options.UseOpenIddict<Guid>();
                //options.UseMySql(Configuration.GetConnectionString("DefaultConnection"));
                });

            // Add OpenIddict services
            services.AddOpenIddict()
                .AddCore(options =>
                {
                    options.UseEntityFrameworkCore()
                        .UseDbContext<AppDbContext>()
                        .ReplaceDefaultEntities<Guid>();
                })
                .AddServer(options =>
                {
                    options.UseMvc();

                    options.EnableTokenEndpoint("/token");

                    options.AllowPasswordFlow();
                    options.AcceptAnonymousClients();
                    options.DisableHttpsRequirement();
                })
                .AddValidation();

            // ASP.NET Core Identity should use the same claim names as OpenIddict
            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
            });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = OpenIddictValidationDefaults.AuthenticationScheme;
            });

            services.AddRouting(option => option.LowercaseUrls = true);

            services.Configure<PagingOptions>(Configuration.GetSection("DefaultPagingOptions"));

            services.AddScoped<IArtist, ArtistService>();
            services.AddScoped<IAccount, RegisterService>();
            services.AddScoped<ICategory, CategoryService>();
            services.AddScoped<ISong, SongService>();
            services.AddScoped<IGenre, GenreService>();

            services.AddAutoMapper(
                options => options.AddProfile<MapingProfile>(), typeof(Startup));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errorResponse = new ApiError(context.ModelState);
                    return new BadRequestObjectResult(errorResponse);
                };
            });

            AddIdentityServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            app.UseStaticFiles();// For the wwwroot folder

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                            Path.Combine(Directory.GetCurrentDirectory(), "Uplodaed_Documents")),
                RequestPath = "/Uplodaed_Documents"
            });
            //Enable directory browsing
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(
                            Path.Combine(Directory.GetCurrentDirectory(), "Uplodaed_Documents")),
                RequestPath = "/Uplodaed_Documents"
            });

            app.UseMvc();

        }

        private static void AddIdentityServices(IServiceCollection services)
        {
            var builder = services.AddIdentityCore<User>(x => {
                x.Password.RequiredLength = 6;
                x.Password.RequireUppercase = false;
                x.Password.RequireLowercase = false;
                x.Password.RequireNonAlphanumeric = true;
            });
            builder = new IdentityBuilder(
                builder.UserType,
                typeof(UserRole),
                builder.Services);

            builder.AddRoles<UserRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders()
                .AddSignInManager<SignInManager<User>>();
        }

    }
}
