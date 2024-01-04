using Homm3.Domain.Model.Monsters;

namespace Homm3.Domain.Calculator
{
    public static class ValueToMonsterConverter
    {
        public static CalculatedMonsterValues ConvertValueToMonsters(Monster monster, int value, int week)
        {
            var firstWeekMonsterCount = (int)Math.Round((double)value / monster.AiValue);
            var monsterCountDeviation = firstWeekMonsterCount >= 4 ? firstWeekMonsterCount / 4 : 0;
            var firstWeekMonsterMax = firstWeekMonsterCount + monsterCountDeviation;
            var firstWeekMonsterMin = firstWeekMonsterCount - monsterCountDeviation;
            var adjustedForWeeklyGrowthMax = (int)Math.Floor(firstWeekMonsterMax * Math.Pow(1.1, week - 1));
            var adjustedForWeeklyGrowthMin = (int)Math.Floor(firstWeekMonsterMin * Math.Pow(1.1, week - 1));
            var result = new CalculatedMonsterValues
            {
                AverageMonsterCount = adjustedForWeeklyGrowthMin + (adjustedForWeeklyGrowthMax - adjustedForWeeklyGrowthMin) / 2,
                MinimalCount = adjustedForWeeklyGrowthMin,
                MaximalCount = adjustedForWeeklyGrowthMax,
                MonsterCountDeviation = (adjustedForWeeklyGrowthMax - adjustedForWeeklyGrowthMin) / 2,
                Monster = monster
            };
            return result;
        }
    }
}
