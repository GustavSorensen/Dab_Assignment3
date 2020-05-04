﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Dab_Social_Network.Models
{
    public class Comment : Model
    {
        public string Content { get; set; }
        public DateTime TimeCreated { get; set; }
        public string UserId { get; set; }
        public string PostId { get; set; }
    }
}
