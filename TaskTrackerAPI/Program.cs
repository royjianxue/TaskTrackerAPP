using Microsoft.EntityFrameworkCore;
using TaskTrackerData.DbConexts;
using TaskTrackerData.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TaskContext>(
            dbContextOptions => dbContextOptions.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"])
                    .LogTo(Console.WriteLine,
                    new[] { DbLoggerCategory.Database.Command.Name },
                    LogLevel.Information)
                    .EnableSensitiveDataLogging());


//AutoMapper.Extensions.Microsoft.DependencyInjection
// AppDomain.CurrentDoman.GetAssemblies will scan the TaskTrackerAPI assembly

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ISignUpRepository, SignUpRepository>();


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
