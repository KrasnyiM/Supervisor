using DelphiSupervisorV6;
using SupervisorLogic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IConfigWatcher, ConfigWatcher>();
builder.Services.AddSingleton<IProcessProvider, SdkProcessProvider>();
builder.Services.AddSingleton<IServicesTable, ConfiguredServicesTable>();
builder.Services.AddSingleton<IView, ConsoleView>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

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
