using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DistributionOfPoints_Console;

namespace DistributionOfPoints
{
    /// <summary>
    /// Логика взаимодействия для WizardWindows.xaml
    /// </summary>
    public partial class WizardWindows : Window
    {
        private Unit wizard = MongoExamples.Find("Wizard");
        private bool initActive;
        private bool scrollWas = false;

        public WizardWindows()
        {
            InitializeComponent();

            initActive = true;
            ComboBoxUnits.SelectedIndex = 2;

            SkillPointsTextBox.Text = wizard.skillPoints.ToString();
            StrengthTextBox.Text = wizard.strength[1].ToString();
            DexterityTextBox.Text = wizard.dexterity[1].ToString();
            ConstitutionTextBox.Text = wizard.constitution[1].ToString();
            IntelligenceTextBox.Text = wizard.intelligence[1].ToString();

            MaxHPLabel.Content = wizard.maxHP;
            MaxMPLabel.Content = wizard.maxMP;
            PAttackLabel.Content = wizard.PAttack;
            MAttackLabel.Content = wizard.MAttack;
            PDefLabel.Content = wizard.PDef;
        }

        private void UpdateSpecifications() // updating the display of indicators
        {
            SkillPointsTextBox.Text = wizard.skillPoints.ToString();

            MaxHPLabel.Content = wizard.maxHP;
            MaxMPLabel.Content = wizard.maxMP;
            PAttackLabel.Content = wizard.PAttack;
            MAttackLabel.Content = wizard.MAttack;
            PDefLabel.Content = wizard.PDef;
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
            wizard.ManagementStrengthWizard('+');
            StrengthTextBox.Text = wizard.strength[1].ToString();

            UpdateSpecifications();
            CheckingLimit(wizard, wizard.strength, AddStrength_Button);
        }

        private void AddDexterity_Button_Click(object sender, RoutedEventArgs e)
        {
            wizard.ManagementDexterityWizard('+');
            DexterityTextBox.Text = wizard.dexterity[1].ToString();

            UpdateSpecifications();
            CheckingLimit(wizard, wizard.dexterity, AddDexterity_Button);
        }

        private void AddConstitution_Button_Click(object sender, RoutedEventArgs e)
        {
            wizard.ManagementConstitutionWizard('+');
            ConstitutionTextBox.Text = wizard.constitution[1].ToString();

            UpdateSpecifications();
            CheckingLimit(wizard, wizard.constitution, AddConstitution_Button);
        }

        private void AddIntelligence_Button_Click(object sender, RoutedEventArgs e)
        {
            wizard.ManagementIntelligenceWizard('+');
            IntelligenceTextBox.Text = wizard.intelligence[1].ToString();

            UpdateSpecifications();
            CheckingLimit(wizard, wizard.intelligence, AddIntelligence_Button);
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e) // resetting characteristics
        {
            MongoExamples.ResetValues(wizard.Name);
            wizard = MongoExamples.Find("Wizard");

            StrengthTextBox.Text = wizard.strength[1].ToString();
            DexterityTextBox.Text = wizard.dexterity[1].ToString();
            ConstitutionTextBox.Text = wizard.constitution[1].ToString();
            IntelligenceTextBox.Text = wizard.intelligence[1].ToString();

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

        private void WizardImg_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!scrollWas)
            {
                if (e.Delta > 0)
                {
                    new WarriorWindow().Show();
                    Close();
                }

                else if (e.Delta < 0)
                {
                    new RogueWindow().Show();
                    Close();
                }

                scrollWas = true;
            }
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            MongoExamples.ReplaceByName(wizard.Name, wizard);
        }
    }
}
