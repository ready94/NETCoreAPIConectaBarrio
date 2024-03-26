using MySqlConnector;
using NETCoreAPIConectaBarrio.Services;
using NETCoreAPIConectaBarrio.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMySqlDataSource(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddTransient<MySqlConnection>(_ =>
    new MySqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IComplaintService, ComplaintService>();
builder.Services.AddTransient<ILoginService, LoginService>();
builder.Services.AddTransient<INewsService, NewsService>();
builder.Services.AddTransient<ITournamentService, TournamentService>();
builder.Services.AddTransient<IUserService, UserService>();

var app = builder.Build();

// global cors policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
