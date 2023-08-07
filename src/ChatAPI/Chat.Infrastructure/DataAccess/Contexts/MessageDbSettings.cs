using Chat.Infrastructure.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.DataAccess.Contexts
{
    public class MessageDbSettings : DbSetting
    {
        
        public MessageDbSettings(string collectionName)
        {
            CollectionName = collectionName;
        }
    }
}
