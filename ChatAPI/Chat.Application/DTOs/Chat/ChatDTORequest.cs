using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.DTOs.Chat
{
    public class ChatDTORequest
    {
        public List<Guid> Users { get; set; }
    }
}
