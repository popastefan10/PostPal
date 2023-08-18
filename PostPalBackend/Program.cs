using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PostPalBackend.Data;
using PostPalBackend.Helpers;
using PostPalBackend.Helpers.Extensions;
using PostPalBackend.Helpers.Middleware;
using PostPalBackend.Helpers.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<PostPalDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PostPal")));
builder.Services.AddControllers();
builder.Services.AddRepositories();
builder.Services.AddSeeders();
builder.Services.AddServices();
builder.Services.AddUtils();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Description = "Please enter JWT token",
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		BearerFormat = "JWT",
		Scheme = "bearer"
	});

	options.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type=ReferenceType.SecurityScheme,
					Id="Bearer"
				}
			},
			new string[]{}
		}
	});
});

var app = builder.Build();

SeedData(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.EnablePersistAuthorization();
	});
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Custom middleware
app.UseMiddleware<JwtMiddleware>();
app.ConfigureExceptionHandler();

app.MapControllers();

app.Run();

static void SeedData(IHost app)
{
	var serviceProvider = app.Services.GetService<IServiceScopeFactory>()?.CreateScope().ServiceProvider;

	var usersSeeder = serviceProvider?.GetService<UsersSeeder>();
	usersSeeder?.SeedInitialUsers();
}
