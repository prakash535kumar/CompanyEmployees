using CompanyEmployees;
using CompanyEmployees.Extensions;
using Contracts;
using NLog;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(),
"/nlog.config"));

// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();

builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureServiceManager();
builder.Services.AddAutoMapper(typeof(Program)); // AutoMapper


// This method registers only the controllers in IServiceCollection and not Views or Pages because they are not required in the Web API project which we are building.
/*Without this code, our API wouldn’t work, and wouldn’t know where to
route incoming requests. But now, our app will find all of the controllers
inside of the Presentation project and configure them with the
framework. They are going to be treated the same as if they were defined
conventionally.
*/
builder.Services.AddControllers()
.AddApplicationPart(typeof(CompanyEmployees.Presentation.AssemblyReference).Assembly);

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);
if (app.Environment.IsProduction())
    app.UseHsts();

// We don't need "app.Environment.IsDevelopment()" anymore, as we are Handling Exception above
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    /*we remove the call to the UseDeveloperExceptionPage
method in the development environment since we don’t need it now and 
it also interferes with our error handler middleware.
*/
    //app.UseDeveloperExceptionPage();
}

//The UseHttpRedirection method is used to add the middleware for the redirection from HTTP to HTTPS
app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
