using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace WarCraftIII_Logic
{
    [BsonIgnoreExtraElements]
    public class Unit : IUnit
    {
        // Indicators
        public string Name { get; internal set; }

        public int SkillPoints { get; private set; }
        public int SkillPointsMax { get; private set; }
        public int MaxEx { get; private set; }
        private int Ex;
        public int Exp
        {
            get => Ex;
            set
            {
                Ex = value;
                if (MaxEx == Ex) UpLvl();
            }
        }
        public int Lvl { get; private set; }
        public double MaxHP { get; private set; }
        public double MaxMP { get; private set; }
        public double PAttack { get; private set; }
        public double MAttack { get; private set; }
        public double PDef { get; private set; }

        // Specifications
        public int[] Strength { get; private set; }  // minimum level; actual level; maximum level
        public int[] Dexterity { get; private set; }
        public int[] Constitution { get; private set; }
        public int[] Intelligence { get; private set; }

        public List<string> Inventory { get; private set; }

        public Unit(string name, int skillPoints, int skillPointsMax, int ex, int maxEx, double maxHP, double maxMP,
                    double pAttack, double mAttack, double pDef, int[] strength, int[] dexterity, int[] constitution,
                    int[] intelligence) // Manual creation
        {
            Name = name;
            SkillPoints = skillPoints;
            SkillPointsMax = skillPointsMax;
            Ex = ex;
            MaxEx = maxEx;
            MaxHP = maxHP;
            MaxMP = maxMP;
            PAttack = pAttack;
            MAttack = mAttack;
            PDef = pDef;

            Strength = strength;
            Dexterity = dexterity;
            Constitution = constitution;
            Intelligence = intelligence;
        }

        public void UpLvl()
        {
            Ex = 0;
            MaxEx *= 2;
            Lvl++;
        }

        // ---------------------------------------------------- WARRIOR ----------------------------------------------------

        public void ManagementStrengthWarrior(char sign)
        {
            if (sign == '+' && (Strength[1] < Strength[2] && SkillPoints > 0))
            {
                SkillPoints--;
                Strength[1]++;

                PAttack += 5;
                MaxHP += 2;
            }

            else if (sign == '-' && (Strength[0] < Strength[1] && SkillPoints < SkillPointsMax))
            {
                SkillPoints++;
                Strength[1]--;

                PAttack -= 5;
                MaxHP -= 2;
            }
        }

        public void ManagementDexterityWarrior(char sign)
        {
            if (sign == '+' && (Dexterity[1] < Dexterity[2] && SkillPoints > 0))
            {
                SkillPoints--;
                Dexterity[1]++;

                PAttack++;
                PDef++;
            }

            else if (sign == '-' && (Dexterity[0] < Dexterity[1] && SkillPoints < SkillPointsMax))
            {
                SkillPoints++;
                Dexterity[1]--;

                PAttack--;
                PDef--;
            }
        }

        public void ManagementConstitutionWarrior(char sign)
        {
            if (sign == '+' && (Constitution[1] < Constitution[2] && SkillPoints > 0))
            {
                SkillPoints--;
                Constitution[1]++;

                MaxHP += 10;
                PDef += 2;
            }

            else if (sign == '-' && (Constitution[0] < Constitution[1] && SkillPoints < SkillPointsMax))
            {
                SkillPoints++;
                Constitution[1]--;

                MaxHP -= 10;
                PDef -= 2;
            }
        }

        public void ManagementIntelligenceWarrior(char sign)
        {
            if (sign == '+' && (Intelligence[1] < Intelligence[2] && SkillPoints > 0))
            {
                SkillPoints--;
                Intelligence[1]++;

                MaxMP++;
                MAttack++;
            }

            else if (sign == '-' && (Intelligence[0] < Intelligence[1] && SkillPoints < SkillPointsMax))
            {
                SkillPoints++;
                Intelligence[1]--;

                MaxMP--;
                MAttack--;
            }
        }

        // ---------------------------------------------------- ROGUE ----------------------------------------------------

        public void ManagementStrengthRogue(char sign)
        {
            if (sign == '+' && (Strength[1] < Strength[2] && SkillPoints > 0))
            {
                SkillPoints--;
                Strength[1]++;

                PAttack += 2;
                MaxHP++;
            }

            else if (sign == '-' && (Strength[0] < Strength[1] && SkillPoints < SkillPointsMax))
            {
                SkillPoints++;
                Strength[1]--;

                PAttack -= 2;
                MaxHP--;
            }
        }

        public void ManagementDexterityRogue(char sign)
        {
            if (sign == '+' && (Dexterity[1] < Dexterity[2] && SkillPoints > 0))
            {
                SkillPoints--;
                Dexterity[1]++;

                PAttack += 4;
                PDef += 1.5;
            }

            else if (sign == '-' && (Dexterity[0] < Dexterity[1] && SkillPoints < SkillPointsMax))
            {
                SkillPoints++;
                Dexterity[1]--;

                PAttack -= 4;
                PDef -= 1.5;
            }
        }

        public void ManagementConstitutionRogue(char sign)
        {
            if (sign == '+' && (Constitution[1] < Constitution[2] && SkillPoints > 0))
            {
                SkillPoints--;
                Constitution[1]++;

                MaxHP += 6;
            }

            else if (sign == '-' && (Constitution[0] < Constitution[1] && SkillPoints < SkillPointsMax))
            {
                SkillPoints++;
                Constitution[1]--;

                MaxHP -= 6;
            }
        }

        public void ManagementIntelligenceRogue(char sign)
        {
            if (sign == '+' && (Intelligence[1] < Intelligence[2] && SkillPoints > 0))
            {
                SkillPoints--;
                Intelligence[1]++;

                MaxMP += 1.5;
                MAttack += 2;
            }

            else if (sign == '-' && (Intelligence[0] < Intelligence[1] && SkillPoints < SkillPointsMax))
            {
                SkillPoints++;
                Intelligence[1]--;

                MaxMP -= 1.5;
                MAttack -= 2;
            }
        }

        // ---------------------------------------------------- WIZARD ----------------------------------------------------

        public void ManagementStrengthWizard(char sign)
        {
            if (sign == '+' && (Strength[1] < Strength[2] && SkillPoints > 0))
            {
                SkillPoints--;
                Strength[1]++;

                PAttack += 3;
                MaxHP++;
            }

            else if (sign == '-' && (Strength[0] < Strength[1] && SkillPoints < SkillPointsMax))
            {
                SkillPoints++;
                Strength[1]--;

                PAttack -= 3;
                MaxHP--;
            }
        }

        public void ManagementDexterityWizard(char sign)
        {
            if (sign == '+' && (Dexterity[1] < Dexterity[2] && SkillPoints > 0))
            {
                SkillPoints--;
                Dexterity[1]++;

                PDef += 0.5;
            }

            else if (sign == '-' && (Dexterity[0] < Dexterity[1] && SkillPoints < SkillPointsMax))
            {
                SkillPoints++;
                Dexterity[1]--;

                PDef -= 0.5;
            }
        }

        public void ManagementConstitutionWizard(char sign)
        {
            if (sign == '+' && (Constitution[1] < Constitution[2] && SkillPoints > 0))
            {
                SkillPoints--;
                Constitution[1]++;

                MaxHP += 3;
                PDef++;
            }

            else if (sign == '-' && (Constitution[0] < Constitution[1] && SkillPoints < SkillPointsMax))
            {
                SkillPoints++;
                Constitution[1]--;

                MaxHP -= 3;
                PDef--;
            }
        }

        public void ManagementIntelligenceWizard(char sign)
        {
            if (sign == '+' && (Intelligence[1] < Intelligence[2] && SkillPoints > 0))
            {
                SkillPoints--;
                Intelligence[1]++;

                MaxMP += 2;
                MAttack += 5;
            }

            else if (sign == '-' && (Intelligence[0] < Intelligence[1] && SkillPoints < SkillPointsMax))
            {
                SkillPoints++;
                Intelligence[1]--;

                MaxMP -= 2;
                MAttack -= 5;
            }
        }

        // ---------------------------------------------------- *INVENTORY* ----------------------------------------------------

        public void AddInventory(string loot)
        {
            Inventory.Add(loot);
        }

        public void RemoveInventory(string loot)
        {
            Inventory.Remove(loot);
        }
    }
}
