using FlowerREST;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Vi bruger Singleton, så hele applikationen bruger samme RepositoryFlowers-objekt.
builder.Services.AddSingleton<RepositoryFlowers>();

// CORS: tillader frontend fra fx Live Server at kalde API'et.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// JWT authentication
// AddJwtBearer gør at ASP.NET kan læse og validere JWT-token fra Authorization-headeren.
// Token sendes typisk som: Authorization: Bearer <token>
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                // Tjekker hvem der har udstedt tokenet
                ValidateIssuer = true,

                // Tjekker hvem tokenet er lavet til
                ValidateAudience = true,

                // Tjekker at tokenet ikke er udløbet
                ValidateLifetime = true,

                // Tjekker at tokenet er signeret med den rigtige secret key
                ValidateIssuerSigningKey = true,

                ValidIssuer = "FlowerREST",
                ValidAudience = "FlowerRESTUsers",

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
"THIS_IS_A_LONG_SECRET_KEY_FOR_FLOWER_REST_JWT_123456789")
                    )
            };
    });

// Authorization bruges sammen med [Authorize]
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS skal stå før authentication/authorization.
app.UseCors("AllowAll");

// VIGTIGT:
// Authentication = hvem er du?
// Authorization = hvad må du?
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();