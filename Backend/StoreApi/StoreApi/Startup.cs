using FluentValidation.AspNetCore;
using Lib.Service.Mongo;
using Lib.Service.Mongo.Context;
using Lib.Service.Mongo.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StoreApi.Core.Application.AttributeLogic;
using StoreApi.Core.Application.ProductLogic;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace StoreApi
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

        //services.AddCors(options =>
        //{
        //  options.AddPolicy("CorsPolicy",
        //    builder => builder.AllowAnyOrigin()
        //    .AllowAnyMethod()
        //    .AllowAnyHeader());
        //});


        services.AddControllers()
                .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductCreate>());
            services.AddMediatR(typeof(ProductCreate.ProductCreateCommand).Assembly)

                .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<AttributeCreate>());
      services.AddMediatR(typeof(AttributeCreate.AttributeCreateCommand).Assembly);

      //          .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<UserRepositorio>());
      //services.AddMediatR(typeof(UserRepositorio.UserCreateCommand).Assembly);


          services.Configure<MongoContext>(opt =>
                  {
                      opt.ConnectionString = Configuration.GetConnectionString("MongoDB");
                      //opt.ApplicationDbContext = Configuration.GetConnectionString("Database");
                      opt.Database = Configuration.GetConnectionString("Database");
                  });

            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));


      //services.AddSwaggerGen(c =>
      //      {
      //        c.OperationFilter<SecurityRequirementsOperationFilter>();

      //        c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
      //        {
      //          Description = "Autorizacion Standar, Usar Bearer. Ejemplo \"bearer {token}\"",
      //          In = ParameterLocation.Header,
      //          Name = "Authorization",
      //          Type = SecuritySchemeType.ApiKey,
      //          Scheme = "Bearer"
      //        });

      //      });

      //var secretKey = this.Configuration.GetValue<string>("Secret");
      //services.AddAuthentication(auth =>
      //{
      //  auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      //  auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      //}).AddJwtBearer(jwt =>
      //{
      //  jwt.RequireHttpsMetadata = false;
      //  jwt.SaveToken = true;
      //  jwt.TokenValidationParameters = new TokenValidationParameters()
      //  {
      //    ValidateIssuerSigningKey = true,
      //    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
      //    ValidateIssuer = false,
      //    ValidateAudience = false
      //  };
      //});

      //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      //                   .AddJwtBearer(options =>
      //                   {
      //                     options.TokenValidationParameters = new TokenValidationParameters
      //                     {
      //                       ValidateIssuerSigningKey = true,
      //                       IssuerSigningKey = new SymmetricSecurityKey(
      //                              System.Text.Encoding.ASCII.GetBytes(
      //                                  Configuration.GetSection("AppSettings:Token").Value)),
      //                       ValidateIssuer = false,
      //                       ValidateAudience = false
      //                     };
      //                   });

              services.AddCors();

              services.AddControllers();
              services.AddSwaggerGen(c =>
            {
              c.SwaggerDoc("v1", new OpenApiInfo { Title = "StoreApi", Version = "v1" });
            });
      
    }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StoreApi v1"));

           // app.UseCors("CorsPolicy");  
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(x => x.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
