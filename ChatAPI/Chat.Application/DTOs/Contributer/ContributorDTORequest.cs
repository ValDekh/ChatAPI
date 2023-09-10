using Chat.Domain.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.DTOs.Contributer
{
    public class ContributorDTORequest
    {
        public Guid UserId { get; set; }
        public List<string> permissions { get; set; }
    }
}
