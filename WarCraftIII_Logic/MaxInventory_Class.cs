using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace WarCraftIII_Logic
{
    [BsonIgnoreExtraElements]
    class MaxInventory_Class
    {
        public string Name { get; set; } = "MaxInventory";
        public string[] MaxInventory { get; set; }
    }
}
