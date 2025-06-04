using Microsoft.EntityFrameworkCore;
using Semzeri.API.Extensions;
using Semzeri.API.Middlewares;
using Semzeri.DAL;
using Semzeri.DAL.Entities;
using Semzeri.DAL.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApi()
    .AddDataAccess(builder.Configuration);

var app = builder.Build();

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
await dbContext.Database.MigrateAsync();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.MapIdentityApi<AppUser>();

app.Run();