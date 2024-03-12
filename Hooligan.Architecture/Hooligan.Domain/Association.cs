﻿using System.ComponentModel.DataAnnotations;

namespace Hooligan.Domain;

public class Association
{
    [Key] public Guid Id { get; init; }
    [Required] public string First { get; init; } = string.Empty;
    [Required] public string Second { get; init; } = string.Empty;
    [Required] public string Result { get; init; } = string.Empty;
    [Required] public string Icon { get; init; } = string.Empty;
}