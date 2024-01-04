using Homm3.Domain.Model.Enums;
using Homm3.Domain.Model.MapObjects;
using Microsoft.AspNetCore.Mvc;
using Homm3.Domain.Model.Monsters;
using Homm3.WebApi.Contracts;
using Homm3.WebApi.Extensions;
using Homm3.Domain.Model.MapPresets;

namespace Homm3.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReferenceDataController
    {
        private readonly IMapObjectFactory mapObjectFactory;
        private readonly IMonsterFactory monsterFactory;
        private readonly IPresetFactory presetFactory;

        public ReferenceDataController(IMapObjectFactory mapObjectFactory, IMonsterFactory monsterFactory, IPresetFactory presetFactory)
        {
            this.mapObjectFactory = mapObjectFactory;
            this.monsterFactory = monsterFactory;
            this.presetFactory = presetFactory;
        }

        [HttpGet]
        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any)]
        public ReferenceDataContract GetReferenceData()
        {
            var monsters = monsterFactory.ListMonsters().Select(x => new MonsterContract
            {
                Name = x.Name,
                Town = x.Town.ToString(),
                Value = x.AiValue,
                Level = x.Level,
                UpgradeLevel = x.UpgradeLevel,
                DisplayName = x.DisplayName
            }).ToList();

            var allMapObjects = mapObjectFactory.ListMapObjects();
            var mapObjects = allMapObjects.Select(x => new MapObjectContract
            {
                Name = x.Name,
                Town = x.ObjectTown.ToString(),
                Value = x.GetAiValue(new Domain.Calculator.ZoneConfiguration { TotalTownZoneCount = 0 }),
                
            }).ToList();

            var guessableObjects = allMapObjects
                .GroupBy(x => x.Visual)
                .Where(x => x.Count() > 1)
                .SelectMany(x => x)
                .Select(x => x.Visual)
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            guessableObjects.Add("ZoneGuard");

            var towns = Enum.GetNames(typeof(Town)).Select((value, key) => value).ToList();
            List<KeyValuePair<string, int>> zoneStrengths = new List<KeyValuePair<string, int>>
            {
                new KeyValuePair<string, int>("ZoneGuard", (int)ZoneStrength.ZoneGuard),
                new KeyValuePair<string, int>("Weak", (int)ZoneStrength.Weak),
                new KeyValuePair<string, int>("Average", (int)ZoneStrength.Average),
                new KeyValuePair<string, int>("Strong", (int)ZoneStrength.Strong),
            };
            var mapMonsterStrengths = EnumHelper.ConvertEnumToKeyValuePair<MapMonsterStrength>();
            var result = new ReferenceDataContract(
                monsters,
                mapObjects,
                towns,
                guessableObjects,
                mapMonsterStrengths,
                zoneStrengths,
                this.presetFactory.GetPresets());
            return result;
        }
    }
}
