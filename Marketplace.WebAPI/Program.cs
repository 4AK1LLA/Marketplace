using Marketplace.Core.Interfaces;
using Marketplace.WebAPI.Extensions;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureOptions(builder.Configuration);
builder.Services.AddJwtAuthentication();
builder.Services.AddMyCors(builder.Configuration, MyAllowSpecificOrigins);
builder.Services.AddControllers();
builder.Services.AddSwagger();
builder.Services.AddAutoMapper();
builder.Services.AddMyServices();
builder.Services.AddUnitOfWork();
builder.Services.AddSerializator();
builder.Services.AddMyDbContext();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<ISeeder>();

    seeder.Seed();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();