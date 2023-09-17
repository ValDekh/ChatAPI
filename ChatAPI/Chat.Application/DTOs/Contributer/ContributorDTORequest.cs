using Chat.Domain.Entities;
using Chat.Domain.Structures;
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

        public bool CanReadMessages { get; set; }

        public bool CanCreateMessages { get; set; }

        public bool CanUpdateMessages { get; set; }

        public bool CanDeleteMessages { get; set; }

        public bool CanReadContributor { get; set; }

        public bool CanAddContributor { get; set; }

        public bool CanUpdateContributor { get; set; }

        public bool CanDeleteContributor { get; set; }


        public List<Permission> GetSelectedPermissions()
        {
            var selectedPermissions = new List<Permission>
        {
            new Permission { Action = Permissions.ReadMessage, CanPerform = CanReadMessages },
            new Permission { Action = Permissions.CreateMessage, CanPerform = CanCreateMessages },
            new Permission { Action = Permissions.UpdateMessage, CanPerform = CanUpdateMessages  },
            new Permission { Action = Permissions.DeleteMessage, CanPerform = CanDeleteMessages },
            new Permission { Action = Permissions.ReadContributor, CanPerform = CanReadContributor },
            new Permission { Action = Permissions.AddContributor, CanPerform = CanAddContributor },
            new Permission { Action = Permissions.UpdateContributor, CanPerform = CanUpdateContributor },
            new Permission { Action = Permissions.DeleteContributor, CanPerform = CanDeleteContributor },
        };

            return selectedPermissions;
        }
    }
}