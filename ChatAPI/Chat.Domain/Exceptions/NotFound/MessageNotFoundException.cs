using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Exceptions.NotFound
{
    public sealed class MessageNotFoundException : NotFoundException
    {
        public MessageNotFoundException(Guid messageId)
            : base($"The message with the identifier {messageId} was not found.")
        {
        }
    }
}
