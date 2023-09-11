using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Exceptions.NotFound
{
    public class ContributorNotFoundException : NotFoundException
    {
        public ContributorNotFoundException(Guid userId) : base($"The contributor with the User identifier {userId} was not found.")
        {
        }
    }
}
