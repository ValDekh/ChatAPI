using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Exceptions.NotFound
{
    public sealed class ChatNotFoundException : NotFoundException
    {
        public ChatNotFoundException(Guid chatId)
            : base($"The chat with the identifier {chatId} was not found.")
        {
        }
    }
}
