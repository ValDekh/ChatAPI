﻿using Chat.Domain.Entities;
using Chat.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Services.Interfaces
{
    public interface IMongoRepositoryFactory
      
    {
        IRepository<T> CreateRepository<T>(string collectionName) where T : BaseEntity;
    }
}