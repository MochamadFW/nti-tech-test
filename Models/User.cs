using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NTI_Technical_Test.Models;

public partial class User
{
    public int Id { get; set; }

    [Required, MaxLength(100)] public string Name { get; set; } = null!;

    [Required, MaxLength(50)] public string Email { get; set; } = null!;

    [Required, MaxLength(255)] public string Password { get; set; } = null!;

    /// <summary>
    /// enum(admin/user)
    /// </summary>
    [Required, MaxLength(10)] public string Role { get; set; } = null!;
}
