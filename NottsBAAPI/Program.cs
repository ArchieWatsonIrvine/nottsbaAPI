using Microsoft.Data.Sqlite;
using Microsoft.OpenApi;
using NottsBAAPI;
using NottsBAAPI.Extensions;
using NottsBAAPI.Models;

var builder = WebApplication.CreateBuilder(args);
dbConfig.SetupDatabase();

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

app.MapPost("/api/query", (SqlRequest request) =>
{
    if (string.IsNullOrWhiteSpace(request.Query))
    {
        return Results.BadRequest(new { error = "No SQL query provided." });
    }

    try
    {
        // Reference the connection string from your DbConfig class
        using var conn = new SqliteConnection(dbConfig.ConnectionString);
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = request.Query;

        if (request.Query.TrimStart().StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
        {
            using var reader = cmd.ExecuteReader();
            var results = new List<Dictionary<string, object>>();

            while (reader.Read())
            {
                var row = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                }
                results.Add(row);
            }
            return Results.Ok(new { data = results });
        }
        else
        {
            int rowsAffected = cmd.ExecuteNonQuery();
            return Results.Ok(new { status = "Success", rows_affected = rowsAffected });
        }
    }
    catch (SqliteException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
    catch (Exception ex)
    {
        return Results.Problem(detail: ex.Message, statusCode: 500);
    }
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAngularApp");

app.MapControllers();

app.Run();