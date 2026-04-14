using DungeonKnocker.Classes;
using DungeonKnocker.Constants.Enums;
using DungeonKnocker.Controllers;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DungeonKnocker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GamePhase _currentPhase;
        private DoorController _doorController;
        private TreasureController _treasureController;
        private MonsterController _monsterController;
        private Player _player;
        private PlayerController _playerController;

        public MainWindow()
        {
            InitializeComponent();
            _currentPhase = GamePhase.KnockOnTheDoor;
            _doorController = new DoorController();
            _treasureController = new TreasureController();
            _monsterController = new MonsterController();
            _player = new Player();
            _playerController = new PlayerController(_player);

            // Set the DataContext for the player stat block bindings
            this.DataContext = _player;
        }

        private void RenderCard(DoorCard card) // Or ICard, if we want to use the same method for treasures later
        {
            if (card == null)
            {
                // Reset the card display to show that no card is currently drawn after fight or successful escape
                CardIdTextBlock.Text = "Id: -";
                CardNameTextBlock.Text = "No card drawn";
                CardDescriptionTextBlock.Text = "";
                ActionButtonsPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                // Render the card details on the UI
                CardIdTextBlock.Text = $"Id: {card.Id}";
                CardNameTextBlock.Text = card.Name;
                CardDescriptionTextBlock.Text = card.Description;

                // Render action buttons based on the card type. Might be switched to switch-case later if we want to add more types and buttons.
                ActionButtonsPanel.Visibility = card.Type == DoorCardType.Monster
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }
        }

        private void DrawDoor_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPhase != GamePhase.KnockOnTheDoor)
            {
                MessageBox.Show("You can't draw a card now! Deal with what you have first.");
                return;
            }

            var card = _doorController.DrawCard();
            if (card != null)
            {
                RenderCard(card);

                if (card.Type == DoorCardType.Monster)
                {
                    _currentPhase = GamePhase.ResolvingRoom;
                    _monsterController.CurrentMonster = card;
                }
            }
            else
            {
                MessageBox.Show("No more door cards available!");
            }
        }

        private void DrawTreasure_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RunFromMonster_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPhase != GamePhase.ResolvingRoom)
            {
                MessageBox.Show("You can't run now! Deal with what you have first.");
                return;
            }

            // Deconstruct the result of TryToEscape into roll and escaped variables
            var (roll, escaped) = _monsterController.TryToEscape();
            string rollMessage = roll.HasValue
                ? $"You rolled a {roll.Value} to escape."
                : "You don`t roll for escape. It`s determined.";

            if (escaped)
            {
                MessageBox.Show("You successfully escaped from the monster!" +
                    $"\n{rollMessage}");

                _currentPhase = GamePhase.KnockOnTheDoor;
                _monsterController.CurrentMonster = null; // Clear the current monster after escaping
                RenderCard(null);
            }
            else
            {
                MessageBox.Show("You failed to escape from the monster!" +
                    $"\n{rollMessage}");
                // There should be some consequences for failing to escape, like taking damage or losing levels
                // Plus clear the current monster and reset the phase to KnockOnTheDoor, and clearing UI
            }
        }

        private void FightButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPhase != GamePhase.ResolvingRoom)
            {
                MessageBox.Show("You can't fight now! Deal with what you have first.");
                return;
            }

            if (_monsterController.TryToFight(_player))
            {
                MessageBox.Show("You defeated the monster!");
                _playerController.LevelUpPlayer(_monsterController.CurrentMonster.LevelsRewarded.Value);
                // TODO:
                // There should be some rewards for defeating the monster, like gaining treasures

            }
            else
            {
                MessageBox.Show("You failed to defeat the monster!");
                // There should be some consequences for failing to fight, like taking damage or losing levels
                // Plus clear the current monster and reset the phase to KnockOnTheDoor, and clearing UI
            }
            _currentPhase = GamePhase.KnockOnTheDoor;
            _monsterController.CurrentMonster = null; // Clear the current monster after defeating it
            RenderCard(null);
        }
    }
}