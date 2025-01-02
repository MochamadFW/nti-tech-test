using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NTI_Technical_Test.Models;

public partial class Student
{
    public int Id { get; set; }

    [Required, MaxLength(20)] public string Nisn { get; set; } = null!;

    [Required, MaxLength(100)] public string Name { get; set; } = null!;

    public int? Grade { get; set; }

    [Required, MaxLength(10)] public string Gender { get; set; } = null!;

    [Required] public DateOnly Dob { get; set; }

    public string? Address { get; set; }

    [MaxLength(100)]public string? Parents { get; set; }
}
