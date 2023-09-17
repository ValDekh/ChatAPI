using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Entities
{
    public class Permission : BaseEntity
    {
        public string Action { get; set; } = null!;
        public bool CanPerform { get; set; }
    }
}
