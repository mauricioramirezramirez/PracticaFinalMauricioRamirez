﻿using System;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace PractivaFinalMauricioRamirez.Models
{
	public class PersonalItemModel
	{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        [JsonPropertyName("Name")]
        public string Name { get; set; } = null!;

        [BsonElement("Description")]
        [JsonPropertyName("Description")]
        public string Description { get; set; } = null!;

        [BsonElement("IsCompleted")]
        [JsonPropertyName("IsCompleted")]
        public bool IsCompleted { get; set; } = false!;
    }
}

