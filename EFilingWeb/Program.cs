using EFilingWeb.Handler;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// builder.Services.AddCorrelationId()

builder.Logging.AddConsole(b => b.LogToStandardErrorThreshold = LogLevel.Trace);

IServiceCollection services = builder.Services;

// Add services to the container.
services.AddControllers()
        .ConfigureApiBehaviorOptions(x => {
                                       x.SuppressMapClientErrors = true;
                                     });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddLogging(b => b.AddConsole().AddFilter(level => level >= LogLevel.Trace));

services.AddScoped<TexGenerator>();
services.AddScoped<PdfGenerator>();
services.AddScoped<PdfStreamGenerator>();

services.AddRazorPages();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
  app.UseSwagger();
  app.UseSwaggerUI();
} else {
  app.UseHsts();
}

// app.UseCorrelationId();

app.UseHttpsRedirection();

// app.UseAuthorization();
app.UseStaticFiles();
app.MapRazorPages();
app.UseRouting();
app.MapControllers();

app.Run();



public partial class Program {

}
