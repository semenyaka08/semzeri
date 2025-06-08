using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Semzeri.API.Extensions;
using Semzeri.API.Middlewares;
using Semzeri.BusinessLogic.Extensions;
using Semzeri.BusinessLogic.Services.Interfaces;
using Semzeri.DAL;
using Semzeri.DAL.Entities;
using Semzeri.DAL.Extensions;
using Semzeri.DAL.Seeders;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApi()
    .AddDataAccess(builder.Configuration)
    .AddBusiness();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
await dbContext.Database.MigrateAsync();
var seeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
await seeder.SeedAsync();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x.AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .WithOrigins("http://localhost:4200", "https://localhost:4200"));

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/{code}", async (string code, [FromServices] IUrlsService urlsService) =>
{
    var originalUrl = await urlsService.GetUrlByCodeAsync(code);

    return Results.Redirect(originalUrl);
});

app.MapControllers();
app.MapGroup("api").MapIdentityApi<AppUser>();

app.Run();