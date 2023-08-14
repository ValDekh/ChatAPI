using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Collection_Factory.Databases
{
    public interface IMongoDatabaseFactory
    {
        ValueTask<IMongoDatabase> Get(CancellationToken ct);
    }
}
