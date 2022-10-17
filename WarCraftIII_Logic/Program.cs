namespace WarCraftIII_Logic
{
    class Program
    {
        static void Main()
        {
            MongoExamples.SaveValues("WizardDefaultValue", MongoExamples.Find("Wizard"));
            MongoExamples.SaveValues("RogueDefaultValue", MongoExamples.Find("Rogue"));
            MongoExamples.SaveValues("WarriorDefaultValue", MongoExamples.Find("Warrior"));
        }
    }
}
