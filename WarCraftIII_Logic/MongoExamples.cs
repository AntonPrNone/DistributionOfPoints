using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace WarCraftIII_Logic
{
    public class MongoExamples
    {
        private const string ConnectStringLocal = "mongodb://localhost:27017";
        private const string ConnectStringAtlas = "mongodb+srv://AntonPr:<password>@cluster0.o9nqv6x.mongodb.net";
        private static string ConnectString = ConnectStringLocal;

        public static Unit Find(string name) // Returns a document
        {
            var client = new MongoClient(ConnectString);
            var database = client.GetDatabase("DB");
            var collection = database.GetCollection<Unit>("Units");
            var document = collection.Find(x => x.Name == name).FirstOrDefault();
            return document;
        }

        public static List<string> FindMaxInventory() // Returns a document
        {
            var client = new MongoClient(ConnectString);
            var database = client.GetDatabase("DB");
            var collection = database.GetCollection<MaxInventory_Class>("Units");
            var document = collection.Find(x => x.Name == "MaxInventory").FirstOrDefault();
            return document.MaxInventory.ToList();
        }

        public static void SaveValues(string name, Unit unit) // Replaces the document
        {
            var client = new MongoClient(ConnectString);
            var database = client.GetDatabase("DB");
            var collection = database.GetCollection<Unit>("Units");
            collection.ReplaceOne(x => x.Name == name, unit);
        }

        public static void ResetValues(string name) // Resets document values
        {
            Unit DefaultValue = Find(name + "DefaultValue");
            DefaultValue.Name = name;
            SaveValues(name, DefaultValue);
        }
    }
}
