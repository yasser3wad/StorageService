using Microsoft.Extensions.DependencyInjection;
using Storage.CoreInfrastructure.CoreDAL;
using Storage.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StorageContext _context;
        private readonly IServiceProvider _serviceProvider;

        private IBaseRepository<FileMetadata> _fileRepo;
        public UnitOfWork(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _context = serviceProvider.GetRequiredService<StorageContext>();
        }

        public IBaseRepository<FileMetadata> FileRepo => _fileRepo ??= new BaseRepository<FileMetadata>(_serviceProvider);


        public Task MigrateAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return _context.MigrateAsync(cancellationToken);
            }
            catch
            {
                return Task.CompletedTask;
            }
        }
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
