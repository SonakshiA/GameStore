using System.Reflection.Metadata.Ecma335;
using Gamestore.Api.Data;
using Gamestore.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.Api.Endpoints;

public static class GenresEndpoints
{
    public static WebApplication MapGenresEndpoints(this WebApplication app){
        app.MapGet("genres",(GameStoreContext dbContext) => {
            dbContext.Genres.Select(genre => genre.ToDto()).AsNoTracking();
        });

        return app;
    }
}
