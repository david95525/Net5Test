using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using net5test.Repositories;
using net5test.Repositories.DataModels;
using net5test.Repositories.Interface;
using net5test.Services.MemberService;
using net5test.Services.AuthorizeService;
using net5test.Common.Cryptography;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using net5test.Services.BloodPressureService;
using JavaScriptEngineSwitcher.ChakraCore;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;

namespace net5test
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
           
            //�[�Jcookie
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
           {
               option.LogoutPath = new PathString("/Home/Logout");//�n�XAction
                //�Τ᭶�����d�Ӥ[�A�n�J�O���A��Controller��Action�̥Τ�n�J�ɡA�]�i�H�]�w��
                option.SlidingExpiration = false;
           });
            services.AddControllersWithViews();

            services.AddScoped<DbContext, microlife_devContext>();
            services.AddDbContext<microlife_devContext>(
             option => option.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));
            services.AddAutoMapper(typeof(Startup));
            //Repository

            services.AddScoped<IGenericRepository<Member>, GenericRepository<Member>>();
            services.AddScoped<IGenericRepository<LogBloodpressure>, GenericRepository<LogBloodpressure>>();
            services.AddScoped<IGenericRepository<SysCountry>, GenericRepository<SysCountry>>();
            //Oauth2
            services.AddScoped<IGenericRepository<OauthClientsClass>, GenericRepository<OauthClientsClass>>();
            services.AddScoped<IGenericRepository<OauthClientsKind>, GenericRepository<OauthClientsKind>>();
            services.AddScoped<IGenericRepository<OauthRefreshToken>, GenericRepository<OauthRefreshToken>>();
            services.AddScoped<IGenericRepository<OauthAuthorizationCode>, GenericRepository<OauthAuthorizationCode>>();
            services.AddScoped<IGenericRepository<OauthAccessToken>, GenericRepository<OauthAccessToken>>();
            //Services
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IBloodPressureService, BloodPressureService>();
            services.AddScoped<IAuthorizeService, AuthorizeService>();
            //�[�K
            services.AddScoped<ICryptography, Cryptography>();
            ////log
            services.AddScoped<IGenericRepository<ApiActionLog>, GenericRepository<ApiActionLog>>();

      
            //session
            services.AddSession();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //JsEngineSwitcher
            services.AddJsEngineSwitcher(options => options.DefaultEngineName = ChakraCoreJsEngine.EngineName)
              .AddChakraCore();

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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            //�ϥ�session
            app.UseSession();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseCors("CorsPolicy");
            // ����
            app.UseAuthentication();
            // ���v
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=OAuth2}/{action=RequestAuthorization}/{id?}");
            });
        }

    }
}
