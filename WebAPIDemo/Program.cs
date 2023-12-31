using Microsoft.EntityFrameworkCore;
using WebAPIDemo.IRepositories;
using WebAPIDemo.Models;
using WebAPIDemo.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ArtTattoo2023DbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IAccountMemberRepo, AccountMemberRepo>();
builder.Services.AddScoped<IArtTattooServiceRepo, ArtTattooServiceRepo>();
builder.Services.AddScoped<IArtTattooStyleRepo, ArtTattooStyleRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
