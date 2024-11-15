using WebApi.Data;
using WebApi.Data.Interfaces;
using WebApi.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

PostgresqlConfiguration postgresqlConfiguration = new PostgresqlConfiguration(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
builder.Services.AddSingleton(postgresqlConfiguration);

builder.Services.AddScoped<ITareaService, TareaService>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowFrontEnd", policy =>
    { 
    policy.WithOrigins("http://localhost:4200") //aqui pones la url de tu frontend
    .AllowAnyHeader()
    .AllowAnyMethod();
    });

});





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontEnd");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
