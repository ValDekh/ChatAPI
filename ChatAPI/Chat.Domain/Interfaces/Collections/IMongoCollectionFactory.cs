using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Interfaces.Collections
{
    public interface IMongoCollectionFactory
    {
        ValueTask<IMongoCollection<T>> Get<T>(CancellationToken ct) where T : class, new();
    }
}
