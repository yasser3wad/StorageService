using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.CoreInfrastructure.CoreDAL
{
    public interface IMigratable
    {
        Task MigrateAsync(CancellationToken cancellationToken = default);
    }
}
