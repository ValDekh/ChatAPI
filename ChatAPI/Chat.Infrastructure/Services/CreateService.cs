using AutoMapper;
using Chat.Application.Services.Interfaces;
using Chat.Domain.Entities;
using Chat.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Services
{
    public class CreateService<TEntity, TDTO> : ICreateService<TEntity, TDTO>
        where TEntity : BaseEntity
        where TDTO : class
    {
        private readonly IMapper _mapper;
        private readonly IRepository<TEntity> _repository;
        private TDTO _gotDTO { get; set; }
        public CreateService(IMapper mapper, IRepository<TEntity> repository, TDTO gotDTO)
        {
            _mapper= mapper;
            _repository= repository;
            _gotDTO= gotDTO;    
        }

        public async Task<TEntity> CreateAsync()
        {
            var newEntity = _mapper.Map<TEntity>(_gotDTO);
            await _repository.AddAsync(newEntity);
            return newEntity;
        }
    }
}

