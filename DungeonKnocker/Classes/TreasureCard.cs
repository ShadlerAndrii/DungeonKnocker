using DungeonKnocker.Constants.Enums;
using DungeonKnocker.Interfaces;

namespace DungeonKnocker.Classes
{
    class TreasureCard : ICard, IEquipable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        
        public int GoldValue { get; set; }
        public bool IsConsumable { get; set; } // Indicates if the card is discarded after use (e.g., potions)

        public Dictionary<StatType, int> Bonuses { get; set; }
        public EquipmentSlot Slot { get; set; }

        public bool IsEquippable => Slot != EquipmentSlot.None; // Determines if the card can be equipped based on its slot

    }
}
