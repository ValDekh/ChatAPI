using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Services.Interfaces
{
    public interface IDeleteService<TEntity, TDTO>
    {
        Task<StatusCodeResult> DeleteAsync();
    }
}
