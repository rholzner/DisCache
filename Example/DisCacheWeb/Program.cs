using SqlDisCache.Manager;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSqlDisCache(connectionString: "Server=localhost;Database=TestCache.v2;Trusted_Connection=True;Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True;");

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
