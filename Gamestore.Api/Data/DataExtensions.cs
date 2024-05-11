﻿using Microsoft.EntityFrameworkCore;

namespace Gamestore.Api.Data;
//to automatically create all Migrations.
public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app){
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        dbContext.Database.Migrate();
    }
}
