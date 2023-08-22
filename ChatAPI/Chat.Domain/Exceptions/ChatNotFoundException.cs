﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Exceptions
{
    public sealed class ChatNotFoundException : NotFoundException
    {
        public ChatNotFoundException(Guid ownerId)
            : base($"The chat with the identifier {ownerId} was not found.")
        {
        }
    }
}