using System;
using System.Windows;
using System.Windows.Controls;
using DistributionOfPoints_Console;

namespace DistributionOfPoints
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WarriorWindow : Window
    {
        Unit warrior = MongoExamples.Find("Warrior");
        private bool initActive;
        private bool scrollWas = false;

        public WarriorWindow()
        {
            InitializeComponent();

            initActive = true;
            ComboBoxUnits.SelectedIndex = 0;

            SkillPointsTextBox.Text = warrior.skillPoints.ToString();
            StrengthTextBox.Text = warrior.strength[1].ToString();
            DexterityTextBox.Text = warrior.dexterity[1].ToString();
            ConstitutionTextBox.Text = warrior.constitution[1].ToString();
            IntelligenceTextBox.Text = warrior.intelligence[1].ToString();

            MaxHPLabel.Content = warrior.maxHP;
            MaxMPLabel.Content = warrior.maxMP;
            PAttackLabel.Content = warrior.PAttack;
            MAttackLabel.Content = warrior.MAttack;
            PDefLabel.Content = warrior.PDef;
        }

        private void UpdateSpecifications() // updating the display of indicators
        {
            SkillPointsTextBox.Text = warrior.skillPoints.ToString();

            MaxHPLabel.Content = warrior.maxHP;
            MaxMPLabel.Content = warrior.maxMP;
            PAttackLabel.Content = warrior.PAttack;
            MAttackLabel.Content = warrior.MAttack;
            PDefLabel.Content = warrior.PDef;
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
            warrior.ManagementStrengthWarrior('+');
            StrengthTextBox.Text = warrior.strength[1].ToString();

            UpdateSpecifications();
            CheckingLimit(warrior, warrior.strength, AddStrength_Button);
        }

        private void AddDexterity_Button_Click(object sender, RoutedEventArgs e)
        {
            warrior.ManagementDexterityWarrior('+');
            DexterityTextBox.Text = warrior.dexterity[1].ToString();

            UpdateSpecifications();
            CheckingLimit(warrior, warrior.dexterity, AddDexterity_Button);
        }

        private void AddConstitution_Button_Click(object sender, RoutedEventArgs e)
        {
            warrior.ManagementConstitutionWarrior('+');
            ConstitutionTextBox.Text = warrior.constitution[1].ToString();

            UpdateSpecifications();
            CheckingLimit(warrior, warrior.constitution, AddConstitution_Button);
        }

        private void AddIntelligence_Button_Click(object sender, RoutedEventArgs e)
        {
            warrior.ManagementIntelligenceWarrior('+');
            IntelligenceTextBox.Text = warrior.intelligence[1].ToString();

            UpdateSpecifications();
            CheckingLimit(warrior, warrior.intelligence, AddIntelligence_Button);
        }

        private void ReduceStrength_Button_Click(object sender, RoutedEventArgs e)
        {
            warrior.ManagementStrengthWarrior('-');
            StrengthTextBox.Text = warrior.strength[1].ToString();

            UpdateSpecifications();
            CheckingLimit(warrior, warrior.strength, AddStrength_Button);
        }

        private void ReduceDexterity_Button_Click(object sender, RoutedEventArgs e)
        {
            warrior.ManagementDexterityWarrior('-');
            DexterityTextBox.Text = warrior.dexterity[1].ToString();

            UpdateSpecifications();
            CheckingLimit(warrior, warrior.dexterity, AddDexterity_Button);
        }

        private void ReduceConstitution_Button_Click(object sender, RoutedEventArgs e)
        {
            warrior.ManagementConstitutionWarrior('-');
            ConstitutionTextBox.Text = warrior.constitution[1].ToString();

            UpdateSpecifications();
            CheckingLimit(warrior, warrior.constitution, AddConstitution_Button);
        }

        private void ReduceIntelligence_Button_Click(object sender, RoutedEventArgs e)
        {
            warrior.ManagementIntelligenceWarrior('-');
            IntelligenceTextBox.Text = warrior.intelligence[1].ToString();

            UpdateSpecifications();
            CheckingLimit(warrior, warrior.intelligence, AddIntelligence_Button);
        }

        // ------------------------------------------------------------------------------------------------------

        private void ResetButton_Click(object sender, RoutedEventArgs e) // resetting characteristics
        {
            MongoExamples.ResetValues(warrior.Name);
            warrior = MongoExamples.Find("Warrior");

            StrengthTextBox.Text = warrior.strength[1].ToString();
            DexterityTextBox.Text = warrior.dexterity[1].ToString();
            ConstitutionTextBox.Text = warrior.constitution[1].ToString();
            IntelligenceTextBox.Text = warrior.intelligence[1].ToString();

            UpdateSpecifications();

            AddStrength_Button.IsEnabled = true;
            AddDexterity_Button.IsEnabled = true;
            AddConstitution_Button.IsEnabled = true;
            AddIntelligence_Button.IsEnabled = true;
        }

        private void ComboBoxUnits_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!initActive)
            {
                if (ComboBoxUnits.SelectedIndex == 0)
                {
                    new WarriorWindow().Show();
                    Close();
                }

                else if (ComboBoxUnits.SelectedIndex == 1)
                {
                    new RogueWindow().Show();
                    Close();
                }

                else if (ComboBoxUnits.SelectedIndex == 2)
                {
                    new WizardWindows().Show();
                    Close();
                }
            }

            else initActive = false;
        }

        private void WarriorImg_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            if (!scrollWas)
            {
                if (e.Delta > 0)
                {
                    new RogueWindow().Show();
                    Close();
                }

                else if (e.Delta < 0)
                {
                    new WizardWindows().Show();
                    Close();
                }

                scrollWas = true;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MongoExamples.ReplaceByName(warrior.Name, warrior);
        }  
    }
}
