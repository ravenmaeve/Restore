using API.Data;
using API.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//this is for DbContext
builder.Services.AddDbContext<StoreContext>(options =>{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
//fixed CORS issue
builder.Services.AddCors();

//for Expception Middleware
builder.Services.AddTransient<ExceptionMiddleware>();
//builder.Services.AddScoped<IMiddleware>()

/**************************************/
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();
/**************************************/
var app = builder.Build();

//for Exception Handling
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
/**************************************/
// if (app.Environment.IsDevelopment())
// {
//     app.MapOpenApi();
// }

// app.UseHttpsRedirection();

// app.UseAuthorization();
/**************************************/

//for allowing client's access
app.UseCors(opt =>{
    opt.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("https://localhost:3000");
});

app.MapControllers();

DbInitializer.InitDb(app);

app.Run();
