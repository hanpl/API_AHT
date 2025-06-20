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
        builder.WithOrigins("http://localhost:3000", "http://localhost:3001", "http://172.17.18.12:8385", "http://172.17.18.12:8383", "http://172.17.18.12:8087", "http://172.17.18.12:8088",
            "http://localhost:5173", "http://localhost:8082", "http://172.17.18.12:8585", "http://172.17.18.12:8282", "http://172.17.18.12:8281", "http://172.17.18.12:8384",
            "http://172.17.19.95:8085", "http://172.17.18.12:8586", "https://localhost:7248", "http://172.17.18.12:8082",
            "http://172.17.18.12:8587", "http://172.17.18.12:8588" , "http://172.17.18.12:8284")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });
});

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
