﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.DTOs
{
    public class ChatDTO
    {
        [BsonId]
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        public List<ObjectId> Users { get; set; }
        public List<MessageDTO> Messages { get; set; }
    }
}
