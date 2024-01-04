using Homm3.Domain.Model.Enums;
using Homm3.Domain.Model.MapObjects;
using Homm3.Domain.Model.Monsters;

namespace Homm3.Domain.Calculator
{
    public class Homm3Calculator : IHomm3Calculator
    {
        private readonly IMapObjectFactory mapObjectFactory;

        public Homm3Calculator(IMapObjectFactory mapObjectFactory)
        {
            this.mapObjectFactory = mapObjectFactory;
        }

        public CalculatedMonsterValues CalculateMonsterCount(
            MapMonsterStrength mapMonsterStrength,
            ZoneStrength zoneStrength,
            ZoneConfiguration zoneConfiguration,
            Monster monster,
            List<MapObject> objects,
            int week,
            Town? town = null)
        {
            var totalValue = ObjectValueCalculator.GetTotalValue(zoneConfiguration, objects, town);
            int protectionIndex = (int)zoneStrength + (int)mapMonsterStrength;
            var adjustedWithStrengthValue = AdjustedValueCalculator.CalculateAdjustedValue(totalValue, protectionIndex);
            var monsterValue = ValueToMonsterConverter.ConvertValueToMonsters(monster, adjustedWithStrengthValue, week);
            return monsterValue;
        }

        public List<GuessedObjectResult> GuessObject(
            MapMonsterStrength mapMonsterStrength,
            ZoneStrength zoneStrength,
            ZoneConfiguration zoneConfiguration,
            Monster monster,
            List<MapObject> objects,
            int week,
            string objectVisuals,
            int minMonsterCount,
            int maxMonsterCount,
            Town? town = null)
        {
            if (objectVisuals == "ZoneGuard")
            {
                return GuessZoneGuard(mapMonsterStrength, monster, week, minMonsterCount, maxMonsterCount);
            }
            else
            {
                return GuessForMapObjects(mapMonsterStrength, zoneStrength, zoneConfiguration, monster, objects, week, objectVisuals, minMonsterCount, maxMonsterCount, town);
            }
        }

        private static List<GuessedObjectResult> GuessZoneGuard(MapMonsterStrength mapMonsterStrength, Monster monster, int week, int minMonsterCount, int maxMonsterCount)
        {
            var maxValueToCheck = 300000;
            var minValueToCheck = 500;
            var minValue = maxValueToCheck;
            var maxValue = minValueToCheck;
            for (var val = 500; val <= 300000; val += 500)
            {
                CalculatedMonsterValues monsterValue = GetZoneGuardMonsterCount(mapMonsterStrength, monster, week, val);
                if (monsterValue.MinimalCount <= maxMonsterCount && monsterValue.MaximalCount >= minMonsterCount)
                {
                    if (val < minValue)
                    {
                        minValue = val;
                    }
                    if (val > maxValue)
                    {
                        maxValue = val;
                    }
                }
            }
            var averageInput = minMonsterCount + (maxMonsterCount - minMonsterCount) / 2;
            var averageZoneGuard = minValue + (maxValue - minValue) / 2;

            GuessedObjectResult min = CreateZoneGuardObject(mapMonsterStrength, monster, week, averageInput, minValue, "Min");
            GuessedObjectResult average = CreateZoneGuardObject(mapMonsterStrength, monster, week, averageInput, averageZoneGuard, "Average");
            GuessedObjectResult max = CreateZoneGuardObject(mapMonsterStrength, monster, week, averageInput, maxValue, "Max");
            return new List<GuessedObjectResult> { min, average, max };
        }

        private List<GuessedObjectResult> GuessForMapObjects(
            MapMonsterStrength mapMonsterStrength, 
            ZoneStrength zoneStrength,
            ZoneConfiguration zoneConfiguration, 
            Monster monster,
            List<MapObject> objects, 
            int week,
            string objectVisuals, 
            int minMonsterCount, 
            int maxMonsterCount, 
            Town? town)
        {
            var result = new List<GuessedObjectResult>();
            var guessableObjects = this.mapObjectFactory.ListMapObjects()
                .Where(x => string.Equals(x.Visual, objectVisuals) && ((x.ObjectTown == null) || (x.ObjectTown == town))).ToList();
            if (guessableObjects.Count <= 1)
            {
                return new List<GuessedObjectResult>();
            }
            foreach (var guessableObject in guessableObjects)
            {
                var listWithObjectWithGuessableObject = new List<MapObject>();
                listWithObjectWithGuessableObject.AddRange(objects);
                listWithObjectWithGuessableObject.Add(guessableObject);
                var totalValue = ObjectValueCalculator.GetTotalValue(zoneConfiguration, listWithObjectWithGuessableObject, town);
                int protectionIndex = (int)zoneStrength + (int)mapMonsterStrength;
                var adjustedWithStrengthValue = AdjustedValueCalculator.CalculateAdjustedValue(totalValue, protectionIndex);
                var monsterValue = ValueToMonsterConverter.ConvertValueToMonsters(monster, adjustedWithStrengthValue, week);
                if (monsterValue.MinimalCount <= maxMonsterCount && monsterValue.MaximalCount >= minMonsterCount)
                {
                    result.Add(new GuessedObjectResult
                    {
                        MapObject = guessableObject,
                        DifferenceFromAverage = Math.Abs(monsterValue.AverageMonsterCount - (minMonsterCount + (maxMonsterCount - minMonsterCount) / 2)),
                        Min = monsterValue.MinimalCount,
                        Max = monsterValue.MaximalCount
                    });
                }
            }
            return result.OrderByDescending(x => x.MapObject.Frequency).ThenBy(x => x.DifferenceFromAverage).ToList();
        }

        private static GuessedObjectResult CreateZoneGuardObject(MapMonsterStrength mapMonsterStrength, Monster monster, int week, int inputAverage, int zoneGuardValue, string suffix)
        {
            CalculatedMonsterValues monsterMinValue = GetZoneGuardMonsterCount(mapMonsterStrength, monster, week, zoneGuardValue);

            return new GuessedObjectResult
            {
                MapObject = new MapObject($"ZoneGuard {suffix} {zoneGuardValue / 1000}k", zoneGuardValue, frequency: 0),
                Max = monsterMinValue.MaximalCount,
                Min = monsterMinValue.MinimalCount,
                DifferenceFromAverage = Math.Abs(monsterMinValue.AverageMonsterCount - inputAverage),
            };
        }

        private static CalculatedMonsterValues GetZoneGuardMonsterCount(MapMonsterStrength mapMonsterStrength, Monster monster, int week, int val)
        {
            var totalValue = val;
            int protectionIndex = (int)ZoneStrength.ZoneGuard + (int)mapMonsterStrength;
            var adjustedWithStrengthValue = AdjustedValueCalculator.CalculateAdjustedValue(totalValue, protectionIndex);
            var monsterValue = ValueToMonsterConverter.ConvertValueToMonsters(monster, adjustedWithStrengthValue, week);
            return monsterValue;
        }
    }
}
