using DungeonKnocker.Classes;
using DungeonKnocker.Constants;
using DungeonKnocker.Constants.Enums;

namespace DungeonKnocker.Controllers
{
    class MonsterController
    {
        private DoorCard _currentMonster;

        public DoorCard CurrentMonster
        {
            set { _currentMonster = value; }
            get { return _currentMonster; }
        }

        public (int? RollResult, bool IsEscaped) TryToEscape()
        {
            switch (_currentMonster.EscapeType)
            {
                case EscapeType.Guaranteed:
                    {
                        return (null, true);
                    }
                case EscapeType.Impossible:
                    {
                        return (null, false);
                    }
                default: // EscapeType.Normal
                    {
                        int roll = Random.Shared.Next(1, 7); // Simulate a dice roll (1-6)
                        int bonus = _currentMonster.EscapeBonus ?? 0;
                        int totalScore = roll + bonus;
                        return (roll, totalScore >= EscapeThreshold.escapeThreshold);
                    }
            }
        }

        public bool TryToFight(Player player)
        {
            return player.TotalCombatPower >= _currentMonster.MonsterLevel;
        }
    }
}