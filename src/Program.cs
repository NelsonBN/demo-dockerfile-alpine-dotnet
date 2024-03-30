var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

var app = builder.Build();

app.UseSwagger()
   .UseSwaggerUI();

app.MapGet("/demo-request", (ILoggerFactory factory) =>
{
    var logger = factory.CreateLogger("Endpoint");

    // Get time in local time zone
    var now = DateTime.Now;

    // Get time in UTC
    var utcNow = DateTime.UtcNow;

    // Get Tokyo Standard Time zone
    TimeZoneInfo tokyoTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");
    DateTime tokyoNow = TimeZoneInfo.ConvertTime(now, TimeZoneInfo.Local, tokyoTimeZone);


    logger.LogInformation($"Hello, World! at: '{now}/{utcNow}/{tokyoNow}'");

    return Results.Ok(new
    {
        Message = "Hello, World!",
        Now = now,
        UtcNow = utcNow,
        TokyoNow = tokyoNow

    });
}).WithOpenApi();

app.Run();
