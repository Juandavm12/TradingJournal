using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using TradingJournal.API.Data;
using TradingJournal.API.Helpers;
using TradingJournal.Shared.Entities;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Trading Journal API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. <br /> <br />
 Enter 'Bearer' [space] and then your token in the text input below.<br /> <br />
 Example: 'Bearer 12345abcdef'<br /> <br />",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
 {
 {
 new OpenApiSecurityScheme
 {
 Reference = new OpenApiReference
 {
 Type = ReferenceType.SecurityScheme,
 Id = "Bearer"
 },
 Scheme = "oauth2",
 Name = "Bearer",
 In = ParameterLocation.Header,
 },
 new List<string>()
 }
 });
});


builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("name = Connectionwhitparams"));


builder.Services.AddIdentity<User, IdentityRole>(x =>
{
    x.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;
    x.SignIn.RequireConfirmedEmail = true;
    x.User.RequireUniqueEmail = true;
    x.Password.RequireDigit = true;
    x.Password.RequiredUniqueChars = 0;
    x.Password.RequireLowercase = true;
    x.Password.RequireNonAlphanumeric = false;
    x.Password.RequireUppercase = true;
    x.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    x.Lockout.MaxFailedAccessAttempts = 3;
    x.Lockout.AllowedForNewUsers = true;
})
.AddEntityFrameworkStores<DataContext>()
.AddDefaultTokenProviders();


builder.Services.AddScoped<IUserHelper, UserHelper>();

builder.Services.AddScoped<IMailHelper, MailHelper>();

builder.Services.AddScoped<IFileStorage, FileStorage>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(x => x.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwtKey"]!)),
    ClockSkew = TimeSpan.Zero
});



builder.Services.AddTransient<SeedDb>();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

SeedData(app);

static void SeedData(WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory!.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<SeedDb>();
        service!.SeedAsync().Wait();
    }
}

if (app.Environment.IsDevelopment())
{
}

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

    app.UseCors(x => x

     .AllowAnyMethod()
     .AllowAnyHeader()
     .SetIsOriginAllowed(origin => true)
     .AllowCredentials()


     );

    app.Run();
