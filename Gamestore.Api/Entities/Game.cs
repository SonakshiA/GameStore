using Gamestore.Api.Entities;

namespace Gamestore.Api;

public class Game
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public int GenreId { get; set; }

    public Genre? Genre { get; set; } //Relation between Genre class and Game class

    public decimal Price { get; set; }

    public DateOnly ReleaseDate { get; set; }
}
