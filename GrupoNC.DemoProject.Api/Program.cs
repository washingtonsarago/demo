using GrupoNC.DemoProject.Api.Models;
using GrupoNC.DemoProject.Api.Repository;
using GrupoNC.DemoProject.Api.Repository.Interfaces;
using GrupoNC.DemoProject.Api.Services;
using GrupoNC.DemoProject.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
        .SetIsOriginAllowed(origin => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
    );
});

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GrupoNC.DemoProject", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            System.Array.Empty<string>()
        }
    });
});

builder.Services
    .Configure<JWTModel>(builder.Configuration.GetSection("Jwt"))
    .Configure<ConnectionStringsModel>(builder.Configuration.GetSection("ConnectionStrings"));

builder.Services
    .AddMvc()
    .AddControllersAsServices();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Repository
builder.Services.AddSingleton<IAddressesRepository, AddressesRepository>();
builder.Services.AddSingleton<IUsersRepository, UsersRepository>();

//Service
builder.Services.AddSingleton<IAddressesService, AddressesService>();
builder.Services.AddSingleton<IUsersService, UsersService>();

var app = builder.Build();

app.UseCors(options => options
    .SetIsOriginAllowed(origin => true)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
);
app.UseStaticFiles();
app.UseSwagger();

#if (DEBUG)
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "GrupoNC.DemoProject Microservice v1");
    c.RoutePrefix = string.Empty;
});
#endif

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

#if (DEBUG)
app.UsePathBase("/index.html");
#endif

app.UseHttpsRedirection();

app.Run();