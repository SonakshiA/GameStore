﻿using System.ComponentModel.DataAnnotations;

namespace Gamestore.Api.Dtos;

public record class CreateGameDto(
[Required][StringLength(50)] string Name, 
int GenreId, 
[Range(1,100)]decimal Price, 
DateOnly ReleaseDate);
