namespace Gamestore.Api.Dtos;

public record class GameDetailsDto(
    int Id, string Name, int GenreId, decimal Price, DateOnly ReleaseDate);

//records are immutable, so we use them over class for DTO (date transfer objects)