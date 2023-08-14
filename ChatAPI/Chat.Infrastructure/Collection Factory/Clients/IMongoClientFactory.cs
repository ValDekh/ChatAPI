using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Collection_Factory.Clients
{
    public interface IMongoClientFactory
    {
        IMongoClient GetOrCreate();
    }
}
