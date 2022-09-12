using System.Windows;
using System.Windows.Controls;


namespace DistributionOfPoints
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Warrior warrior = new Warrior();
        public MainWindow()
        {
            InitializeComponent();

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

        private void AddStrength_Button_Click(object sender, RoutedEventArgs e)
        {
            warrior.ManagementStrength();
            StrengthTextBox.Text = warrior.strength[1].ToString();

            UpdateSpecifications();
            CheckingLimit(warrior, warrior.strength, AddStrength_Button);
        }

        private void AddDexterity_Button_Click(object sender, RoutedEventArgs e)
        {
            warrior.ManagementDexterity();
            DexterityTextBox.Text = warrior.dexterity[1].ToString();

            UpdateSpecifications();
            CheckingLimit(warrior, warrior.dexterity, AddDexterity_Button);
        }

        private void AddConstitution_Button_Click(object sender, RoutedEventArgs e)
        {
            warrior.ManagementConstitution();
            ConstitutionTextBox.Text = warrior.constitution[1].ToString();

            UpdateSpecifications();
            CheckingLimit(warrior, warrior.constitution, AddConstitution_Button);
        }

        private void AddIntelligence_Button_Click(object sender, RoutedEventArgs e)
        {
            warrior.ManagementIntelligence();
            IntelligenceTextBox.Text = warrior.intelligence[1].ToString();

            UpdateSpecifications();
            CheckingLimit(warrior, warrior.intelligence, AddIntelligence_Button);
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e) // resetting characteristics
        {
            warrior = new Warrior();
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
    }
}
