using api.Common.Configurations;
using api.Common.Settings;
using api.Data.Contexts;
using api.Data.Models;
using Microsoft.AspNetCore.Identity;

var appSettings = new AppSettings();
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddContextDependecies(builder.Configuration, out appSettings)
                .AddDependencies()
                .AddJWT(appSettings)
                .AddSwaggerConfigs();

builder.Services.AddIdentity<User, IdentityRole>(config =>
                    {
                        config.User.RequireUniqueEmail = true;
                        config.Password.RequireDigit = false;
                        config.Password.RequireLowercase = false;
                        config.Password.RequireUppercase = false;
                        config.Password.RequireNonAlphanumeric = false;
                        config.Password.RequiredLength = 4;
                    })
                .AddEntityFrameworkStores<SiteDbContext>()
                .AddDefaultTokenProviders();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder => builder.WithOrigins("https://localhost")
                                               .SetIsOriginAllowed(x => true)
                                               .AllowAnyHeader()
                                               .AllowAnyMethod());
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