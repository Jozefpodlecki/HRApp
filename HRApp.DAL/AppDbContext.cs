using HRApp.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace HRApp.DAL
{
    [ExcludeFromCodeCoverage]
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }

        public AppDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;

        public DbSet<OutOfOffice> OutOfOffice { get; set; } = null!;

        public DbSet<AnnualLeaveApplication> AnnualLeaveApplications { get; set; } = null!;

        public DbSet<Person> People { get; set; } = null!;

        public DbSet<PersonStats> PersonStats { get; set; } = null;

        public DbSet<Role> Roles { get; set; } = null;

        public DbSet<UserRole> UserRoles { get; set; } = null;

        public DbSet<StatutorySickPayApplication> StatutorySickPayApplications { get; set; } = null;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Application>()
                .ToTable("Applications")
                .HasDiscriminator<string>("Type")
                .HasValue<AnnualLeaveApplication>(typeof(AnnualLeaveApplication).FullName)
                .HasValue<StatutorySickPayApplication>(typeof(StatutorySickPayApplication).FullName);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
