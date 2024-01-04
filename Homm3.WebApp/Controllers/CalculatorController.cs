using Homm3.Domain.Calculator;
using Homm3.Domain.Model.Enums;
using Homm3.Domain.Model.MapObjects;
using Homm3.Domain.Model.Monsters;
using Homm3.WebApi.Contracts;
using Homm3.WebApp.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Homm3.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly IHomm3Calculator homm3Calculator;
        private readonly IMapObjectFactory mapObjectFactory;
        private readonly IMonsterFactory monsterFactory;

        public CalculatorController(
            IHomm3Calculator homm3Calculator,
            IMapObjectFactory mapObjectFactory,
            IMonsterFactory monsterFactory)
        {
            this.homm3Calculator = homm3Calculator;
            this.mapObjectFactory = mapObjectFactory;
            this.monsterFactory = monsterFactory;
        }

        [HttpPost]
        [Route("GetMonsterValue")]
        public CalculatedMonsterValuesContract GetCalculatedMonsterValues([FromBody] CalculateMonsterValueRequest calculateMonsterValueRequest)
        {
            Enum.TryParse<Town>(calculateMonsterValueRequest.TownName, out var town);
            Enum.TryParse<ZoneStrength>(calculateMonsterValueRequest.ZoneStrengthName, out var zoneStrength);
            Enum.TryParse<MapMonsterStrength>(calculateMonsterValueRequest.MapMonsterStrengthName, out var mapMonsterStrength);
            var monster = this.monsterFactory.GetMonster(calculateMonsterValueRequest.MonsterName);
            var objects = calculateMonsterValueRequest.ObjectNames.Select(x => this.mapObjectFactory.GetMapObject(x)).ToList();
            var zoneConfiguration = new ZoneConfiguration
            {
                TotalTownZoneCount = calculateMonsterValueRequest.TotalTownZoneCount,
                CurrentTownZoneCount = calculateMonsterValueRequest.CurrentTownZoneCount
            };
            var result = this.homm3Calculator.CalculateMonsterCount(
                mapMonsterStrength,
                zoneStrength,
                zoneConfiguration,
                monster,
                objects,
                calculateMonsterValueRequest.Week,
                town);

            return new CalculatedMonsterValuesContract
            {
                AverageMonsterCount = result.AverageMonsterCount,
                MaximalCount = result.MaximalCount,
                MinimalCount = result.MinimalCount,
                Monster = new MonsterContract
                {
                    Level = result.Monster.Level,
                    Name = result.Monster.Name,
                    Town = result.Monster.Town.ToString(),
                    UpgradeLevel = result.Monster.UpgradeLevel,
                    Value = result.Monster.AiValue
                },
                MonsterCountDeviation = result.MonsterCountDeviation
            };
        }

        [HttpPost]
        [Route("GuessObject")]
        public List<GuessedObjectResult> GuessObject([FromBody] GuessMapObjectRequest guessMapObjectRequest)
        {
            Enum.TryParse<Town>(guessMapObjectRequest.TownName, out var town);
            Enum.TryParse<ZoneStrength>(guessMapObjectRequest.ZoneStrengthName, out var zoneStrength);
            Enum.TryParse<MapMonsterStrength>(guessMapObjectRequest.MapMonsterStrengthName, out var mapMonsterStrength);
            var monster = this.monsterFactory.GetMonster(guessMapObjectRequest.MonsterName);
            var objects = guessMapObjectRequest.ObjectNames.Select(x => this.mapObjectFactory.GetMapObject(x)).ToList();
            var zoneConfiguration = new ZoneConfiguration
            {
                TotalTownZoneCount = guessMapObjectRequest.TotalTownZoneCount,
                CurrentTownZoneCount = guessMapObjectRequest.CurrentTownZoneCount
            };
            var result = this.homm3Calculator.GuessObject(
                mapMonsterStrength,
                zoneStrength,
                zoneConfiguration,
                monster,
                objects,
                guessMapObjectRequest.Week,
                guessMapObjectRequest.Visual,
                guessMapObjectRequest.MinMonsterCount,
                guessMapObjectRequest.MaxMonsterCount,
                town);
            return result;
        }
    }
}
