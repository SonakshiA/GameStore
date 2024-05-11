using Gamestore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.Api.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : 
        DbContext(options) //inherits, DbContext is a session with the database to qeury and save instances of our entities
{
    public DbSet<Game> Games => Set<Game>();//DbSet is an object to qeury and save instances of Game entity. Set<>() creates the DbSet instance
    public DbSet<Genre> Genres => Set<Genre>();

     //Data seeding to automatically add data to the Genres table. Created when migrations executes
    protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.Entity<Genre>().HasData(
            new{Id = 1, Name = "Fighting",},
            new{Id = 2, Name = "Roleplaying"},
            new{Id = 3, Name = "Sports"},
            new{Id = 4, Name = "Racing"},
            new{Id = 5, Name = "Kids and Family"}
        );
    }

}

//options provides details regarding how to connect to the actual database. We pass connString in Program.cs file 