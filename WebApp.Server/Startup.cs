using Blazored.LocalStorage;
using Blazored.Modal;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using WebAppShared.DAL;
using WebAppShared.DAL.Interface;
using WebAppShared.DAL.Repository;
using WebAppShared.Services;

namespace WebApp.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add MyDbContext to Dependency Injection
            services.AddDbContext<AppDataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")),
                ServiceLifetime.Transient); //// this will reset your model to its original value if you decided to cancel the operations.

            /// Dont move this somewhere otherwise you will get this error (Error: System.InvalidCastException: Unable to cast object of type 'Microsoft.AspNetCore.Components.Server.ServerAuthenticationStateProvider')
            services.AddRazorPages();
            services.AddServerSideBlazor();
            ////// Commented this out when doing Initial Migration
            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IDeletedUserRepository, DeletedUserRepository>();
            services.AddTransient<IUserModelRepository, UserModelRepository>();
            services.AddTransient<ISourcePartnerRepository, SourcePartnerRepository>();
            services.AddTransient<IPersonalInfoRepository, PersonalInfoRepository>();
            services.AddTransient<IBankingInfoRepository, BankingInfoRepository>();
            services.AddTransient<INomineeInfoRepository, NomineeInfoRepository>();
            services.AddTransient<IInvestmentModelRepository, InvestmentModelRepository>();
            services.AddTransient<IInvestmentPayoutRepository, InvestmentPayoutRepository>();
            services.AddTransient<INotificationModelRepository, NotificationModelRepository>();


            //// Adding the Unit of work to the DI container
            //services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddHttpClient<IUserService, UserService>();
            services.AddScoped<IEmailSender, EmailSender>();

            //services.AddSingleton<Microsoft.Extensions.Logging.ILogger>(provider =>
            //        provider.GetRequiredService<Microsoft.Extensions.Logging.ILogger>());

            services.AddBlazoredLocalStorage();
            services.AddBlazoredModal();
            services.AddSingleton<HttpClient>();

            services.AddCors();

            // also use for RoxyFileman to be able to create folder
            services.AddSession(options =>
            {
                options.Cookie.IsEssential = true; // make the session cookie Essential
                options.IdleTimeout = TimeSpan.FromMinutes(1); //You can set Time   
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            ////app.UseCors(_corsPolicyName);
            //app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins(new[] { Configuration["Cors:origin1"], Configuration["Cors:origin2"], Configuration["Cors:origin3"] }));

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
