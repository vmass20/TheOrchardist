using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheOrchardist.Data;
using TheOrchardist.Services;

namespace TheOrchardist
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, ApplicationIdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

      services.AddDbContext<OrchardDBContext>(options1 =>
options1.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

      //services.Configure<AuthMessageSenderOptions>(Configuration);
      //services.Configure<StorageOptions>(Configuration.GetSection("AzureStorageConfig"));
      //services.Configure<StorageOptions>(Configuration.GetSection("AppSettings"));

      services.AddAuthentication().AddMicrosoftAccount(microsoftOptions =>
      {
        microsoftOptions.ClientId = Configuration["AppSettings:msAppID"];
        microsoftOptions.ClientSecret = Configuration["AppSettings:msAppPass"];
      });

      services.AddAuthentication().AddGoogle(googleOptions =>
      {
        googleOptions.ClientId = Configuration["AppSettings:client_id"];
        googleOptions.ClientSecret = Configuration["AppSettings:client_secret"];
        googleOptions.AccessType = "offline";
        googleOptions.SaveTokens = true;
      });



      services.AddMvc()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeFolder("/Account/Manage");
                    options.Conventions.AuthorizePage("/Account/Logout");
                    options.Conventions.AuthorizeFolder("/UserPlants");
                    options.Conventions.AuthorizeFolder("/GlobalPlants");
                });

            // Register no-op EmailSender used by account confirmation and password reset during development
            // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
            services.AddSingleton<IEmailSender, EmailSender>();
      //services.AddTransient<Pages.GlobalPlants.PaginatedList<GlobalPlantList>, List<GlobalPlantList>>();
      services.AddMemoryCache();//deafult in memory implement of Idistrubuted cache
      services.AddSession(options => { options.IdleTimeout = TimeSpan.FromMinutes(20); });
    }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

      app.UseMvc(routes =>
      {
        routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
      });
        }
    }
}
