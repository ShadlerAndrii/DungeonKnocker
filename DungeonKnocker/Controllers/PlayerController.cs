using DungeonKnocker.Classes;

namespace DungeonKnocker.Controllers
{
    class PlayerController
    {
        private Player _player;

        public PlayerController(Player player)
        {
            _player = player;
        }

        public void LevelUpPlayer(int count)
        {
            _player.Level += count;
            // Ensure that the player's level does not drop below 1
            // additionaly check if we want to set a maximum level cap, for example 10 or 20, and implement it here as well
        }
    }
}
