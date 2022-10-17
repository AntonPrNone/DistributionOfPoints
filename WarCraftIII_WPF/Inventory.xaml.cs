using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WarCraftIII_Logic;

namespace WarCraftIII_WPF
{
    /// <summary>
    /// Логика взаимодействия для Inventory.xaml
    /// </summary>
    public partial class Inventory : Window
    {
        private Unit unit;
        private Unit[] units;
        private Unit warrior = MongoExamples.Find("Warrior");
        private Unit rogue = MongoExamples.Find("Rogue");
        private Unit wizard = MongoExamples.Find("Wizard");
        private string[] MaxInventory = MongoExamples.FindMaxInventory();

        public Inventory()
        {
            InitializeComponent();
            units = new Unit[] { warrior, rogue, wizard };
            unit = warrior;

            if (unit.Inventory.Contains("Bow")) Bow.Visibility = Visibility.Visible;
            if (unit.Inventory.Contains("Sword")) Sword.Visibility = Visibility.Visible;
            if (unit.Inventory.Contains("MagicStaff")) MagicStaff.Visibility = Visibility.Visible;
            if (unit.Inventory.Contains("HelmetBronze")) HelmetBronze.Visibility = Visibility.Visible;
            if (unit.Inventory.Contains("HelmetIron")) HelmetIron.Visibility = Visibility.Visible;
            if (unit.Inventory.Contains("HelmetMythical")) HelmetMythical.Visibility = Visibility.Visible;
            if (unit.Inventory.Contains("BreastplateBronze")) BreastplateBronze.Visibility = Visibility.Visible;
            if (unit.Inventory.Contains("BreastplateIron")) BreastplateIron.Visibility = Visibility.Visible;
            if (unit.Inventory.Contains("BreastplateMythical")) BreastplateMythical.Visibility = Visibility.Visible;
        }

        private void LootInventoryImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Image img)
            {
                unit.RemoveInventory(img.Name);
                img.Visibility = Visibility.Hidden;
            }
        }

        private void LootlandImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Image img)
            {
                unit.AddInventory(img.Name);
                img.Visibility = Visibility.Visible;
                
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MongoExamples.SaveValues(unit.Name, unit);
        }
    }
}
