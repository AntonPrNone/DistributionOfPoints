using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
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
            imgsInventory = new List<Image>() { WeaponSword, WeaponBow, WeaponMagicStaff, BreastplateBronze, BreastplateIron, BreastplateMythical,
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

            foreach (string item in unit.Body) // Body
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

                else if (item.Contains("Weapon"))
                {
                    Weapon.Source = new BitmapImage(new Uri($"/img/loot/{item}.png", UriKind.Relative));
                    Weapon.Visibility = Visibility.Visible;
                }
            }
        }

        // ----------------------------------------------- СhangingСharacters ----------------------------------------------

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

            foreach (Image item in imgsInventory) item.Visibility = Visibility.Hidden;
            Helmet.Visibility = Visibility.Hidden;
            Breastplate.Visibility = Visibility.Hidden;
            Weapon.Visibility = Visibility.Hidden;
            UpdateData();
        }

        private void ComboBoxUnits_SelectionChanged(object sender, SelectionChangedEventArgs e) // Changing a character with ComboBox
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

        private void UnitImg_MouseWheel(object sender, MouseWheelEventArgs e) // Changing a character with Scroll
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
                    unit.RemoveInventory(img.Name);
                }

                else if (Helmet.Visibility == Visibility.Hidden && img.Name.Contains(Helmet.Name))
                {
                    Helmet.Source = img.Source;
                    Helmet.Visibility = Visibility.Visible;
                    img.Visibility = Visibility.Hidden;
                    unit.AddBody(img.Name);
                    unit.RemoveInventory(img.Name);
                }

                else if (Weapon.Visibility == Visibility.Hidden && img.Name.Contains(Weapon.Name))
                {
                    Weapon.Source = img.Source;
                    Weapon.Visibility = Visibility.Visible;
                    img.Visibility = Visibility.Hidden;
                    unit.AddBody(img.Name);
                    unit.RemoveInventory(img.Name);
                }
            }
        }

        private void Body_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Image img)
            {
                string nameLoot = unit.RemoveBody(img.Name);
                img.Visibility = Visibility.Hidden;
                foreach (Image item in imgsInventory)
                {
                    if (item.Name == nameLoot)
                    {
                        item.Visibility = Visibility.Visible;
                        break;
                    }
                }
                Anim.AnimFlashingLabel(InventoryLabel, unit);
            }
        }

        private void Land_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Image img)
            {
                Image obj = imgsInventory[MaxInventory.IndexOf(img.Name.Trim('0'))];
                if (unit.AddInventory(obj.Name))
                {
                    Anim.AnimElementMove((Image)sender, obj);
                    Anim.AnimFlashingLabel(InventoryLabel, unit);
                }
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
            Anim.AnimElementSize_MouseEnter((Image)sender);
        }

        private void ButtonBack_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonBack.Source = new BitmapImage(new Uri("/img/iconBack.png", UriKind.Relative));
            Anim.AnimElementSize_MouseLeave((Image)sender);
        }

        // ------------------------------------------------------ Anim -----------------------------------------------------

        private void DisplayingInformation(Image x) // Information about the subject
        {
            switch (x.Name)
            {
                case "WeaponBow0":
                    Anim.AnimElementVisibility(InfoTextBox0_0);
                    break;

                case "WeaponSword0":
                    Anim.AnimElementVisibility(InfoTextBox0_1);
                    break;

                case "WeaponMagicStaff0":
                    Anim.AnimElementVisibility(InfoTextBox0_2);
                    break;

                case "HelmetBronze0":
                    Anim.AnimElementVisibility(InfoTextBox1_0);
                    break;

                case "HelmetIron0":
                    Anim.AnimElementVisibility(InfoTextBox1_1);
                    break;

                case "HelmetMythical0":
                    Anim.AnimElementVisibility(InfoTextBox1_2);
                    break;

                case "BreastplateBronze0":
                    Anim.AnimElementVisibility(InfoTextBox2_0);
                    break;

                case "BreastplateIron0":
                    Anim.AnimElementVisibility(InfoTextBox2_1);
                    break;

                case "BreastplateMythical0":
                    Anim.AnimElementVisibility(InfoTextBox2_2);
                    break;

                default:
                    break;
            }
        }

        private void AnimButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Anim.AnimElementSize_MouseEnter((Button)sender);
        }

        private void AnimButton_MouseLeave(object sender, MouseEventArgs e)
        {
            Anim.AnimElementSize_MouseLeave((Button)sender);
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
    }
}
