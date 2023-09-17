using Chat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Helpers
{
    public static class PermissionHelper
    {
        public static bool HasPermission(IEnumerable<Permission> permissions, string action)
        {
            return permissions.Any(p => p.Action == action && p.CanPerform);
        }
    }
}
