using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using DistributionOfPoints_Console;

namespace DistributionOfPoints
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class DistributionOfPointsWindow : Window
    {
        private Unit unit;
        private Unit[] units;
        private Unit warrior = MongoExamples.Find("Warrior");
        private Unit rogue = MongoExamples.Find("Rogue");
        private Unit wizard = MongoExamples.Find("Wizard");

        private bool mouseScrolled = false;

        public DistributionOfPointsWindow()
        {
            InitializeComponent();
            units = new Unit[] { warrior, rogue, wizard };
            unit = warrior;
            Application.Current.Resources["DefoultColor"] = Application.Current.Resources["WarriorColor"];
            ComboBoxUnits.SelectedIndex = 0;

            UpdateData();
        }

        private void UpdateData() // Updating the display of data
        {
            SkillPointsTextBox.Text = unit.skillPoints.ToString();

            MaxHPLabel.Content = unit.maxHP;
            MaxMPLabel.Content = unit.maxMP;
            PAttackLabel.Content = unit.PAttack;
            MAttackLabel.Content = unit.MAttack;
            PDefLabel.Content = unit.PDef;

            StrengthTextBox.Text = unit.strength[1].ToString();
            DexterityTextBox.Text = unit.dexterity[1].ToString();
            ConstitutionTextBox.Text = unit.constitution[1].ToString();
            IntelligenceTextBox.Text = unit.intelligence[1].ToString();
        }

        private void CheckingLimit(Unit unit, int[] characteristic, Button buttonplus, Button buttonminus) // Checking the achievement of the limit of skill points and levels of characteristics
        {
            if (unit.skillPoints == 0)
            {
                MessageBox.Show("Skill points are over", "The limit has been reached",
         MessageBoxButton.OK, MessageBoxImage.Information);
            }

            else if (characteristic[1] >= characteristic[2])
            {
                MessageBox.Show("The maximum characteristic level has been reached", "The limit has been reached",
         MessageBoxButton.OK, MessageBoxImage.Information);
                buttonplus.Foreground = System.Windows.Media.Brushes.Black;
            }

            else if (characteristic[0] >= characteristic[1])
            {
                MessageBox.Show("The minimum characteristic level has been reached", "The limit has been reached",
         MessageBoxButton.OK, MessageBoxImage.Information);
                buttonminus.Foreground = System.Windows.Media.Brushes.Black;
            }

            else
            {
                buttonplus.Foreground = System.Windows.Media.Brushes.White;
                buttonminus.Foreground = System.Windows.Media.Brushes.White;
            }
        }

        // -------------------------------------- MANAGING CHARACTERISTICS --------------------------------------

        private void AddStrength_Button_Click(object sender, RoutedEventArgs e)
        {
            unit.ManagementStrengthWarrior('+');
            UpdateData();
            CheckingLimit(unit, unit.strength, AddStrength_Button, ReduceStrength_Button);
        }

        private void AddDexterity_Button_Click(object sender, RoutedEventArgs e)
        {
            unit.ManagementDexterityWarrior('+');
            UpdateData();
            CheckingLimit(unit, unit.dexterity, AddDexterity_Button, ReduceDexterity_Button);
        }

        private void AddConstitution_Button_Click(object sender, RoutedEventArgs e)
        {
            unit.ManagementConstitutionWarrior('+');
            UpdateData();
            CheckingLimit(unit, unit.constitution, AddConstitution_Button, ReduceConstitution_Button);
        }

        private void AddIntelligence_Button_Click(object sender, RoutedEventArgs e)
        {
            unit.ManagementIntelligenceWarrior('+');
            UpdateData();
            CheckingLimit(unit, unit.intelligence, AddIntelligence_Button, ReduceIntelligence_Button);
        }

        private void ReduceStrength_Button_Click(object sender, RoutedEventArgs e)
        {
            unit.ManagementStrengthWarrior('-');
            UpdateData();
            CheckingLimit(unit, unit.strength, AddStrength_Button, ReduceStrength_Button);
        }

        private void ReduceDexterity_Button_Click(object sender, RoutedEventArgs e)
        {
            unit.ManagementDexterityWarrior('-');
            UpdateData();
            CheckingLimit(unit, unit.dexterity, AddDexterity_Button, ReduceDexterity_Button);
        }

        private void ReduceConstitution_Button_Click(object sender, RoutedEventArgs e)
        {
            unit.ManagementConstitutionWarrior('-');
            UpdateData();
            CheckingLimit(unit, unit.constitution, AddConstitution_Button, ReduceConstitution_Button);
        }

        private void ReduceIntelligence_Button_Click(object sender, RoutedEventArgs e)
        {
            unit.ManagementIntelligenceWarrior('-');
            UpdateData();
            CheckingLimit(unit, unit.intelligence, AddIntelligence_Button, ReduceIntelligence_Button);
        }

        // ------------------------------------------------------------------------------------------------------

        private void ResetButton_Click(object sender, RoutedEventArgs e) // Reset characteristics
        {
            MongoExamples.ResetValues(unit.Name);
            unit = MongoExamples.Find(unit.Name);

            for (int un = 0; un < units.Length; un++)
            {
                if (units[un].Name == unit.Name) units[un] = unit;
            }

            UpdateData();
            ResetColorButtons();
        }

        private void ResetColorButtons() // reset button colors
        {
            AddConstitution_Button.Foreground = System.Windows.Media.Brushes.White;
            AddDexterity_Button.Foreground = System.Windows.Media.Brushes.White;
            AddStrength_Button.Foreground = System.Windows.Media.Brushes.White;
            AddIntelligence_Button.Foreground = System.Windows.Media.Brushes.White;
            ReduceConstitution_Button.Foreground = System.Windows.Media.Brushes.White;
            ReduceDexterity_Button.Foreground = System.Windows.Media.Brushes.White;
            ReduceStrength_Button.Foreground = System.Windows.Media.Brushes.White;
            ReduceIntelligence_Button.Foreground = System.Windows.Media.Brushes.White;
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

        private void UnitImg_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e) // Changing a character with Scroll
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

                    SwitchingUnits(units[units.Length - 1]);
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

            UpdateData();
            ResetColorButtons();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MongoExamples.SaveValues(unit.Name, unit);
        }

        private void Image_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            new StartWindow().Show();
            Close();
        }
    }
}
