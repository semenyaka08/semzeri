using Microsoft.EntityFrameworkCore;
using Semzeri.API.Extensions;
using Semzeri.API.Middlewares;
using Semzeri.BusinessLogic.Services.Interfaces;
using Semzeri.DAL;
using Semzeri.DAL.Entities;
using Semzeri.DAL.Extensions;
using Semzeri.DAL.Seeders;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApi()
    .AddDataAccess(builder.Configuration);

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
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/{code}", async (string code, IUrlsService urlsService) =>
{
    var originalUrl = await urlsService.GetUrlByCodeAsync(code);

    return Results.Redirect(originalUrl);
});

app.MapControllers();
app.MapGroup("api").MapIdentityApi<AppUser>();

app.Run();