using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
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
        private List<string> MaxInventory = MongoExamples.FindMaxInventory();
        private List<Image> imgs;
        private bool mouseScrolled = false;

        public Inventory()
        {
            InitializeComponent();
            units = new Unit[] { warrior, rogue, wizard };
            unit = warrior;
            ComboBoxUnits.SelectedIndex = 0;

            UpdateData();

            imgs = new List<Image>() { Sword, Bow, MagicStaff, BreastplateBronze, BreastplateIron, BreastplateMythical,
                                       HelmetBronze, HelmetIron, HelmetMythical};
        }

        private void Inventory_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Image img)
            {
                unit.RemoveInventory(img.Name);
                img.Visibility = Visibility.Hidden;
            }
        }

        private void Land_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Image img)
            {
                var obj = imgs[MaxInventory.IndexOf(img.Name.Trim('0'))];
                unit.AddInventory(obj.Name); 
                obj.Visibility = Visibility.Visible; 
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MongoExamples.SaveValues(unit.Name, unit);
        }

        private void ComboBoxUnits_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!mouseScrolled)
            {
                if (ComboBoxUnits.SelectedIndex == 0 && unit.Name != warrior.Name)
                {
                    SwitchingUnits(warrior);
                }

                if (ComboBoxUnits.SelectedIndex == 1 && unit.Name != rogue.Name)
                {
                    SwitchingUnits(rogue);
                }

                else if (ComboBoxUnits.SelectedIndex == 2 && unit.Name != wizard.Name)
                {
                    SwitchingUnits(wizard);
                }
            }
        }

        private void UnitImg_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (Array.IndexOf(units, unit) == units.Length - 1)
                {
                    mouseScrolled = true;
                    ComboBoxUnits.SelectedIndex = 0;
                    mouseScrolled = false;

                    SwitchingUnits(units[0]);
                }

                else
                {
                    mouseScrolled = true;
                    ComboBoxUnits.SelectedIndex = Array.IndexOf(units, unit) + 1;
                    mouseScrolled = false;

                    SwitchingUnits(units[Array.IndexOf(units, unit) + 1]);
                }
            }

            else if (e.Delta < 0)
            {
                if (Array.IndexOf(units, unit) == 0)
                {
                    mouseScrolled = true;
                    ComboBoxUnits.SelectedIndex = units.Length - 1;
                    mouseScrolled = false;

                    SwitchingUnits(units[^1]);
                }

                else
                {
                    mouseScrolled = true;
                    ComboBoxUnits.SelectedIndex = Array.IndexOf(units, unit) - 1;
                    mouseScrolled = false;

                    SwitchingUnits(units[Array.IndexOf(units, unit) - 1]);
                }
            }
        }

        private void SwitchingUnits(Unit unit) // Changing a character
        {
            MongoExamples.SaveValues(this.unit.Name, this.unit);
            this.unit = unit;

            if (unit.Name == "Warrior")
            {
                UnitImg.Source = new BitmapImage(new Uri("/img/Warrior.png", UriKind.Relative));
                Application.Current.Resources["DefoultColor"] = Application.Current.Resources["WarriorColor"];
            }

            if (unit.Name == "Rogue")
            {
                UnitImg.Source = new BitmapImage(new Uri("/img/Rogue.png", UriKind.Relative));
                Application.Current.Resources["DefoultColor"] = Application.Current.Resources["RogueColor"];
            }

            if (unit.Name == "Wizard")
            {
                UnitImg.Source = new BitmapImage(new Uri("/img/Wizard.png", UriKind.Relative));
                Application.Current.Resources["DefoultColor"] = Application.Current.Resources["WizardColor"];
            }

            foreach (Image item in imgs) item.Visibility = Visibility.Hidden;
            Helmet.Visibility = Visibility.Hidden;
            Breastplate.Visibility = Visibility.Hidden;
            Weapon.Visibility = Visibility.Hidden;
            UpdateData();
        }

        private void UpdateData()
        {
            if (unit.Inventory.Contains("Bow")) Bow.Visibility = Visibility.Visible;
            if (unit.Inventory.Contains("Sword")) Sword.Visibility = Visibility.Visible;
            if (unit.Inventory.Contains("MagicStaff")) MagicStaff.Visibility = Visibility.Visible;
            if (unit.Inventory.Contains("HelmetBronze")) HelmetBronze.Visibility = Visibility.Visible;
            if (unit.Inventory.Contains("HelmetIron")) HelmetIron.Visibility = Visibility.Visible;
            if (unit.Inventory.Contains("HelmetMythical")) HelmetMythical.Visibility = Visibility.Visible;
            if (unit.Inventory.Contains("BreastplateBronze")) BreastplateBronze.Visibility = Visibility.Visible;
            if (unit.Inventory.Contains("BreastplateIron")) BreastplateIron.Visibility = Visibility.Visible;
            if (unit.Inventory.Contains("BreastplateMythical")) BreastplateMythical.Visibility = Visibility.Visible;
            foreach (var item in unit.Body)
            {
                if (item.Contains("Helmet"))
                {
                    Helmet.Source = new BitmapImage(new Uri($"/img/loot/{item}.png", UriKind.Relative));
                    Helmet.Visibility = Visibility.Visible;
                }

                else if (item.Contains("Breastplate"))
                {
                    Breastplate.Source = new BitmapImage(new Uri($"/img/loot/{item}.png", UriKind.Relative));
                    Breastplate.Visibility = Visibility.Visible;
                }

                else
                {
                    Weapon.Source = new BitmapImage(new Uri($"/img/loot/{item}.png", UriKind.Relative));
                    Weapon.Visibility = Visibility.Visible;
                }
            }
        }

        private void ButtonBack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            new StartWindow().Show();
            Close();
        }

        private void ButtonBack_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonBack.Source = new BitmapImage(new Uri("/img/iconBack2.png", UriKind.Relative));
        }

        private void ButtonBack_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonBack.Source = new BitmapImage(new Uri("/img/iconBack.png", UriKind.Relative));
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            unit.ResetInventory();
            foreach (Image item in imgs) item.Visibility = Visibility.Hidden;
        }

        private void Inventory_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Image img)
            {
                if (Breastplate.Visibility == Visibility.Hidden && img.Name.Contains(Breastplate.Name))
                {
                    Breastplate.Source = img.Source;
                    Breastplate.Visibility = Visibility.Visible;
                    img.Visibility = Visibility.Hidden;
                    unit.AddBody(img.Name);
                }

                else if (Helmet.Visibility == Visibility.Hidden && img.Name.Contains(Helmet.Name))
                {
                    Helmet.Source = img.Source;
                    Helmet.Visibility = Visibility.Visible;
                    img.Visibility = Visibility.Hidden;
                    unit.AddBody(img.Name);
                }

                else if (Weapon.Visibility == Visibility.Hidden)
                {
                    Weapon.Source = img.Source;
                    Weapon.Visibility = Visibility.Visible;
                    img.Visibility = Visibility.Hidden;
                    unit.AddBody(img.Name);
                }
            }
        }

        private void Body_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Image img)
            {
                var nameLoot = unit.RemoveBody(img.Name);
                foreach (var item in imgs)
                {
                    if (item.Name == nameLoot)
                    {
                        item.Visibility = Visibility.Visible;
                        img.Visibility = Visibility.Hidden;
                        unit.AddInventory(item.Name);
                        break;
                    }
                }
            }
        }
    }
}
