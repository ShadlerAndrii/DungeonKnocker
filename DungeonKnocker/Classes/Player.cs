using DungeonKnocker.Constants.Enums;
using DungeonKnocker.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DungeonKnocker.Classes
{
    class Player : INotifyPropertyChanged
    {
        private int _level = 1;

        public int Level
        {
            get => _level;
            set
            {
                if (_level != value)
                {
                    _level = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalCombatPower));
                }
            }
        }

        // Collection of equipped items, which can be used to calculate bonuses to combat power and other stats
        public List<IEquipable> EquippedItems { get; set; } = new List<IEquipable>();

        // Dynamic property to calculate total combat power based on level and equipped items
        public int TotalCombatPower
        {
            get
            {
                int power = Level;

                foreach (var item in EquippedItems)
                {
                    // Realisation for my Dictionary<StatType, int> Bonuses in IEquipable, to add up all the bonuses for CombatPower
                    if (item.Bonuses != null && item.Bonuses.TryGetValue(StatType.CombatPower, out int bonus))
                    {
                        power += bonus;
                    }
                }
                return power;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
