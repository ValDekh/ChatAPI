using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure
{
    public abstract class MongoConnectionOptions
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public abstract class MongoDatabaseOptions
    {
        public string Name { get; set; }
    }

    public abstract class MongoCollectionOptions
    {
        public string Name { get; set; }
    }
}
