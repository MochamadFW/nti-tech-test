using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NTI_Technical_Test.Models;

public partial class Subject
{
    public int Id { get; set; }
    [Required, MaxLength(100)] public string Name { get; set; } = null!;

    /// <summary>
    /// enum(formal/non-formal)
    /// </summary>
    [Required, MaxLength(20)] public string Type { get; set; } = null!;

    /// <summary>
    /// enum(compulsory/optional)
    /// </summary>
    [Required, MaxLength(20)] public string Category { get; set; } = null!;

    public List<int>? StudentIds { get; set; }

    public List<int>? TeacherIds { get; set; }

    public int? GradeQualification { get; set; }
}
