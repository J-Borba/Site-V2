using api.Configurations;
using api.Data;
using api.Data.Dtos.Common;
using api.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var appSettings = new AppSettingsDto();
// Add services to the container.
builder.Services.AddContextDependecies(builder.Configuration, out appSettings)
                .RegisterDI()
                .RegisterApiConfig(appSettings)
                .RegisterSwagger();

builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<SiteDbContext>()
                .AddDefaultTokenProviders();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
                   .SetIsOriginAllowed(x => true)
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.Run();
