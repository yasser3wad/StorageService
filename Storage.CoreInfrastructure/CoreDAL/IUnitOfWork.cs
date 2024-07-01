using Storage.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.CoreInfrastructure.CoreDAL
{
    public interface IUnitOfWork : IMigratable
    {
        IBaseRepository<FileMetadata> FileRepo { get; }

        public Task MigrateAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
