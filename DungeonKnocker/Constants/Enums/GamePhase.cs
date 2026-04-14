namespace DungeonKnocker.Constants.Enums
{
    enum GamePhase
    {
        KnockOnTheDoor, // Гравець має вибити двері
        ResolvingRoom, // Гравець б'ється з монстром / читає прокляття / виконує дію кімнати
        LootTheRoom,  // Гравець може взяти скарб
        DisposeCards // Гравець має викинути карти, щоб не перевищувати 5 карт на руці
        // Charity       // Гравець роздає зайві карти в кінці ходу
    }
}
