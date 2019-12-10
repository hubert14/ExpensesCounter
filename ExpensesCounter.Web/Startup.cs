using System.Text;
using ExpensesCounter.Web.DI;
using ExpensesCounter.Web.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace ExpensesCounter.Web
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
            var authSection       = Configuration.GetSection("AuthOptions");
            var connectionSection = Configuration.GetSection("Connections");

            var authOptions       = authSection.Get<AuthOptions>();
            var connectionOptions = connectionSection.Get<DbConnectionOptions>();

            services.Configure<AuthOptions>(authSection);
            services.Configure<DbConnectionOptions>(connectionSection);

            services.AddServices();
            services.AddAutoMapper();
            services.AddContext(connectionOptions.DefaultConnection);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                     {
                         options.RequireHttpsMetadata = false;
                         options.TokenValidationParameters = new TokenValidationParameters
                         {
                             ValidateIssuer   = true,
                             ValidIssuer      = authOptions.Issuer,
                             ValidateAudience = true,
                             ValidAudience    = authOptions.Audience,
                             ValidateLifetime = true,
                             IssuerSigningKey =
                                 new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authOptions.SecurityKey)),
                             ValidateIssuerSigningKey = true
                         };
                     });

            services.AddRouting();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseAuthentication();

            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}