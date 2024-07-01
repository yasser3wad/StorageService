using Microsoft.EntityFrameworkCore;
using Storage.Domain.Entities;

namespace Storage.Persistence
{
    public class StorageContext : DbContext
    {
        public StorageContext(DbContextOptions<StorageContext> options)
            : base(options)
        { }

        public DbSet<FileMetadata> FileMetadata { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StorageContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
        public Task MigrateAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return Database.MigrateAsync(cancellationToken);
            }
            catch
            {
                return Task.CompletedTask;
            }
        }

    }
}
