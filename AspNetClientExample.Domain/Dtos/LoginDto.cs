﻿#pragma warning disable CS8618
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace AspNetClientExample.Domain.Dtos;

public class LoginDto
{
    public string Login { get; set; }
    public string Password { get; set; }
}