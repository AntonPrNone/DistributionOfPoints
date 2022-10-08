using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DistributionOfPoints_Console;

namespace DistributionOfPoints
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WarriorWindow : Window
    {
        Unit unit;
        Unit warrior = MongoExamples.Find("Warrior");
        Unit rogue = MongoExamples.Find("Rogue");
        Unit wizard = MongoExamples.Find("Wizard");

        bool mouseScrolled = false;

        public WarriorWindow()
        {
            InitializeComponent();

            unit = warrior;
            ComboBoxUnits.SelectedIndex = 0;

            SkillPointsTextBox.Text = unit.skillPoints.ToString();
            StrengthTextBox.Text = unit.strength[1].ToString();
            DexterityTextBox.Text = unit.dexterity[1].ToString();
            ConstitutionTextBox.Text = unit.constitution[1].ToString();
            IntelligenceTextBox.Text = unit.intelligence[1].ToString();

            MaxHPLabel.Content = unit.maxHP;
            MaxMPLabel.Content = unit.maxMP;
            PAttackLabel.Content = unit.PAttack;
            MAttackLabel.Content = unit.MAttack;
            PDefLabel.Content = unit.PDef;
        }

        private void UpdateSpecifications() // updating the display of indicators
        {
            SkillPointsTextBox.Text = unit.skillPoints.ToString();

            MaxHPLabel.Content = unit.maxHP;
            MaxMPLabel.Content = unit.maxMP;
            PAttackLabel.Content = unit.PAttack;
            MAttackLabel.Content = unit.MAttack;
            PDefLabel.Content = unit.PDef;
        }

        private void CheckingLimit(Unit unit, int[] characteristic, Button button) // checking the achievement of the limit of skill points and levels of characteristics
        {
            if (unit.skillPoints == 0)
            {
                MessageBox.Show("Skill points are over", "The limit has been reached",
         MessageBoxButton.OK, MessageBoxImage.Information);
                AddStrength_Button.IsEnabled = false;
                AddDexterity_Button.IsEnabled = false;
                AddConstitution_Button.IsEnabled = false;
                AddIntelligence_Button.IsEnabled = false;
            }

            if (characteristic[1] >= characteristic[2])
            {
                MessageBox.Show("The maximum characteristic level has been reached", "The limit has been reached",
         MessageBoxButton.OK, MessageBoxImage.Information);
                button.IsEnabled = false;
            }
        }

        // -------------------------------------- MANAGING CHARACTERISTICS --------------------------------------

        private void AddStrength_Button_Click(object sender, RoutedEventArgs e)
        {
            unit.ManagementStrengthWarrior('+');
            StrengthTextBox.Text = unit.strength[1].ToString();

            UpdateSpecifications();
            CheckingLimit(unit, unit.strength, AddStrength_Button);
        }

        private void AddDexterity_Button_Click(object sender, RoutedEventArgs e)
        {
            unit.ManagementDexterityWarrior('+');
            DexterityTextBox.Text = unit.dexterity[1].ToString();

            UpdateSpecifications();
            CheckingLimit(unit, unit.dexterity, AddDexterity_Button);
        }

        private void AddConstitution_Button_Click(object sender, RoutedEventArgs e)
        {
            unit.ManagementConstitutionWarrior('+');
            ConstitutionTextBox.Text = unit.constitution[1].ToString();

            UpdateSpecifications();
            CheckingLimit(unit, unit.constitution, AddConstitution_Button);
        }

        private void AddIntelligence_Button_Click(object sender, RoutedEventArgs e)
        {
            unit.ManagementIntelligenceWarrior('+');
            IntelligenceTextBox.Text = unit.intelligence[1].ToString();

            UpdateSpecifications();
            CheckingLimit(unit, unit.intelligence, AddIntelligence_Button);
        }

        private void ReduceStrength_Button_Click(object sender, RoutedEventArgs e)
        {
            unit.ManagementStrengthWarrior('-');
            StrengthTextBox.Text = unit.strength[1].ToString();

            UpdateSpecifications();
            CheckingLimit(unit, unit.strength, AddStrength_Button);
        }

        private void ReduceDexterity_Button_Click(object sender, RoutedEventArgs e)
        {
            unit.ManagementDexterityWarrior('-');
            DexterityTextBox.Text = unit.dexterity[1].ToString();

            UpdateSpecifications();
            CheckingLimit(unit, unit.dexterity, AddDexterity_Button);
        }

        private void ReduceConstitution_Button_Click(object sender, RoutedEventArgs e)
        {
            unit.ManagementConstitutionWarrior('-');
            ConstitutionTextBox.Text = unit.constitution[1].ToString();

            UpdateSpecifications();
            CheckingLimit(unit, unit.constitution, AddConstitution_Button);
        }

        private void ReduceIntelligence_Button_Click(object sender, RoutedEventArgs e)
        {
            unit.ManagementIntelligenceWarrior('-');
            IntelligenceTextBox.Text = unit.intelligence[1].ToString();

            UpdateSpecifications();
            CheckingLimit(unit, unit.intelligence, AddIntelligence_Button);
        }

        // ------------------------------------------------------------------------------------------------------

        private void ResetButton_Click(object sender, RoutedEventArgs e) // resetting characteristics
        {
            MongoExamples.ResetValues(unit.Name);
            unit = MongoExamples.Find("Warrior");

            StrengthTextBox.Text = unit.strength[1].ToString();
            DexterityTextBox.Text = unit.dexterity[1].ToString();
            ConstitutionTextBox.Text = unit.constitution[1].ToString();
            IntelligenceTextBox.Text = unit.intelligence[1].ToString();

            UpdateSpecifications();

            AddStrength_Button.IsEnabled = true;
            AddDexterity_Button.IsEnabled = true;
            AddConstitution_Button.IsEnabled = true;
            AddIntelligence_Button.IsEnabled = true;
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

        private void WarriorImg_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            Unit[] units = { warrior, rogue, wizard };
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

        private void Window_Closed(object sender, EventArgs e)
        {
            MongoExamples.ReplaceByName(unit.Name, unit);
        }

        private void SwitchingUnits(Unit unit)
        {
            MongoExamples.ReplaceByName(this.unit.Name, this.unit);
            this.unit = unit;

            if (unit.Name == "Warrior")
            {
                WarriorImg.Source = new BitmapImage(new Uri("/BTNKnight.png", UriKind.Relative));

                GradientStopCollection gsc = new GradientStopCollection();
                gsc.Add(new GradientStop()
                {
                    Color = Colors.Red,
                    Offset = 0.0
                });

                gsc.Add(new GradientStop()
                {
                    Color = Colors.Black,
                    Offset = 0.5
                });

                ProstoWindow.Background = new LinearGradientBrush(gsc, 0)
                {
                    StartPoint = new Point(0.5, 0),
                    EndPoint = new Point(0.5, 1)
                };
            }

            if (unit.Name == "Rogue")
            {
                WarriorImg.Source = new BitmapImage(new Uri("/BTNBandit.webp", UriKind.Relative));
            }

            if (unit.Name == "Wizard")
            {
                WarriorImg.Source = new BitmapImage(new Uri("/BTNRogueWizard.webp", UriKind.Relative));
            }

            UpdateSpecifications();
            StrengthTextBox.Text = unit.strength[1].ToString();
            DexterityTextBox.Text = unit.dexterity[1].ToString();
            ConstitutionTextBox.Text = unit.constitution[1].ToString();
            IntelligenceTextBox.Text = unit.intelligence[1].ToString();
        }
    }
}
