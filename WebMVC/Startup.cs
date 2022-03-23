using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DependencyResolves;
using FormHelper;
using FluentValidation.AspNetCore;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encyption;
using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace WebMVC
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
            services.AddControllersWithViews();
            //Bizim API mize izin verdiðim yerin dýþýnda istek gelmesi problemi için güvenlik
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.WithOrigins("http://localhost:3000"));//girdiðim siteden talet gelirse buna izin ver demek
            });
            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();//Bizim kendi TokenOptions kütüphanemiz olaný seç

            //token için 
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters//configuresyon bilgilerini verriyorum
                    {
                        ValidateIssuer = true,//Bu Adama tok verdiðimiz zaten appsetting.jsonda "www.alisari.com" veridðim bilgiyi veriyorum
                        ValidateAudience = true,
                        ValidateLifetime = true,//Verdiðim Yaþam süresini kortrol et
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,//Anahtarýda kotrol edeyim mi
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                    };
                });

            // asagidaki ikisini Session icin kullandim 
            services.AddDistributedMemoryCache();
            services.AddSession();

            //Kendi oluþturduðum services
            services.AddDependencyResolvers(new ICoreModule[]
            {
                new CoreModule(),//Benim baþka CoreModule olursa orada ekleyebilirim 
            });
        }

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
            //Kendi eklediðim app. larý tek seferde buradan sýnýfý ekleyerek oluþturuyorum
            app.ConfigureCustomExceptionMiddleware();

            //Cors(günvenlik) Ýçin Kullandým sýralamasý önemli
            app.UseCors(builder => builder.WithOrigins("http://localhost:3000").AllowAnyHeader());//Burdan gelen bütün isteklere izin ver

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //Sýralamalarý önemli giriþ yapmadan iþlem yapýlmaz gibi düþün
            //Eklenmesi gerekir
            app.UseAuthentication();//Giriþ izni gibi düþün giriþ yapabilir

            app.UseAuthorization();

            //Session icin kullandim hafizida deger tutmak
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(// Area paneli icin olusturdum ilk seferde Default kismi acilsin
                    name: "Admin_Default",
                    pattern: "{area:exists}/{controller=Default}/{action=Index}"
                );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
