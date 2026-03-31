var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
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

// Диагностические эндпоинты
app.MapGet("/health", () => Results.Json(new
{
    status = "ok",
    timestamp = DateTime.UtcNow
}));

app.MapGet("/version", (IConfiguration config) =>
{
    var appName = config["App:Name"];
    var appVersion = config["App:Version"];
    return Results.Json(new
    {
        application = appName,
        version = appVersion
    });
});

app.MapGet("/db/ping", (IConfiguration config) =>
{
    var connectionString = config.GetConnectionString("Mssql");
    return Results.Json(new
    {
        status = "not_implemented",
        message = "Database connection not implemented yet",
        connectionString = connectionString
    });
});

app.MapControllers();

app.Run();
