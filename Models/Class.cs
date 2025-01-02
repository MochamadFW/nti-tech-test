using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTI_Technical_Test.Models;

public partial class Class
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = null!;

    public List<int>? StudentIds { get; set; }

    public int? HomeroomTeacherId { get; set; }

    public int Capacity { get; set; }

    [MaxLength(10)]
    public string Status { get; set; } = null!;

    public virtual Teacher? HomeroomTeacher { get; set; }
}
