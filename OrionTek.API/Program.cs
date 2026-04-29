using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OrionTek.API.Application.Common.Interfaces;
using OrionTek.API.Infrastructure.Persistence;
using OrionTek.API.Mappings;
using OrionTek.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=oriontek.db"));

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseAuthorization();
app.MapControllers();

app.Run();