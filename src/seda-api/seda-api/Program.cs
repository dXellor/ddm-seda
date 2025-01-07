using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Minio;
using seda_bll.Extensions;
using seda_dll.Data;
using seda_dll.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();
var config = builder.Configuration;

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = config["Cryptography:Tokens:JwtIssuer"],
        ValidAudience = config["Cryptography:Tokens:JwtAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Cryptography:Tokens:JwtSecretKey"]!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "DDM SEDA API", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
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

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddMinio(configureClient =>
{
    configureClient
        .WithEndpoint(config["MinIO:Host"])
        .WithCredentials(config["MinIO:AccessKey"], config["MinIO:SecretKey"])
        .WithSSL(false);
});

builder.Services.AddDbContext<DataContext>();
builder.Services.RegisterRepositories();
builder.Services.RegisterServices();
builder.Services.RegisterMapperProfiles();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_allowChosenOrigins",
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();  
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        var swaggerJsonBasePath = "/swagger/v1/swagger.json";
        var swaggerEndpoint = Path.Combine(".", swaggerJsonBasePath);
        c.SwaggerEndpoint(swaggerEndpoint, "DDM SEDA API v1");

        c.RoutePrefix = "";
        c.OAuthClientId("swagger-ui");
        c.OAuthUsePkce();
    });
}

app.UseCors("_allowChosenOrigins");

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseHttpsRedirection();
app.Run();