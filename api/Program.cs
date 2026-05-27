using api.Filter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UserApp.Service.Commons;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppSetting>(
    builder.Configuration.GetSection("AppSetting")
);

var userAppConnectionString = builder.Configuration.GetConnectionString("UserApp");
var databaseProvider = builder.Configuration["Database:Provider"];
var autoMapperLicenseKey = builder.Configuration["AppSetting:AutoMapperLicence"];
UserApp.Service.Main.ConfigureService(builder.Services, userAppConnectionString, 1, autoMapperLicenseKey ?? "", databaseProvider);

string jwtSecret = builder.Configuration["AppSetting:JwtSecret"] ?? "";
builder.Services.AddAuthentication(cfg => {
    cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x => {
    x.RequireHttpsMetadata = false;
    x.SaveToken = false;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8
            .GetBytes(jwtSecret)
        ),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ExceptionFilter());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
