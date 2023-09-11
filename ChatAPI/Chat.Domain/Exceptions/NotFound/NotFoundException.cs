using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Exceptions.NotFound
{
    public abstract class NotFoundException : Exception
    {
        protected NotFoundException(string message)
            : base(message)
        {
        }

        public int StatusCode { get; } = 404;

        public override string Message
        {
            get
            {
                return $"Status Code: {StatusCode}, {base.Message}";
            }
        }
    }
}
