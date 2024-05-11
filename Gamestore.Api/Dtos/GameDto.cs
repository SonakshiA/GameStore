namespace Gamestore.Api.Dtos;

public record class GameDto(int Id, string Name, string Genre, decimal Price, DateOnly ReleaseDate);

//records are immutable, so we use them over class for DTO (date transfer objects)