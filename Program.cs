using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore.SqlServer;

var builder = WebApplication.CreateBuilder(args);

// Configuración de Swagger para la documentación de la API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HotelReservationAPI", Version = "v1" });
});

// Configuración de DbContext
// builder.Services.AddDbContext<HotelContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("HotelDatabase")));

// Configura los controladores
builder.Services.AddControllers();



builder.Services.AddDbContext<HotelDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Configuración del pipeline de HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelReservationAPI v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers(); // Mapea los controladores para que la API funcione correctamente

app.Run();
