using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Services.Converters
{
   public static class ObjectIdGuidConverter
    {
        public static Guid ConvertObjectIdToGuid(ObjectId objectId)
        {
            byte[] guidBytes = objectId.ToByteArray().Concat(new byte[4]).ToArray();
            return new Guid(guidBytes);
        }

        public static ObjectId ConvertGuidToObjectId(Guid guid)
        {
            byte[] guidBytes = guid.ToByteArray();
            byte[] objectIdBytes = guidBytes.Take(12).ToArray();
            return new ObjectId(objectIdBytes);
        }
    }
}
