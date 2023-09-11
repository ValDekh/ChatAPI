using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Exceptions.ForbiddenException
{
    public class ForbiddenException : Exception
    {
        public int StatusCode { get; }
        public ForbiddenException(HttpStatusCode statusCode, Guid userId) : base($"User with id: {userId} doesn't have permissions")
        {
            StatusCode = (int)statusCode;
        }

        public static ForbiddenException Default(Guid userId)
        {
            return new ForbiddenException(HttpStatusCode.Forbidden, userId);
        }
    }
}
