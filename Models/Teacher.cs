using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NTI_Technical_Test.Models;

public partial class Teacher
{
    public int Id { get; set; }

    [Required, MaxLength(18)] public string Nip { get; set; } = null!;

    [Required, MaxLength(16)] public string Nuptk { get; set; } = null!;

    [Required, MaxLength(100)] public string Name { get; set; } = null!;

    [Required, MaxLength(10)] public string Gender { get; set; } = null!;

    [Required] public DateOnly Dob { get; set; }

    [Required, MaxLength(3)] public string EmploymentStatus { get; set; } = null!;

    [Required] public bool Certification { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
