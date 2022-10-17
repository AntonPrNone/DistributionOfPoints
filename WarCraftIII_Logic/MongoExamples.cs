using MongoDB.Driver;
using System.Linq;

namespace WarCraftIII_Logic
{
    public class MongoExamples
    {
        public static Unit Find(string name) // Returns a document
        {
            var client = new MongoClient();
            var database = client.GetDatabase("DB");
            var collection = database.GetCollection<Unit>("Units");
            var document = collection.Find(x => x.Name == name).FirstOrDefault();
            return document;
        }

        public static string[] FindMaxInventory() // Returns a document
        {
            var client = new MongoClient();
            var database = client.GetDatabase("DB");
            var collection = database.GetCollection<MaxInventory_Class>("Units");
            var document = collection.Find(x => x.Name == "MaxInventory").FirstOrDefault();
            return document.MaxInventory;
        }

        public static void SaveValues(string name, Unit unit) // Replaces the document
        {
            var client = new MongoClient();
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
