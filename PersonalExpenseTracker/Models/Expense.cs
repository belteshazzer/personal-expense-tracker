using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace PersonalExpenseTracker.Models
{
    public class Expense{

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id {get; set;}

        [BsonElement("date")]
        public DateTime date {get; set;}

        [BsonElement("category")]
        public string category {get; set;}

        [BsonElement("amount")]
        public decimal amount {get; set;}

        [BsonElement("description")]
        public string description {get; set;}
    }
}