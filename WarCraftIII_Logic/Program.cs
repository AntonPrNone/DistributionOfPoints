namespace WarCraftIII_Logic
{
    class Program
    {
        static void Main()
        {
            MongoExamples.SaveValues("WizardDefaultValue", MongoExamples.Find("Wizard"));
        }
    }
}
