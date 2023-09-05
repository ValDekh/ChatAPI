using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.DTOs.Message
{
    public class MessageDTORequest
    {
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public string Text { get; set; }
        public string TrackUrl { get; set; }
    }
}
