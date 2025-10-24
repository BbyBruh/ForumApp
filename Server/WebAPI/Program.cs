using FileRepositories;
using RepositoryContracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//*******
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IPost, PostFileRepository>();
builder.Services.AddScoped<IUser, UserFileRepository>();
builder.Services.AddScoped<IComment, CommentFileRepository>();

var app = builder.Build();

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.Run();