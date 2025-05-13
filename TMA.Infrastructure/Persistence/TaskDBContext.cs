using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TMA.Domain.Entities;


namespace TMA.Infrastructure.Persistence
{
    public class TaskDBContext(DbContextOptions<TaskDBContext> options) : DbContext(options)
    {
        public virtual DbSet<TaskEntity> Tasks { get; set; }
        public virtual DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) => 
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
       
    }
}
