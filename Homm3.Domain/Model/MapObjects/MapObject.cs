using Homm3.Domain.Calculator;
using Homm3.Domain.Model.Enums;

namespace Homm3.Domain.Model.MapObjects
{
    public class MapObject : IComparable<MapObject>
    {
        public string Name { get; set; }
        public int AiValue { get; set; }
        public string Visual { get; set; }
        public int Frequency { get; set; }
        public Town? ObjectTown { get; }
        public string OrderingDescription { get; private set; }

        public MapObject(
            string name, 
            int aiValue, 
            string? orderingDescription = null,
            string? visual = null, 
            int? frequency = 1,
            Town? town = null)
        {
            Name = name;
            AiValue = aiValue;
            Visual = visual ?? name;
            OrderingDescription = orderingDescription ?? name;
            Frequency = frequency ?? 1;
            ObjectTown = town;
        }

        public virtual int GetAiValue(ZoneConfiguration userInput, Town? objectTown = null)
        {
            return AiValue;
        }

        public int CompareTo(MapObject? other)
        {
            return OrderingDescription.CompareTo(other?.OrderingDescription);
        }
    }
}
