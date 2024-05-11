using Gamestore.Api.Data;
using Gamestore.Api.Dtos;
using Gamestore.Api.Endpoints;
using Gamestore.Api.GamesEndpoints;


var builder = WebApplication.CreateBuilder(args);

//connecting to SQLite and registering the service
//var connString = "Data Source=GameStore.db"; //should be in appsettings.json technically
var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connString); //DBContext Registered with Service Provider (dependency injection)
//inject that instance of GameStoreContext into our code. 
//Registered with Scope lifetime, same instance given to all services that need it in one single HTTP Request

//configuration of our request pipeline
var app = builder.Build(); //builds instance of web application
app.MapGamesEndpoints();
app.MapGenresEndpoints();
app.MigrateDb(); //generates Migration automatically

app.Run();


//WebApplication is the host. It represents an HTTP server implementation

//NuGet package installed
//1.Microsoft.EntityFrameworkCore.Sqlite
//2.dotnet add package MinimalApis.Extensions
//3. dotnet-ef
//4.Microsoft.EntityFrameworkCore.Design 

// dotnet ef migrations add InitialCreate --output-dir Data\Migrations - for creating a migration
//dotnet ef database update