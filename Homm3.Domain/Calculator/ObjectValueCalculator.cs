using Homm3.Domain.Model.Enums;
using Homm3.Domain.Model.MapObjects;

namespace Homm3.Domain.Calculator
{
    public static class ObjectValueCalculator
    {
        public static int GetTotalValue(ZoneConfiguration zoneConfiguration, List<MapObject> objects, Town? objectTown = null)
        {
            var value = objects.Sum(o => o.GetAiValue(zoneConfiguration, objectTown));
            return value;
        }
    }
}
