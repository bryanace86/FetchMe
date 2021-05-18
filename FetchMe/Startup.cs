using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using FetchMe.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FetchMe.Services.AutoMapper;
using FetchMe.Services.PhotographerServices;
using Azure.Storage.Blobs;
using FetchMe.Services.BlobServices;
using FetchMe.Services.PhotographServices;
using FetchMe.Services.LocationServices;
using FetchMe.Models.AzureConfigurationModels;
using Vereyon.Web;
using FetchMe.Services.Policies;
using Microsoft.AspNetCore.Authorization;
using FetchMe.Services.LikeServices;
using FetchMe.Services.PhotographerLocationsServices;
using Microsoft.AspNetCore.Identity.UI.Services;
using FetchMe.Services.EmailServices;
using FetchMe.Models.EmailConfigurationModels;
using FetchMe.Services.MailChimpServices;
using AutoMapper;
using FetchMe.Services.GalleryServices;
using FetchMe.API;
using FetchMe.Models;
using FetchMe.Services.UserProfileImageService;
using FetchMe.Services.ApplicationUserServices;
using FetchMe.Services.SupportServices;
using FetchMe.Services.BlogServices;
using FetchMe.Services.PostServices;

namespace FetchMe
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

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddHttpContextAccessor();

            //services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            
            services.AddDefaultIdentity<ApplicationUser>(
                options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
             

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                )
                .AddRazorRuntimeCompilation();

            services.AddRazorPages();

            services.AddAuthentication()
                .AddFacebook(options =>
                {
                    options.AppId = Configuration["Authentication:Facebook:AppId"];
                    options.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                    options.AccessDeniedPath = "/AccessDeniedPathInfo";
                })
                .AddGoogle(options =>
                {
                    IConfigurationSection googleAuthNSection =
                        Configuration.GetSection("Authentication:Google");

                    options.ClientId = googleAuthNSection["AppId"];
                    options.ClientSecret = googleAuthNSection["AppSecret"];
                });

            //Email Sender
            services.AddTransient<IEmailSender, EmailService>();
            services.Configure<EmailConfiguration>(Configuration.GetSection("EmailConfiguration"));

            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    "HasPhotographesRemaining", 
                    policy => policy.Requirements.Add(
                        new HasPhotographsRemainingRequirement(1000)
                        ));
            });

            services.AddTransient<IAuthorizationHandler, HasPhotographsRemainingHandler>();

            services.AddAntiforgery(options =>
            {
                // Set Cookie properties using CookieBuilder properties†.
                options.FormFieldName = "AntiforgeryFieldname";
                options.HeaderName = "X-CSRF-TOKEN-HEADERNAME";
                options.SuppressXFrameOptionsHeader = false;
            });

            services.AddAutoMapper(configuration =>
            {
                configuration.AddProfile(new AutomapperProfile());
            });
            
            services.AddFlashMessage();
            
            //MailChimp
            services.AddSingleton<IMailChimpService, MailChimpService>();
            
            //Azure Blob Storage
            services.AddSingleton(x => new BlobServiceClient(Configuration.GetValue<string>("AzureBlobStorageConnectionString")));
            services.Configure<AzureStorageConfiguration>(Configuration);
            services.AddSingleton<IBlobService, BlobService>();

            //repos
            services.AddTransient<IBlogService, BlogService>();
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IPhotographerService, PhotographerService>();
            services.AddTransient<IPhotographService, PhotographService>();
            services.AddTransient<ILocationService, LocationService>();
            services.AddTransient<ILikeService, LikeService>();
            services.AddTransient<IPhotographerLocationService, PhotographerLocationService>();
            services.AddTransient<IGalleryService, GalleryService>();
            services.AddTransient<IUserProfileImageService, UserProfileImageService>();
            services.AddTransient<IApplicationUserService, ApplicationUserService>();
            services.AddTransient<ISupportService, SupportService>();
            
            //APIs
            services.AddTransient<GalleriesAPIController>();

            //email
            services.AddTransient<IEmailSender, EmailService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}
