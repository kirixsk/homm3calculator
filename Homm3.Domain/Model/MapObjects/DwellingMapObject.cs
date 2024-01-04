using Homm3.Domain.Calculator;
using Homm3.Domain.Model.Enums;
using Homm3.Domain.Model.Monsters;

namespace Homm3.Domain.Model.MapObjects
{
    public class DwellingMapObject : MapObject
    {
        public Monster[] Monsters { get; set; }
        public int ExtraMultiplier { get; set; }

        public DwellingMapObject(string name, params Monster[] monster) : base(name, 0)
        {
            Monsters = monster;
            ExtraMultiplier = 1;
        }

        public override int GetAiValue(ZoneConfiguration userInput, Town? objectTown = null)
        {
            float result = 0.0f;

            foreach (var monster in Monsters)
            {
                result += GetAiValue(userInput, monster);
            }

            result = result / Math.Min(Monsters.Length, 2);

            return (int)Math.Floor(ExtraMultiplier * result);
        }

        private float GetAiValue(ZoneConfiguration userInput, Monster monster)
        {
            float result;
            if (monster.Town == Town.Neutral)
            {
                result = monster.AiValue * monster.Growth;
            }
            else
            {
                float TotalZoneCount = userInput.TotalTownZoneCount;
                if (TotalZoneCount == 0)
                {
                    return monster.AiValue * monster.Growth;
                }
                var townsCount = (float)userInput.CurrentTownZoneCount;
                result = monster.AiValue * (monster.Growth * (1 + townsCount / TotalZoneCount) + townsCount / 2);
            }
            return result;
        }
    }
}
