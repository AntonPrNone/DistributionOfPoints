using System.Collections.Generic;

namespace WarCraftIII_Logic
{
    interface IUnit
    {
        int Exp { get; set; }
        void UpLvl();
        // ---------------------------------------------------- WARRIOR ----------------------------------------------------

        void ManagementStrengthWarrior(char sign);
        void ManagementDexterityWarrior(char sign);
        void ManagementConstitutionWarrior(char sign);
        void ManagementIntelligenceWarrior(char sign);

        // ----------------------------------------------------- ROGUE -----------------------------------------------------

        void ManagementStrengthRogue(char sign);
        void ManagementDexterityRogue(char sign);
        void ManagementConstitutionRogue(char sign);
        void ManagementIntelligenceRogue(char sign);

        // ----------------------------------------------------- WIZARD ----------------------------------------------------

        void ManagementStrengthWizard(char sign);
        void ManagementDexterityWizard(char sign);
        void ManagementConstitutionWizard(char sign);
        void ManagementIntelligenceWizard(char sign);

        // -----------------------------------------------------------------------------------------------------------------
        // -------------------------------------------------- *INVENTORY* --------------------------------------------------

        void AddInventory(string loot);
        void RemoveInventory(string loot);
        void ResetInventory();
        void EditInventory(List<string> inv);

        // ----------------------------------------------------- *BODY* ----------------------------------------------------

        void AddBody(string loot);
        string RemoveBody(string loot);
        void ResetBody();
        void RecoverBody();
        void EditBody(List<string> body);

        // ----------------------------------------------- ^BUFF^ / ʌDEBUFFʌ -----------------------------------------------

        void DistributionBuff(string loot, char sign);
        void BuffDebuffBow(char sign);
        void BuffDebuffSword(char sign);
        void BuffDebuffMagicStaff(char sign);
        void BuffDebuffHelmetBronze(char sign);
        void BuffDebuffHelmetIron(char sign);
        void BuffDebuffHelmetMythical(char sign);
        void BuffDebuffBreastplateBronze(char sign);
        void BuffDebuffBreastplateIron(char sign);
        void BuffDebuffBreastplateMythical(char sign);
    }
}
