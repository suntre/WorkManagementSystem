#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WorkManagementSystem.Entities;

namespace WorkManagementSystem.Data
{
    public class MyAppData : DbContext
    {
        public MyAppData(DbContextOptions<MyAppData> options) : base(options) { }

        public DbSet<WorkerEntity> Worker { get; set; }

        public DbSet<TaskEntity> Task { get; set; }

        public DbSet<Role> Role { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkerEntity>()
                .Property(w => w.name)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<WorkerEntity>()
                .Property(w => w.lastName)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<TaskEntity>()
                .Property(t => t.title)
                .IsRequired();
            modelBuilder.Entity<Role>()
                .Property(r => r.name)
                .IsRequired();
            modelBuilder.Entity<WorkerEntity>()
                .Property(u => u.login)
                .IsRequired();
            modelBuilder.Entity<WorkerEntity>()
                .Property(u => u.password)
                .IsRequired();
                
        }
    }
}
