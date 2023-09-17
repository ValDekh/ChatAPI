using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.EventHandlers.ContributorCreateEventHandlers
{
    public class ContributorCreatedEventHandler : BaseEventHandler<ContributorCreateEventArgs>
    {
        private readonly ContributorCreateObserver _contributorCreateObserver;
        public ContributorCreatedEventHandler(ContributorCreateObserver contributorCreateObserver)
        {
            _contributorCreateObserver = contributorCreateObserver;
            OnCreate += _contributorCreateObserver.CreateContributor;
        }
    }
}
