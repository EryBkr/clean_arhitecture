using Autofac;
using Autofac.Extensions.DependencyInjection;
using Bussiness.DependencyResolvers.AutoFac;
using DataAccess.Concrete.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

//DI mekanizmasýnýn AutoFac a ait olduðunu söyledik
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
//Business katmanýnda DI için tanýmladýðýmýz class burada ki DI iþlemini gerçekleþtirecek
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutoFacBusinessModule()));

// Add services to the container.

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
