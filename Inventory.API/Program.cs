using Inventory.Infrastructure.DependencyInjection;
using Inventory.Application.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Inventory.Infrastructure;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        var config = builder.Configuration;
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuerSigningKey = true,
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"])),
//            ValidateIssuer = true,
//            ValidIssuer = config["Jwt:Issuer"],
//            ValidateAudience = true,
//            ValidAudience = config["Jwt:Audience"],
//            ClockSkew = TimeSpan.Zero
//        };
//    });




Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


//agregar beartoken authentication
//agregar nidelware de autenticación JWT
// Configura DbContext con SQL Server y la cadena de conexión
builder.Services.AddDbContext<InventoryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    .EnableSensitiveDataLogging()
    .LogTo(Console.WriteLine, LogLevel.Information)
    );

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddControllers();

var app = builder.Build();
app.UseCors("AllowAngularApp");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.Run();

