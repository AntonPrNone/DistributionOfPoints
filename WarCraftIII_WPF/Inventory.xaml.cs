using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        private List<Image> imgsInventory;
        private bool mouseScrolled = false;

        public Inventory()
        {
            InitializeComponent();
            unit = warrior;
            imgsInventory = new List<Image>() { Sword, Bow, MagicStaff, BreastplateBronze, BreastplateIron, BreastplateMythical,
                                       HelmetBronze, HelmetIron, HelmetMythical};
            UpdateData();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            units = new Unit[] { warrior, rogue, wizard };
            ComboBoxUnits.SelectedIndex = 0;
        }

        private void UpdateData()
        {
            foreach (Image imgInventory in imgsInventory) // Inventory
            {
                if (unit.Inventory.Contains(imgInventory.Name)) imgInventory.Visibility = Visibility.Visible;
            }

            foreach (var item in unit.Body) // Body
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

        // ----------------------------------------------- СhangingСharacters ----------------------------------------------

        private void SwitchingUnits(Unit unit)
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

            foreach (Image item in imgsInventory) item.Visibility = Visibility.Hidden;
            Helmet.Visibility = Visibility.Hidden;
            Breastplate.Visibility = Visibility.Hidden;
            Weapon.Visibility = Visibility.Hidden;
            UpdateData();
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

        // --------------------------------------------- Inventory. Body. Land ---------------------------------------------

        private void Inventory_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Image img)
            {
                unit.RemoveInventory(img.Name);
                img.Visibility = Visibility.Hidden;
            }
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
                img.Visibility = Visibility.Hidden;
                foreach (var item in imgsInventory)
                {
                    if (item.Name == nameLoot)
                    {
                        item.Visibility = Visibility.Visible;
                        break;
                    }
                }
            }
        }

        private void Land_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Image img)
            {
                Image obj = imgsInventory[MaxInventory.IndexOf(img.Name.Trim('0'))];
                unit.AddInventory(obj.Name); 
                obj.Visibility = Visibility.Visible; 
            }
        }

        private void Land_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            DisplayingInformation((Image)sender);
        }

        // ---------------------------------------------------- *ButtonBack* -----------------------------------------------

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

        // -----------------------------------------------------------------------------------------------------------------

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            unit.ResetInventory();
            unit.ResetBody();
            foreach (Image item in imgsInventory) item.Visibility = Visibility.Hidden;
            Helmet.Visibility = Visibility.Hidden;
            Breastplate.Visibility = Visibility.Hidden;
            Weapon.Visibility = Visibility.Hidden;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MongoExamples.SaveValues(unit.Name, unit);
        }

        private async void DisplayingInformation(Image x)
        {
            switch (x.Name)
            {
                case "Bow0":
                    InfoTextBox0_0.Visibility = Visibility.Visible;
                    await Task.Delay(2000);
                    InfoTextBox0_0.Visibility = Visibility.Hidden;
                    break;

                case "Sword0":
                    InfoTextBox0_1.Visibility = Visibility.Visible;
                    await Task.Delay(2000);
                    InfoTextBox0_1.Visibility = Visibility.Hidden;
                    break;

                case "MagicStaff0":
                    InfoTextBox0_2.Visibility = Visibility.Visible;
                    await Task.Delay(2000);
                    InfoTextBox0_2.Visibility = Visibility.Hidden;
                    break;

                case "HelmetBronze0":
                    InfoTextBox1_0.Visibility = Visibility.Visible;
                    await Task.Delay(2000);
                    InfoTextBox1_0.Visibility = Visibility.Hidden;
                    break;

                case "HelmetIron0":
                    InfoTextBox1_1.Visibility = Visibility.Visible;
                    await Task.Delay(2000);
                    InfoTextBox1_1.Visibility = Visibility.Hidden;
                    break;

                case "HelmetMythical0":
                    InfoTextBox1_2.Visibility = Visibility.Visible;
                    await Task.Delay(2000);
                    InfoTextBox1_2.Visibility = Visibility.Hidden;
                    break;

                case "BreastplateBronze0":
                    InfoTextBox2_0.Visibility = Visibility.Visible;
                    await Task.Delay(2000);
                    InfoTextBox2_0.Visibility = Visibility.Hidden;
                    break;

                case "BreastplateIron0":
                    InfoTextBox2_1.Visibility = Visibility.Visible;
                    await Task.Delay(2000);
                    InfoTextBox2_1.Visibility = Visibility.Hidden;
                    break;

                case "BreastplateMythical0":
                    InfoTextBox2_2.Visibility = Visibility.Visible;
                    await Task.Delay(2000);
                    InfoTextBox2_2.Visibility = Visibility.Hidden;
                    break;
            }
        }
    }
}
