using Homm3.Domain.Model.MapPresets;

namespace Homm3.WebApi.Contracts
{
    public class ReferenceDataContract
    {
        public ReferenceDataContract(
            List<MonsterContract> monsters,
            List<MapObjectContract> mapObjects,
            List<string> towns,
            List<string> guessableObjects,
            List<KeyValuePair<string, int>> mapMonsterStrength,
            List<KeyValuePair<string, int>> zoneMonsterStrength,
            List<Preset> presets)
        {
            Monsters = monsters;
            MapObjects = mapObjects;
            Towns = towns;
            GuessableObjects = guessableObjects;
            MapMonsterStrength = mapMonsterStrength;
            ZoneMonsterStrength = zoneMonsterStrength;
            Presets = presets;
        }
        public List<MonsterContract> Monsters { get; }
        public List<MapObjectContract> MapObjects { get; }
        public List<string> Towns { get; }
        public List<string> GuessableObjects { get; }
        public List<KeyValuePair<string, int>> MapMonsterStrength { get; }
        public List<KeyValuePair<string, int>> ZoneMonsterStrength { get; }
        public List<Preset> Presets { get; }
    }
}
