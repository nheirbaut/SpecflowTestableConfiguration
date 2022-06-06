using SpecflowTestableConfiguration.Api.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((_, config) =>
{
    if (!Directory.Exists("CustomOptions"))
        Directory.CreateDirectory("CustomOptions");

    if (!File.Exists("CustomOptions/CustomData.json"))
        File.Copy("DefaultCustomOptions/CustomData.json", "CustomOptions/CustomData.json");

    config.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "CustomOptions/CustomData.json"), optional: false, reloadOnChange: true);
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<CustomDataOptions>(builder.Configuration.GetSection(CustomDataOptions.CustomData));

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

public partial class Program { }