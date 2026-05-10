using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Task> Tasks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(u => u.FullName).HasMaxLength(200);
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("Tasks");
                entity.HasKey(t => t.Id);

                entity.Property(t => t.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(t => t.Description)
                    .HasMaxLength(2000);

                entity.Property(t => t.TypeTask)
                    .HasConversion<string>()
                    .HasMaxLength(50);

                entity.Property(t => t.Status)
                    .HasConversion<string>()
                    .HasMaxLength(50);

                entity.Property(t => t.CreatedAt)
                    .HasDefaultValueSql("NOW()")
                    .ValueGeneratedOnAdd();

                entity.HasOne(t => t.Parent)
                    .WithMany(t => t.Children)
                    .HasForeignKey(t => t.ParentId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false);

                entity.HasOne(t => t.User)
                    .WithMany(u => u.Tasks)
                    .HasForeignKey(t => t.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<TaskTag>(entity =>
            {
                entity.ToTable("TaskTags");
                entity.HasKey(t => t.Id);

                entity.Property(t => t.Name)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.HasOne(t => t.TaskItem)
                      .WithMany(t => t.Tags)
                      .HasForeignKey(t => t.TaskId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}