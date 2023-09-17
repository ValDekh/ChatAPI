using Chat.Domain.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Exceptions.ContributorExist
{
    public class ContributorExistException : ArgumentException
    {
        public ContributorExistException(Guid userId) : base($"User with Id: {userId} already has a such contributor",
            paramName:nameof(Contributor)) 
        {

        }
    }
}
