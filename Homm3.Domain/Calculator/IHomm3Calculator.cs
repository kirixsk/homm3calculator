using Homm3.Domain.Model.Enums;
using Homm3.Domain.Model.MapObjects;
using Homm3.Domain.Model.Monsters;

namespace Homm3.Domain.Calculator
{
    public interface IHomm3Calculator
    {
        CalculatedMonsterValues CalculateMonsterCount(
            MapMonsterStrength mapMonsterStrength,
            ZoneStrength zoneStrength,
            ZoneConfiguration zoneConfiguration,
            Monster monster,
            List<MapObject> objects,
            int week,
            Town? town = null);
        List<GuessedObjectResult> GuessObject(
            MapMonsterStrength mapMonsterStrength,
            ZoneStrength zoneStrength,
            ZoneConfiguration zoneConfiguration,
            Monster monster,
            List<MapObject> objects,
            int week,
            string objectVisuals,
            int minMonsterCount,
            int maxMonsterCount,
            Town? town = null);
    }
}