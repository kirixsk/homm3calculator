using Homm3.Domain.Calculator;
using Homm3.Domain.Model.Enums;

namespace Homm3.Domain.Model.MapObjects
{
    public class PandoraWithMonstersMapObject : MapObject
    {
        public int TotalValue { get; }
        public PandoraWithMonstersMapObject(
            string name, 
            int totalValue,
            string orderingDescription,
            Town town) : base(name, totalValue, orderingDescription, "Pandora", 3, town)
        {
            TotalValue = totalValue;
        }

        public override int GetAiValue(ZoneConfiguration userInput, Town? objectTown = null)
        {
            if (objectTown == null)
            {
                return TotalValue;
            }
            var townsCount = (float)userInput.CurrentTownZoneCount;
            var totalNumberOfZones = (float)userInput.TotalTownZoneCount;
            return (int)Math.Floor(TotalValue * (1 + townsCount / totalNumberOfZones));
        }
    }
}
