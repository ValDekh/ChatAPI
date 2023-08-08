﻿using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Common
{
    public class BaseEntity
    {
        public ObjectId Id { get; set; }
       
        public ObjectId? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public ObjectId? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
