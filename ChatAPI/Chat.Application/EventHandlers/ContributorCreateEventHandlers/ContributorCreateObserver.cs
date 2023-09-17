using Chat.Domain.Entities;
using Chat.Domain.Interfaces;
using Chat.Domain.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.EventHandlers.ContributorCreateEventHandlers
{
    public class ContributorCreateObserver
    {
        private readonly IContributorRepository _contributorRepository;
        public ContributorCreateObserver(IContributorRepository contributorRepository)
        {
            _contributorRepository = contributorRepository;
        }

        public async void CreateContributor(object sender, ContributorCreateEventArgs e)
        {
            var contributor = new Contributor()
            {
                UserId = e.UserId,
                ChatId = e.ChatId,
                Permissions = Permissions.Admin.Select(action => new Permission()
                {
                    Action = action,
                    CanPerform = true,
                }).ToList()
            };
            await _contributorRepository.AddAsync(contributor);
        }
    }
}
