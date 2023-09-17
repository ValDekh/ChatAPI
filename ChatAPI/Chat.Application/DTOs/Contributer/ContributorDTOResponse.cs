using Chat.Domain.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.DTOs.Contributer
{
    public class ContributorDTOResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ChatId { get; set; }
        public List<Permission> Permissions { get; set; }
    }
}
