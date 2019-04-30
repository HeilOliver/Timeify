using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timeify.Core.Entities;
using Timeify.Core.Interfaces.Gateways;

namespace Timeify.Infrastructure.Context.App
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<JobEntity> Jobs { get; set; }

        public DbSet<JobTaskEntity> JobTasks { get; set; }

        public DbSet<JobTaskUserEntity> JobTaskUserLink { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(ConfigureUser);
            modelBuilder.Entity<JobEntity>(ConfigureJob);
            modelBuilder.Entity<JobTaskEntity>(ConfigureJobTask);
            modelBuilder.Entity<JobTaskUserEntity>(ConfigureJobTaskUserLink);
        }

        private void ConfigureUser(EntityTypeBuilder<UserEntity> builder)
        {
            var tokenNavigation = builder.Metadata.FindNavigation(nameof(UserEntity.RefreshTokens));
            tokenNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            var taskNavigation = builder.Metadata.FindNavigation(nameof(UserEntity.AssignedTasks));
            taskNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder
                .HasAlternateKey(u => u.UserName);

            builder.Ignore(b => b.Email);
            builder.Ignore(b => b.PasswordHash);
        }

        private void ConfigureJob(EntityTypeBuilder<JobEntity> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(JobEntity.JobTasks));
            //EF access the JobTasks collection through its backing field
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }

        private void ConfigureJobTask(EntityTypeBuilder<JobTaskEntity> builder)
        {
            var taskNavigation = builder.Metadata.FindNavigation(nameof(JobTaskEntity.AssignedUsers));
            taskNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }

        private void ConfigureJobTaskUserLink(EntityTypeBuilder<JobTaskUserEntity> builder)
        {
            builder
                .HasKey(bc => new {bc.JobTaskId, bc.UserName});

            builder
                .HasOne(bc => bc.JobTaskEntity)
                .WithMany(c => c.AssignedUsers)
                .HasForeignKey(bc => bc.JobTaskId);

            builder
                .HasOne(bc => bc.UserEntity)
                .WithMany(c => c.AssignedTasks)
                .HasForeignKey(bc => bc.UserName)
                .HasPrincipalKey(b => b.UserName);
        }

        public override int SaveChanges()
        {
            AddAuitInfo();
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            AddAuitInfo();
            return await base.SaveChangesAsync();
        }

        private void AddAuitInfo()
        {
            var entries = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity &&
                            (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added) ((BaseEntity) entry.Entity).Created = DateTime.UtcNow;
                ((BaseEntity) entry.Entity).Modified = DateTime.UtcNow;
            }
        }
    }
}