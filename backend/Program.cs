using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Ristorante.Data;
using Ristorante.Services;
using Ristorante.Services.Interfaces;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

//  JWT dal appsettings.json
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!); // ! = non null


//Connessione al DbContext
builder.Services.AddDbContext<RistoranteDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Add services to the container.

//Aggiunge controller e Swagger
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev",
        builder => builder
            .WithOrigins("http://localhost:4200") // frontend Angular
            .AllowAnyHeader() // permette tutti gli header (Content-Type, Authorization, ecc)
            .AllowAnyMethod() // permette GET, POST, PUT, DELETE...
    );
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"]!,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});
builder.Services.AddAuthorization();

builder.Services.AddScoped<Ristorante.Services.Interfaces.IUserService, 
    Ristorante.Services.UserService>();
builder.Services.AddScoped<Ristorante.Services.Interfaces.IAuthService,
    Ristorante.Services.AuthService>();
builder.Services.AddScoped<Ristorante.Services.Interfaces.IRestaurantService, 
    Ristorante.Services.RestaurantService>();
builder.Services.AddScoped<Ristorante.Services.Interfaces.IReservationService, 
    Ristorante.Services.ReservationService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();



app.UseCors("AllowAngularDev");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
