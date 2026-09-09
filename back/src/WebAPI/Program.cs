using Domain.Interfaces;
using Scalar.AspNetCore;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(o => o.AddPolicy(
		"MyPolicy", builder =>
		{
			builder.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader();
		}
	)
);

builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
	app.MapScalarApiReference();

	// Seed
	using (var scope = app.Services.CreateScope())
	{
		var context = scope.ServiceProvider.GetRequiredService<SqlDbContext>();
		DbInitializer.Seed(context);
	}
}

app.MapGet("/", () => Results.Ok("API is healthy!"));
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();