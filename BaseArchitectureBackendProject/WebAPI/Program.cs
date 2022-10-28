using Autofac;
using Autofac.Extensions.DependencyInjection;
using Bussiness.DependencyResolvers.AutoFac;
using DataAccess.Concrete.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

//DI mekanizmas�n�n AutoFac a ait oldu�unu s�yledik
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
//Business katman�nda DI i�in tan�mlad���m�z class burada ki DI i�lemini ger�ekle�tirecek
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutoFacBusinessModule()));

// Add services to the container.

builder.Services.AddControllers();

//Site bazl� cors kullan�m�
//builder.Services.AddCors(opt => 
//{
//    opt.AddPolicy("AllowOrigin",
//        builder => builder.WithOrigins("https://localhost:4200"));
//});

//B�t�n d�� istekleri kabul edecek �ekilde cors tan�mlad�m
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowOrigin",
        builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Tan�mlad���m�z cors politikas�n� uygulama i�erisinde kulland�m
app.UseCors("AllowOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
