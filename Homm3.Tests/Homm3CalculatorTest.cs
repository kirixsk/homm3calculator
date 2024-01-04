using Homm3.Domain.Calculator;
using Homm3.Domain.Model.Enums;
using Homm3.Domain.Model.MapObjects;
using Homm3.Domain.Model.Monsters;

namespace Homm3.Tests
{
    public class Homm3CalculatorTest
    {
        private readonly MonsterFactory monsterFactory;
        private readonly MapObjectFactory objectFactory;
        private readonly Homm3Calculator calculator;

        public Homm3CalculatorTest()
        {
            monsterFactory = new MonsterFactory();
            objectFactory = new MapObjectFactory(monsterFactory, null);
            calculator = new Homm3Calculator(objectFactory);
        }

        //[Fact]
        //public void CalculateMonsterCount_Pandora10KInTresureandArchangels()
        //{
        //    var neclace = MapObjectFactory.GetMapObject("Necklace of Dragonteeth");
        //    var zoneConfiguration = new ZoneConfiguration
        //    {
        //        TotalTownZoneCount = 5,
        //        TownZoneCounts = new Dictionary<Town, int>
        //        {
        //            { Town.Castle, 1 }
        //        }
        //    };
        //    var monster = MonsterFactory.GetMonster("Roc");
        //    var guessedObjects = Homm3Calculator.GuessObject(
        //        MapMonsterStrength.Strong,
        //        ZoneStrength.Strong,
        //        zoneConfiguration,
        //        monster,
        //        new List<MapObject> { neclace },
        //        1,
        //        "Pandora",
        //        89,
        //        89);
        //}

        //[Fact]
        //public void GuessObject_Pandora10KInTresureandRoch()
        //{
        //    var pandora10kBoxObject = MapObjectFactory.GetMapObject("Pandora's Box (10k exp)");
        //    var zoneConfiguration = new ZoneConfiguration
        //    {
        //        TotalTownZoneCount = 5,
        //        TownZoneCounts = new Dictionary<Town, int>
        //        {
        //            { Town.Castle, 1 }
        //        }
        //    };
        //    var monster = MonsterFactory.GetMonster("Cavalier");
        //    var calculatedMonsterValue = Homm3Calculator.CalculateMonsterCount(
        //        MapMonsterStrength.Strong,
        //        ZoneStrength.Strong,
        //        zoneConfiguration,
        //        monster,
        //        new List<MapObject> { pandora10kBoxObject },
        //        3);
        //}

        //[Fact]
        //public void GuessObject_Pandora10KInStartMedusa()
        //{
        //    var zoneConfiguration = new ZoneConfiguration
        //    {
        //        TotalTownZoneCount = 5,
        //        TownZoneCounts = new Dictionary<Town, int>
        //        {
        //            { Town.Castle, 1 }
        //        }
        //    };
        //    var monster = MonsterFactory.GetMonster("Medusa Queen");
        //    var guessedObjects = Homm3Calculator.GuessObject(
        //        MapMonsterStrength.Strong,
        //        ZoneStrength.Weak,
        //        zoneConfiguration,
        //        monster,
        //        new List<MapObject> { },
        //        1,
        //        "Pandora",
        //        54,
        //        54);
        //}

        //[Fact]
        //public void GuessObject_Pandora10KInStartAyssid()
        //{
        //    var zoneConfiguration = new ZoneConfiguration
        //    {
        //        TotalTownZoneCount = 5,
        //        TownZoneCounts = new Dictionary<Town, int>
        //        {
        //            { Town.Castle, 1 }
        //        }
        //    };
        //    var monster = MonsterFactory.GetMonster("Ayssid");
        //    var guessedObjects = Homm3Calculator.GuessObject(
        //        MapMonsterStrength.Strong,
        //        ZoneStrength.Weak,
        //        zoneConfiguration,
        //        monster,
        //        new List<MapObject> { MapObjectFactory.GetMapObject("Ore") },
        //        1,
        //        "Pandora",
        //        44,
        //        44);
        //}

        //[Fact]
        //public void GuessObject_Pandora10KInStartStormBird()
        //{
        //    var zoneConfiguration = new ZoneConfiguration
        //    {
        //        TotalTownZoneCount = 5,
        //        TownZoneCounts = new Dictionary<Town, int>
        //        {
        //            { Town.Castle, 1 }
        //        }
        //    };
        //    var monster = MonsterFactory.GetMonster("Stormbird");
        //    var guessedObjects = Homm3Calculator.GuessObject(
        //        MapMonsterStrength.Strong,
        //        ZoneStrength.Weak,
        //        zoneConfiguration,
        //        monster,
        //        new List<MapObject> { MapObjectFactory.GetMapObject("Windmill") },
        //        1,
        //        "Pandora",
        //        44,
        //        44);
        //}

        //[Fact]
        //public void GuessObject_Pandora10KInStartLeprechaun()
        //{
        //    var zoneConfiguration = new ZoneConfiguration
        //    {
        //        TotalTownZoneCount = 5,
        //        TownZoneCounts = new Dictionary<Town, int>
        //        {
        //            { Town.Castle, 1 }
        //        }
        //    };
        //    var monster = MonsterFactory.GetMonster("Leprechaun");
        //    var guessedObjects = Homm3Calculator.GuessObject(
        //        MapMonsterStrength.Strong,
        //        ZoneStrength.Weak,
        //        zoneConfiguration,
        //        monster,
        //        new List<MapObject> { },
        //        1,
        //        "Pandora",
        //        26,
        //        26);
        //}

        //[Fact]
        //public void GuessObject_MlynCHydra()
        //{
        //    var zoneConfiguration = new ZoneConfiguration
        //    {
        //        TotalTownZoneCount = 10,
        //        TownZoneCounts = new Dictionary<Town, int>
        //        {
        //            { Town.Rampart, 5 }
        //        }
        //    };
        //    var monster = MonsterFactory.GetMonster("Hydra");
        //    var guessedObjects = Homm3Calculator.GuessObject(
        //        MapMonsterStrength.Strong,
        //        ZoneStrength.Strong,
        //        zoneConfiguration,
        //        monster,
        //        new List<MapObject> { MapObjectFactory.GetMapObject("Cyclops Stockpile") },
        //        1,
        //        "Pandora",
        //        16,
        //        16,
        //        Town.Rampart);
        //}

        [Fact]
        public void GuessObject_MlynCStart()
        {
            var zoneConfiguration = new ZoneConfiguration
            {
                TotalTownZoneCount = 14,
                CurrentTownZoneCount = 2
            };
            var monster = monsterFactory.GetMonster("Crusader");
            var guessedObjects = calculator.GuessObject(
                MapMonsterStrength.Strong,
                ZoneStrength.Average,
                zoneConfiguration,
                monster,
                new List<MapObject> {  },
                3,
               "Pandora",
                45,
                45,
                Town.Conflux);
        }

        [Fact]
        public void GuessObject_MlynCStart2()
        {
            var zoneConfiguration = new ZoneConfiguration
            {
                TotalTownZoneCount = 9,
                CurrentTownZoneCount  = 1
            };
            var monster = this.monsterFactory.GetMonster("Troll");
            var guessedObjects = calculator.CalculateMonsterCount(
                MapMonsterStrength.Strong,
                ZoneStrength.Average,
                zoneConfiguration,
                monster,
                new List<MapObject> { objectFactory.GetMapObject("Dragon Fly Hive") },
                3);
        }
    }
}