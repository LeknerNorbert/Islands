using BLL.Services.AuthService;
using BLL.Services.ClassifiedAdService;
using BLL.Services.EmailService;
using BLL.Services.IslandService;
using BLL.Services.PlayerInformationService;
using DAL.Models;
using DAL.Models.Context;
using DAL.Repositories.ClassifiedAdRepository;
using DAL.Repositories.PlayerInformationRepository;
using DAL.Repositories.UserRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using Web.Filters;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000/")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin();
        });
});

// Custom model validation filter
builder.Services.Configure<ApiBehaviorOptions>(options
    => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ValidateModelAttribute());
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddEndpointsApiExplorer();

// Dependency injections
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IClassifiedAdService, ClassifiedAdService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddSingleton<IGameConfigurationService, GameConfigurationService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IClassifiedAdRepository, ClassifiedAdRepository>();
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();

// Authentication
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
