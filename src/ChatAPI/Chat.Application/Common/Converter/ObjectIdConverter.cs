using AutoMapper;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Common.Converter
{
    public class ObjectIdConverter
    {
        public ObjectId Convert(string source, ObjectId destination, ResolutionContext context)
        {
            ObjectId obkectId = ObjectId.Parse(source);
            return obkectId;
        }
    }
}
