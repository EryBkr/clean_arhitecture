using Autofac;
using Autofac.Extensions.DependencyInjection;
using Bussiness.DependencyResolvers.AutoFac;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//DI mekanizmasýnýn AutoFac a ait olduðunu söyledik
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
//Business katmanýnda DI için tanýmladýðýmýz class burada ki DI iþlemini gerçekleþtirecek
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutoFacBusinessModule()));

// Add services to the container.

builder.Services.AddControllers();

//Site bazlý cors kullanýmý
//builder.Services.AddCors(opt => 
//{
//    opt.AddPolicy("AllowOrigin",
//        builder => builder.WithOrigins("https://localhost:4200"));
//});

//Bütün dýþ istekleri kabul edecek þekilde cors tanýmladým
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowOrigin",
        builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,

        //Kimin daðýttýðý belirtilsin mi?
        ValidateIssuer = true,

        //Belli bir süre sonra token bitsin mi?
        ValidateLifetime = true,

        //Token key doðrulamasý yapýlsýn mý?
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Token:Issuer"],
        ValidAudience = builder.Configuration["Token:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddDependencyResolvers(new Core.Utilities.IoC.ICoreModule[] { new CoreModule() });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Tanýmladýðýmýz cors politikasýný uygulama içerisinde kullandým
app.UseCors("AllowOrigin");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
