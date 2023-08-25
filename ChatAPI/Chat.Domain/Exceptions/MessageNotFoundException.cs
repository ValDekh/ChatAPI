using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Exceptions
{
    public sealed class MessageNotFoundException : NotFoundException
    {
        public MessageNotFoundException(Guid message) 
            : base($"The message with the identifier {message} was not found.")
        {
        }
    }
}
