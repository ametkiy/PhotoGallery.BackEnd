using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhotoGallery.Configurations;
using PhotoGallery.Data;
using PhotoGallery.Middleware;
using PhotoGallery.Model.Entities;
using PhotoGallery.PipeLineBehaviors;
using System.Reflection;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace PhotoGallery
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
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            services.AddControllers();

            services.AddDbContext<PhotoGalleryContext>(options =>
            {
            // Configure the context to use Microsoft SQL Server.
            options.UseSqlite(
                    Configuration.GetConnectionString("DefaultConnection"));

                // Register the entity sets needed by OpenIddict.
                // Note: use the generic overload if you need
                // to replace the default OpenIddict entities.
                options.UseOpenIddict();
            });

            // Register the Identity services.
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<PhotoGalleryContext>()
                .AddDefaultTokenProviders();

            // Configure Identity to use the same JWT claims as OpenIddict instead
            // of the legacy WS-Federation claims it uses by default (ClaimTypes),
            // which saves you from doing the mapping in your authorization controller.
            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserNameClaimType = Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = Claims.Role;
            });

            services.AddOpenIddict()
                // Register the OpenIddict core components.
                .AddCore(options =>
                {
                                // Configure OpenIddict to use the Entity Framework Core stores and models.
                                // Note: call ReplaceDefaultEntities() to replace the default OpenIddict entities.
                                options.UseEntityFrameworkCore()
                           .UseDbContext<PhotoGalleryContext>();
                })

                // Register the OpenIddict server components.
                .AddServer(options =>
                {
                                // Enable the token endpoint.
                                options.SetTokenEndpointUris("/connect/token");

                                // Enable the password flow.
                                options.AllowPasswordFlow();

                                // Accept anonymous clients (i.e clients that don't send a client_id).
                                options.AcceptAnonymousClients();

                                // Register the signing and encryption credentials.
                                options.AddDevelopmentEncryptionCertificate()
                           .AddDevelopmentSigningCertificate();

                                // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
                                options.UseAspNetCore()
                           .EnableTokenEndpointPassthrough();
                })

                // Register the OpenIddict validation components.
                .AddValidation(options =>
                {
                                // Import the configuration from the local OpenIddict server instance.
                                options.UseLocalServer();

                                // Register the ASP.NET Core host.
                                options.UseAspNetCore();
                }
            );
             

            services.AddScoped<IPhotoGalleryContext>(provider => provider.GetService<PhotoGalleryContext>());


            var builder = services.AddIdentityCore<ApplicationUser>();
            var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
            identityBuilder.AddEntityFrameworkStores<PhotoGalleryContext>();
            identityBuilder.AddSignInManager<SignInManager<ApplicationUser>>();


            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddControllers();

            
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssembly(typeof(Startup).Assembly);

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSwaggerGen();

            
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
                app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(options =>
            {
                options.MapControllers();
                options.MapDefaultControllerRoute();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Photo Gallery V1");
            });

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

        }
    }
}
