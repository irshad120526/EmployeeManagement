using Employee.Model;
using EmployeeAPI.DatabaseContext;
using EmployeeAPI.EmployeeRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IAdditionalInfoRepository, AdditionalInfoRepository>();
builder.Services.AddTransient<IMailService, MailService>();

builder.Services.AddDbContext<APIDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.Configure<DataConfig>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
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