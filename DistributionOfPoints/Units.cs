using System;
using System.Collections.Generic;
using System.Text;

namespace DistributionOfPoints
{
    class Unit
    {
        // indicators
        public int skillPoints = 100;
        public double maxHP;
        public double maxMP;
        public double PAttack;
        public double MAttack;
        public double PDef;

        // specifications
        public int[] strength;
        public int[] dexterity;
        public int[] constitution;
        public int[] intelligence;

    }

    class Warrior : Unit
    {
        public Warrior()
        {
            maxHP = 260;
            maxMP = 10;
            PAttack = 165;
            MAttack = 10;
            PDef = 55;

            strength = new[] { 30, 30, 250 }; // minimum level; actual level; maximum level
            dexterity = new[] { 15, 15, 70 };
            constitution = new[] { 20, 20, 100 };
            intelligence = new[] { 10, 10, 50 };
        }

        public void ManagementStrength()
        {
            if (strength[1] < strength[2] && skillPoints > 0) // checking the achievement of the limit of skill points and levels of characteristics
            {
                skillPoints--;
                strength[1]++;

                PAttack += 5;
                maxHP += 2;
            }
        }

        public void ManagementDexterity()
        {
            if (dexterity[1] < dexterity[2] && skillPoints > 0)
            {
                skillPoints--;
                dexterity[1]++;

                PAttack++;
                PDef++;
            }
        }

        public void ManagementConstitution()
        {
            if (constitution[1] < constitution[2] && skillPoints > 0)
            {
                skillPoints--;
                constitution[1]++;

                maxHP += 10;
                PDef += 2;
            }
        }

        public void ManagementIntelligence()
        {
            if (intelligence[1] < intelligence[2] && skillPoints > 0)
            {
                skillPoints--;
                intelligence[1]++;

                maxMP++;
                MAttack++;
            }
        }
    }

    class Rogue : Unit
    {
        public Rogue()
        {
            maxHP = 135;
            maxMP = 22.5;
            PAttack = 150;
            MAttack = 30;
            PDef = 45;

            strength = new[] { 15, 15, 55 };
            dexterity = new[] { 30, 30, 250 };
            constitution = new[] { 20, 20, 80 };
            intelligence = new[] { 15, 15, 70 };
        }

        public void ManagementStrength()
        {
            if (strength[1] < strength[2] && skillPoints > 0)
            {
                skillPoints--;
                strength[1]++;

                PAttack += 2;
                maxHP++;
            }
        }

        public void ManagementDexterity()
        {
            if (dexterity[1] < dexterity[2] && skillPoints > 0)
            {
                skillPoints--;
                dexterity[1]++;

                PAttack += 4;
                PDef += 1.5;
            }
        }

        public void ManagementConstitution()
        {
            if (constitution[1] < constitution[2] && skillPoints > 0)
            {
                skillPoints--;
                constitution[1]++;

                maxHP += 6;
            }
        }

        public void ManagementIntelligence()
        {
            if (intelligence[1] < intelligence[2] && skillPoints > 0)
            {
                skillPoints--;
                intelligence[1]++;

                maxMP += 1.5;
                MAttack += 2;
            }
        }
    }

    class Wizard : Unit
    {
        public Wizard()
        {
            maxHP = 55;
            maxMP = 70;
            PAttack = 30;
            MAttack = 175;
            PDef = 25;

            strength = new[] { 10, 10, 45 };
            dexterity = new[] { 20, 20, 70 };
            constitution = new[] { 15, 15, 60 };
            intelligence = new[] { 35, 35, 250 };
        }

        public void ManagementStrength()
        {
            if (strength[1] < strength[2] && skillPoints > 0)
            {
                skillPoints--;
                strength[1]++;

                PAttack += 3;
                maxHP++;
            }
        }

        public void ManagementDexterity()
        {
            if (dexterity[1] < dexterity[2] && skillPoints > 0)
            {
                skillPoints--;
                dexterity[1]++;

                PDef += 0.5;
            }
        }

        public void ManagementConstitution()
        {
            if (constitution[1] < constitution[2] && skillPoints > 0)
            {
                skillPoints--;
                constitution[1]++;

                maxHP += 3;
                PDef++;
            }
        }

        public void ManagementIntelligence()
        {
            if (intelligence[1] < intelligence[2] && skillPoints > 0)
            {
                skillPoints--;
                intelligence[1]++;

                maxMP += 2;
                MAttack += 5;
            }
        }
    }
}
