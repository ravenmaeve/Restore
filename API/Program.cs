using API.Data;
using API.Entities;
using API.Middleware;
using Microsoft.AspNetCore.Identity;
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

//for Identity
builder.Services.AddIdentityApiEndpoints<User>(options =>
{
    options.User.RequireUniqueEmail = true;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<StoreContext>();


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

//for identity
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//for identity
app.MapGroup("api").MapIdentityApi<User>();// api/login

DbInitializer.InitDb(app);

app.Run();
