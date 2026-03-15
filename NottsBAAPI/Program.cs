using Microsoft.OpenApi;
using NottsBAAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "NottsBA API", Version = "1" });
});

builder.Services.AddCors(o =>
{
    o.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // Your Angular URL
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .SetIsOriginAllowedToAllowWildcardSubdomains();
        });
});

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NottsBAAPI v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAngularApp");

app.MapControllers();

app.Run();