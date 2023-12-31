﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Structures
{
    public static class Permissions
    {
        public const string ReadMessage = "get:message";
        public const string CreateMessage = "create:message";
        public const string UpdateMessage = "update:message";
        public const string DeleteMessage = "delete:message";
        public const string ReadContributor = "get:contributor";
        public const string AddContributor = "create:contributor";
        public const string UpdateContributor = "update:contributor";
        public const string DeleteContributor = "delete:contributor";
        public static List<string> Admin = new(new[]
        {
            ReadMessage,
            CreateMessage,
            UpdateMessage,
            DeleteMessage,
            ReadContributor,
            AddContributor,
            UpdateContributor,
            DeleteContributor
        });
    }
}
