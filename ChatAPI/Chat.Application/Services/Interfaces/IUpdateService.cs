using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Services.Interfaces
{
    public interface IUpdateService<TEntity, TDTO>
    {
        Task<bool> UpdateAsync();
    }
}
