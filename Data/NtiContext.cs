using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NTI_Technical_Test.Models;

namespace NTI_Technical_Test.Data;

public partial class NtiContext : DbContext
{

    private readonly IConfiguration _configuration;

    public NtiContext(DbContextOptions<NtiContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = _configuration.GetConnectionString("PostgreSQL");
            optionsBuilder.UseNpgsql(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("classes_pkey");

            entity.ToTable("classes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.HomeroomTeacherId).HasColumnName("homeroom_teacher_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .HasColumnName("status");
            entity.Property(e => e.StudentIds).HasColumnName("student_ids");

            entity.HasOne(d => d.HomeroomTeacher).WithMany(p => p.Classes)
                .HasForeignKey(d => d.HomeroomTeacherId)
                .HasConstraintName("classes_homeroom_teacher_id_fkey");
        });

        modelBuilder.Entity<Student>(entity =>
        {

            entity.HasKey(e => e.Id).HasName("student_pkey");

            entity.ToTable("student");

            entity.HasIndex(e => e.Nisn, "student_nisn_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Dob).HasColumnName("dob");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");
            entity.Property(e => e.Grade).HasColumnName("grade");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Nisn)
                .HasMaxLength(20)
                .HasColumnName("nisn");
            entity.Property(e => e.Parents)
                .HasMaxLength(50)
                .HasColumnName("parents");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("subjects_pkey");

            entity.ToTable("subject");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('subjects_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Category)
                .HasMaxLength(20)
                .HasComment("enum(compulsory/optional)")
                .HasColumnName("category");
            entity.Property(e => e.GradeQualification).HasColumnName("grade_qualification");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.StudentIds).HasColumnName("student_ids");
            entity.Property(e => e.TeacherIds).HasColumnName("teacher_ids");
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .HasComment("enum(formal/non-formal)")
                .HasColumnName("type");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("teacher_pkey");

            entity.ToTable("teacher");

            entity.HasIndex(e => e.Nip, "teacher_nip_key").IsUnique();

            entity.HasIndex(e => e.Nuptk, "teacher_nuptk_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Certification).HasColumnName("certification");
            entity.Property(e => e.Dob).HasColumnName("dob");
            entity.Property(e => e.EmploymentStatus)
                .HasMaxLength(3)
                .HasColumnName("employment_status");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Nip)
                .HasMaxLength(18)
                .HasColumnName("nip");
            entity.Property(e => e.Nuptk)
                .HasMaxLength(16)
                .HasColumnName("nuptk");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(10)
                .HasComment("enum(admin/user)")
                .HasColumnName("role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
