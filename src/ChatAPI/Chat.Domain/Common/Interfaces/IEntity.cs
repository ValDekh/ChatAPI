using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Common.Interfaces
{
    internal interface IEntity
    {
        public Guid Id { get; set; }
    }
}
