using Homm3.Domain.Model.Enums;
using Homm3.Domain.Model.Monsters;

namespace Homm3.Domain.Model.MapObjects
{
    public interface IObjectWithMonsterRewardFactory
    {
        ILookup<Town, Monster> Monsters { get; }

        IEnumerable<MapObject> GenerateObjectsWithMonstersRewards();
    }
}