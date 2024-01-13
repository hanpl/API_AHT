using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:3000", "http://localhost:3001", 
            "http://localhost:5173", "http://localhost:8082", "http://172.17.18.12:8585", 
            "http://172.17.19.95:8085", "http://172.17.18.12:8586")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });
});
//WithOrigins("http://localhost:3000", "http://localhost:3001", "http://localhost:5173", "http://localhost:8082", "http://172.17.18.12:8585", "http://172.17.19.95:8085", "http://172.17.18.12:8586")

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyCorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();

// 
