using DungeonKnocker.Constants.Enums;

namespace DungeonKnocker.Interfaces
{
    interface IEquipable
    {
        Dictionary<StatType, int> Bonuses { get; set; }
        EquipmentSlot Slot { get; set; }
    }
}
