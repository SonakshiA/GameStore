using Gamestore.Api.Data;
using Gamestore.Api.Dtos;
using Gamestore.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.Api.GamesEndpoints;

public static class GamesEndpoints
{
    const string GetGameEndPointName = "GetGame";

    // private static readonly List<GameDto> games = [
    //     new GameDto(1, "Street Fighter","Fighting",19.99M, new DateOnly(1992,7,15)),
    //     new GameDto(2, "Final Fantasy XIV","Roleplaying",59.99M,new DateOnly(2010,9,30)),
    //     new GameDto(3, "FTFA 23","Sports",69.99M,new DateOnly(2022,9,27)) //69.99M -> the M means it's decimal type
    // ];

    //Extension methods enable you to "add" methods to existing types without creating a new derived type, 
    public static WebApplication MapGamesEndpoints(this WebApplication app){
        // GET /games
        app.MapGet("games",(GameStoreContext dbContext) => 
        dbContext.Games
        .Include(game => game.Genre)
        .Select(game => game.ToDto())
        .AsNoTracking());

        // GET /games/1
        app.MapGet("games/{id}", (int id, GameStoreContext dbContext) => {
           // GameDto? game = games.Find(game => game.Id == id); // ? because could be null
            
            Game? game = dbContext.Games.Find(id);

            
            return game is null ? Results.NotFound() : Results.Ok(game.ToDetailsDto());
        })
        .WithName(GetGameEndPointName); //give a name to the endpoint

        // POST /games
        app.MapPost("games",(CreateGameDto newGame, GameStoreContext dbContext) => {
            // GameDto game = new(games.Count + 1, newGame.Name, newGame.Genre, newGame.Price, newGame.ReleaseDate);
            // games.Add(game);

            //mapping to Dto to Entity
            Game game = newGame.ToEntity();
            game.Genre = dbContext.Genres.Find(game.GenreId);

            dbContext.Games.Add(game);
            dbContext.SaveChanges();

            
            return Results.CreatedAtRoute(GetGameEndPointName,new { id = game.Id}, game.ToDto()); //return gameDto and not game
        }).WithParameterValidation(); //Validating the requirements mentioned in CreateGameDto.cs
        //We used Nugat package (MinimalApis.Extensions) for this to work

        
        // PUT games/1
        app.MapPut("games/{id}",(int id, UpdateGameDto updatedGame, GameStoreContext dbContext) => {
           // var index = games.FindIndex(game => game.Id == id); //gives -1 if index is not found
            var existingGame = dbContext.Games.Find(id);

            if(existingGame is null){
                return Results.NotFound();
            }

            //games[index] = new GameDto(id, updatedGame.Name, updatedGame.Genre, updatedGame.Price, updatedGame.ReleaseDate);

            
            
            dbContext.Entry(existingGame).CurrentValues.SetValues(updatedGame.ToEntity(id));
            dbContext.SaveChanges();
            return Results.NoContent();
        }).WithParameterValidation();

        // DELETE games/1
        app.MapDelete("games/{id}",(int id, GameStoreContext dbContext) => {
            //games.RemoveAll(game => game.Id == id); 

            dbContext.Games.Where(game => game.Id == id).ExecuteDelete();
            dbContext.SaveChanges();
            return Results.NoContent();
        });

        return app;
    }
}
