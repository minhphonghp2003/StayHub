using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Linq;
using Realtime.API;
using Realtime.Infrastructure.Hubs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Define the Bearer token security scheme
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    // Add a security requirement to apply the Bearer scheme globally
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(
    options =>
    {

        options.UseSecurityTokenValidators = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"], // The expected issuer value from configuration
            ValidateAudience = false, // Disable audience validation (can be enabled as needed)
            ValidateLifetime = true, // Ensure the token has not expiredAuthService
            ClockSkew = TimeSpan.Zero,
            ValidateIssuerSigningKey = true, // Ensure the token's signing key is valid
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        };

        options.Events = new JwtBearerEvents
        {

            OnMessageReceived = context =>
            {
                // 1. Get token from query
                var accessToken = context.Request.Query["access_token"].ToString();
                // 2. If exists → use it
                if (!string.IsNullOrEmpty(accessToken))
                {
                    var handler = new JwtSecurityTokenHandler();
                    var jwt = handler.ReadJwtToken(accessToken);

                    Console.WriteLine("========== JWT FULL INFO ==========");

                    // 🔐 RAW TOKEN
                    Console.WriteLine("RAW TOKEN:");
                    Console.WriteLine(jwt.RawData);

                    // 🧾 HEADER
                    Console.WriteLine("\n=== HEADER ===");
                    foreach (var header in jwt.Header)
                    {
                        Console.WriteLine($"{header.Key} = {header.Value}");
                    }

                    // 📦 PAYLOAD (Claims)
                    Console.WriteLine("\n=== PAYLOAD (CLAIMS) ===");
                    foreach (var claim in jwt.Claims)
                    {
                        Console.WriteLine($"{claim.Type} = {claim.Value}");
                    }

                    // ⏰ STANDARD FIELDS
                    Console.WriteLine("\n=== STANDARD FIELDS ===");
                    Console.WriteLine($"Issuer (iss): {jwt.Issuer}");
                    Console.WriteLine($"Audience (aud): {string.Join(",", jwt.Audiences)}");
                    Console.WriteLine($"Valid From (nbf): {jwt.ValidFrom}");
                    Console.WriteLine($"Valid To (exp): {jwt.ValidTo}");
                    Console.WriteLine($"Id (jti): {jwt.Id}");

                    // 🔑 SIGNATURE INFO
                    Console.WriteLine("\n=== SIGNATURE ===");
                    Console.WriteLine($"Algorithm: {jwt.Header.Alg}");
                    Console.WriteLine($"Has Signature: {jwt.RawSignature != null}");

                    Console.WriteLine("==================================");
                    context.Token = accessToken.ToString().Trim();

                }

                return Task.CompletedTask;
            },
        };
    }
);

builder.Services.AddAPIDI(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<FileNotificationHub>("/fileNotificationsHub");

app.Run();
