using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace PersonalExpenseTracker.Models
{
    public class Category{

        [BsonElement("_id")]
        public string id {get; set;}

        [BsonElement("name")]
        public string name {get; set;}

        [BsonElement("iconLink")]
        public string iconLink {get; set;}
    }
}

