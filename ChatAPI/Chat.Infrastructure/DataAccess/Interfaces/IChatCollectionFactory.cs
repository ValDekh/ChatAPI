using Chat.Domain.Interfaces.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.DataAccess.Interfaces
{
    public interface IChatCollectionFactory : IMongoCollectionFactory
    {
    }
}
