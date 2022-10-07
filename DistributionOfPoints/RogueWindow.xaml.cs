using System.Windows;
using System.Windows.Controls;
using DistributionOfPoints_Console;

namespace DistributionOfPoints
{
    /// <summary>
    /// Логика взаимодействия для RogueWindow.xaml
    /// </summary>
    public partial class RogueWindow : Window
    {
        private Unit rogue = MongoExamples.Find("Rogue");
        private bool initActive;
        private bool scrollWas = false;

        public RogueWindow()
        {
            InitializeComponent();

            initActive = true;
            ComboBoxUnits.SelectedIndex = 1;

            SkillPointsTextBox.Text = rogue.skillPoints.ToString();
            StrengthTextBox.Text = rogue.strength[1].ToString();
            DexterityTextBox.Text = rogue.dexterity[1].ToString();
            ConstitutionTextBox.Text = rogue.constitution[1].ToString();
            IntelligenceTextBox.Text = rogue.intelligence[1].ToString();

            MaxHPLabel.Content = rogue.maxHP;
            MaxMPLabel.Content = rogue.maxMP;
            PAttackLabel.Content = rogue.PAttack;
            MAttackLabel.Content = rogue.MAttack;
            PDefLabel.Content = rogue.PDef;
        }

        private void UpdateSpecifications() // updating the display of indicators
        {
            SkillPointsTextBox.Text = rogue.skillPoints.ToString();

            MaxHPLabel.Content = rogue.maxHP;
            MaxMPLabel.Content = rogue.maxMP;
            PAttackLabel.Content = rogue.PAttack;
            MAttackLabel.Content = rogue.MAttack;
            PDefLabel.Content = rogue.PDef;
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

        private void AddStrength_Button_Click(object sender, RoutedEventArgs e)
        {
            rogue.ManagementStrengthRogue('+');
            StrengthTextBox.Text = rogue.strength[1].ToString();

            UpdateSpecifications();
            CheckingLimit(rogue, rogue.strength, AddStrength_Button);
        }

        private void AddDexterity_Button_Click(object sender, RoutedEventArgs e)
        {
            rogue.ManagementDexterityRogue('+');
            DexterityTextBox.Text = rogue.dexterity[1].ToString();

            UpdateSpecifications();
            CheckingLimit(rogue, rogue.dexterity, AddDexterity_Button);
        }

        private void AddConstitution_Button_Click(object sender, RoutedEventArgs e)
        {
            rogue.ManagementConstitutionRogue('+');
            ConstitutionTextBox.Text = rogue.constitution[1].ToString();

            UpdateSpecifications();
            CheckingLimit(rogue, rogue.constitution, AddConstitution_Button);
        }

        private void AddIntelligence_Button_Click(object sender, RoutedEventArgs e)
        {
            rogue.ManagementIntelligenceRogue('+');
            IntelligenceTextBox.Text = rogue.intelligence[1].ToString();

            UpdateSpecifications();
            CheckingLimit(rogue, rogue.intelligence, AddIntelligence_Button);
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e) // resetting characteristics
        {
            MongoExamples.ResetValues(rogue.Name);
            rogue = MongoExamples.Find("Rogue");

            StrengthTextBox.Text = rogue.strength[1].ToString();
            DexterityTextBox.Text = rogue.dexterity[1].ToString();
            ConstitutionTextBox.Text = rogue.constitution[1].ToString();
            IntelligenceTextBox.Text = rogue.intelligence[1].ToString();

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

        private void RogueImg_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            if (!scrollWas)
            {
                if (e.Delta > 0)
                {
                    new WizardWindows().Show();
                    Close();
                }

                else if (e.Delta < 0)
                {
                    new WarriorWindow().Show();
                    Close();
                }

                scrollWas = true;
            }
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            MongoExamples.ReplaceByName(rogue.Name, rogue);
        }
    }
}
