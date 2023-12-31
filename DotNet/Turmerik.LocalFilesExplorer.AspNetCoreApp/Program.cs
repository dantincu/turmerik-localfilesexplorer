using Microsoft.AspNetCore.Authentication.Negotiate;
using System.Text.Json.Serialization;
using Turmerik.LocalFileNotes.AspNetCoreApp.Controllers;
using Turmerik.LocalFilesExplorer.AspNetCoreApp.Dependencies;
using Turmerik.LocalFilesExplorer.AspNetCoreApp.Settings;

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);
LocalFilesExplorerAspNetCoreServices.RegisterAll(builder.Services);

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = null;
    serverOptions.Limits.MaxResponseBufferSize = null;
});

var clientHost = builder.Configuration.GetValue<string>("ClientHost");

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins(
                clientHost).AllowAnyHeader(
                ).AllowAnyMethod(
                ).AllowCredentials();
        });
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ClientVersionAndUserUuidFilter>();
}).AddJsonOptions(options => {
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(
    NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy.
    options.FallbackPolicy = options.DefaultPolicy;
});

var app = builder.Build();
https://www.wipo.int/pct-eservices/en/support/cert_import_backup_chrome.html
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(myAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
