using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.DataAccess
{
    public class ChatDbSetting
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string ChatCollectionName { get; set; }
    }
}
