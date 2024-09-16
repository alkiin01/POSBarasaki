using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using POSBarasaki.Models;
using System.Data.SqlClient;

namespace POSBarasaki
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostEnvironment HostingEnvironment { get; private set; }
        public static string DBConfigDS = string.Empty;
        public static string DBConfigDB = string.Empty;
        public static string DBConfigUser = string.Empty;
        public static string DBConfigPwd = string.Empty;
        public static string key = string.Empty;
        public static string resource = string.Empty;
        public static string _connectionString;
        public Startup(IHostEnvironment env)
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            DBConfigDS = Configuration["DBConfigDS"];
            DBConfigDB = Configuration["DBConfigDB"];
            DBConfigUser = Configuration["DBConfigUser"];
            DBConfigPwd = Configuration["DBConfigPwd"];
            services.AddMvc();
            //_connectionString = ConnectionStringBuildRes();
            services.AddDbContext<EpicPosContext>(opt =>
                opt.UseSqlServer(_connectionString));
            //services.Configure<PasswordHasherOptions>(options =>
            //{
            //    options.IterationCount = 10; // Set faktor cost (rounds) bcrypt
            //});
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
              .AddCookie(options =>
              {
                  options.LoginPath = "/Auth/Login";
                  options.AccessDeniedPath = "/Auth/AccessDenied";
              });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddControllersWithViews();
            services.AddAuthorization();

            services.AddRazorPages(); // Add Razor Pages service
                                      // Add any other services your application needs here
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseDeveloperExceptionPage();

            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Auth}/{action=Login}/{id?}");

                endpoints.MapRazorPages(); // Map Razor Pages

            });

            DBConfigDS = Configuration["DBConfigDS"];
            DBConfigDB = Configuration["DBConfigDB"];
            DBConfigUser = Configuration["DBConfigUser"];
            DBConfigPwd = Configuration["DBConfigPwd"];

            key = Configuration["Key"];
            resource = Configuration["resource"];
        }
        public static string ConnectionStringBuildRes()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = DBConfigDS;
            builder.InitialCatalog = DBConfigDB;
            builder.UserID = DBConfigUser;
            builder.Encrypt = false;
            builder.Password = DBConfigPwd;

            return builder.ConnectionString;
        }
    }
}
