namespace WarCraftIII_Logic
{
    interface IUnit
    {
        void ManagementStrengthWarrior(char sign);
        void ManagementDexterityWarrior(char sign);
        void ManagementConstitutionWarrior(char sign);
        void ManagementIntelligenceWarrior(char sign);

        void ManagementStrengthRogue(char sign);
        void ManagementDexterityRogue(char sign);
        void ManagementConstitutionRogue(char sign);
        void ManagementIntelligenceRogue(char sign);

        void ManagementStrengthWizard(char sign);
        void ManagementDexterityWizard(char sign);
        void ManagementConstitutionWizard(char sign);
        void ManagementIntelligenceWizard(char sign);

        void UpLvl();
    }
}
