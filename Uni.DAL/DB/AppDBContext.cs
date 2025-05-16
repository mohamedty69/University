using Microsoft.EntityFrameworkCore;
using System;
using Uni.DAL.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace Uni.DAL.DB
{
    public class AppDbContext :  IdentityDbContext<Student>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Records> Records { get; set; }
        public DbSet<Takes> Takes { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Teaches> Teaches { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<AcademicStatus> AcademicStatus { get; set; }
        public DbSet<Prerequisites> Prerequisites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Course first
            modelBuilder.Entity<Course>(entity =>
            {
                // Configure the Prerequisites relationships
                entity.HasMany(c => c.Prerequisites)
                    .WithOne(p => p.MainCourse)
                    .HasForeignKey(p => p.CourseId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();

                entity.HasMany(c => c.IsPrerequisiteFor)
                    .WithOne(p => p.PrerequisiteCourse)
                    .HasForeignKey(p => p.PrerequisiteId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();

                // Configure Department relationship
                entity.HasOne(c => c.Department)
                    .WithMany()
                    .HasForeignKey(c => c.DeptName)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // Configure Prerequisites
            modelBuilder.Entity<Prerequisites>(entity =>
            {
                // Set composite primary key
                entity.HasKey(p => new { p.CourseId, p.PrerequisiteId });

                // Add index for better performance
                entity.HasIndex(p => p.CourseId);
                entity.HasIndex(p => p.PrerequisiteId);
            });

            // Configure other entities
            modelBuilder.Entity<Takes>()
                .HasKey(t => new { t.Id, t.CourseCode, t.Semester });

            modelBuilder.Entity<Teaches>()
                .HasKey(t => new { t.IId, t.CId, t.Semester, t.Year });

      
        
        }
    }
}
