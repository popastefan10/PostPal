using PostPalBackend.Data;
using Microsoft.EntityFrameworkCore;
using PostPalBackend.Helpers.Seeders;
using PostPalBackend.Helpers.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<PostPalDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PostPal")));
builder.Services.AddControllers();
builder.Services.AddRepositories();
builder.Services.AddSeeders();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

SeedData(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void SeedData(IHost app) {
	var serviceProvider = app.Services.GetService<IServiceScopeFactory>()?.CreateScope().ServiceProvider;

	var usersSeeder = serviceProvider?.GetService<UsersSeeder>();
	usersSeeder?.SeedInitialUsers();
}
