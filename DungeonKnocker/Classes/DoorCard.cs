using DungeonKnocker.Constants.Enums;
using DungeonKnocker.Interfaces;

namespace DungeonKnocker.Classes
{
    class DoorCard : ICard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }

        public DoorCardType Type { get; set; }

        public int? MonsterLevel { get; set; }
        public int? TreasuresRewarded { get; set; }
        public int? LevelsRewarded { get; set; }
        public EscapeType? EscapeType { get; set; }
        public int? EscapeBonus { get; set; }
        public string? EscapeDebuff { get; set; }
    }
}
