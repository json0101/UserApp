using api.Filter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Threading.RateLimiting;
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
string jwtIssuer = builder.Configuration["AppSetting:JwtIssuer"] ?? "";
string jwtAudience = builder.Configuration["AppSetting:JwtAudience"] ?? "";
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
        ValidateIssuer = true,
        ValidIssuer = jwtIssuer,
        ValidateAudience = true,
        ValidAudience = jwtAudience,
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

// Rate limiting: frena fuerza bruta en el login (politica "login" colgada en AuthController).
builder.Services.AddRateLimiter(options =>
{
    // Por defecto .NET responde 503; lo forzamos a 429 Too Many Requests.
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    // Maximo 5 intentos por minuto, contados por IP del cliente.
    options.AddPolicy("login", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0
            }));
});

// Detras de Caddy: leer X-Forwarded-For para ver la IP real del cliente. SEGURO mientras
// la API no se exponga publicamente (#15): solo Caddy puede setear estos headers.
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

var app = builder.Build();

// Forwarded headers primero: el resto del pipeline ve la IP/proto reales.
app.UseForwardedHeaders();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
