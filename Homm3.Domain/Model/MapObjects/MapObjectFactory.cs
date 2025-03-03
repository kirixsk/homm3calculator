﻿using Homm3.Domain.Model.Monsters;

namespace Homm3.Domain.Model.MapObjects
{
    public class MapObjectFactory : IMapObjectFactory
    {
        private readonly List<MapObject> mapObjects;
        private readonly Dictionary<string, MapObject> mapObjectDictionary;
        private readonly IMonsterFactory monsterFactory;
        private readonly string ZoneGuardPrefix = "ZoneGuard";

        public MapObjectFactory(IMonsterFactory monsterFactory, IObjectWithMonsterRewardFactory objectWithMonsterRewardFactory)
        {
            this.monsterFactory = monsterFactory;
            mapObjects = new List<MapObject>();
            mapObjects.AddRange(ListAdventureMapObjects());
            mapObjects.AddRange(ListArtifacts());
            mapObjects.AddRange(ListDwellings());
            mapObjects.AddRange(objectWithMonsterRewardFactory.GenerateObjectsWithMonstersRewards());
            mapObjects.Sort();
            mapObjectDictionary = mapObjects.ToDictionary(x => x.Name);
        }

        public MapObject GetMapObject(string name)
        {
            if (name.StartsWith(this.ZoneGuardPrefix))
            {
                var numberStringAiValue = name.Replace(this.ZoneGuardPrefix, "");
                var numberIntAiValue = Int32.TryParse(numberStringAiValue, out int guardAiValue) ? guardAiValue : 0;
                return new MapObject("ZoneGuard", numberIntAiValue);
            }
            return mapObjectDictionary[name];
        }

        public List<MapObject> ListMapObjects()
        {
            return mapObjects.ToList();
        }

        private IEnumerable<MapObject> ListAdventureMapObjects()
        {
            yield return new MapObject("Alchemist's Lab", 3500);
            yield return new MapObject("Altar of Mana", 100);
            yield return new MapObject("Altar of Sacrifice", 100);
            yield return new MapObject("Ancient Lamp", 5000);
            yield return new MapObject("Arena", 3000);
            yield return new MapObject("Beholders' Sanctuary", 2500);
            yield return new MapObject("Black Market", 8000);
            yield return new MapObject("Black Tower", 1500);
            yield return new MapObject("Buoy", 100);
            yield return new MapObject("Campfire", 2000);
            yield return new MapObject("Cannon Yard", 3000);
            yield return new MapObject("Cartographer (Ground)", 10000);
            yield return new MapObject("Cartographer (Underground)", 7500);
            yield return new MapObject("Cartographer (Water)", 5000);
            yield return new MapObject("Churchyard", 1500);
            yield return new MapObject("Colosseum of the Magi", 3000);
            yield return new MapObject("Corpse", 500);
            yield return new MapObject("Cover of Darkness", 500);
            yield return new MapObject("Crypt", 1000);
            yield return new MapObject("Crystal", 2000);
            yield return new MapObject("Crystal Cavern", 3500);
            yield return new MapObject("Cyclops Stockpile", 3000);
            yield return new MapObject("Den of Thieves", 100);
            yield return new MapObject("Derelict Ship", 4000);
            yield return new MapObject("Derrick", 750);
            yield return new MapObject("Dragon Fly Hive", 9000);
            yield return new MapObject("Dragon Utopia", 10000);
            yield return new MapObject("Dwarven Treasury", 2000);
            yield return new MapObject("Experimental Shop", 3500);
            yield return new MapObject("Eye of the Magi", 100);
            yield return new MapObject("Faerie Ring", 100);
            yield return new MapObject("Flotsam", 500);
            yield return new MapObject("Fountain of Fortune", 100);
            yield return new MapObject("Fountain of Youth", 100);
            yield return new MapObject("Freelancer's Guild", 100);
            yield return new MapObject("Garden of Revelation", 1500);
            yield return new MapObject("Gazebo", 1500);
            yield return new MapObject("Gems", 2000);
            yield return new MapObject("Gem Pond", 3500);
            yield return new MapObject("Gold", 750);
            yield return new MapObject("Gold Mine", 7000);
            yield return new MapObject("Grave", 500);
            yield return new MapObject("Griffin Conservatory", 2000);
            yield return new MapObject("Hermit's Shack", 1500);
            yield return new MapObject("Hill Fort (Old or New)", 7000);
            yield return new MapObject("Hut of the Magi", 100);
            yield return new MapObject("Idol of Fortune", 100);
            yield return new MapObject("Imp Cache", 1500);
            yield return new MapObject("Ivory Tower", 7000);
            yield return new MapObject("Jetsam", 500);
            yield return new MapObject("Junkman", 200);
            yield return new MapObject("Lean To", 500);
            yield return new MapObject("Learning Stone", 1500);
            yield return new MapObject("Library of Enlightenment", 12000);
            yield return new MapObject("Magic Spring", 500);
            yield return new MapObject("Magic Well", 250);
            yield return new MapObject("Major Artifact", 10000);
            yield return new MapObject("Mansion", 5000);
            yield return new MapObject("Marletto Tower", 1500);
            yield return new MapObject("Medusa Stores", 1500);
            yield return new MapObject("Mercenary Camp", 1500);
            yield return new MapObject("Mercury", 2000);
            yield return new MapObject("Mermaids", 100);
            yield return new MapObject("Mineral Spring", 500);
            yield return new MapObject("Minor Artifact", 5000);
            yield return new MapObject("Mystical Garden", 500);
            yield return new MapObject("Naga Bank", 3000);
            yield return new MapObject("Oasis", 100);
            yield return new MapObject("Obelisk", 350);
            yield return new MapObject("Observatory", 500);
            yield return new MapObject("Observation Tower", 750);
            yield return new MapObject("Ore", 1400);
            yield return new MapObject("Ore Pit", 1500);
            yield return new MapObject("Pandora's Box (5k exp)", 6000, "Pandora's Box exp 05k", "Pandora", 20);
            yield return new MapObject("Pandora's Box (10k exp)", 12000, "Pandora's Box exp 10k", "Pandora", 20);
            yield return new MapObject("Pandora's Box (15k exp)", 18000, "Pandora's Box exp 15k", "Pandora", 20);
            yield return new MapObject("Pandora's Box (20k exp)", 24000, "Pandora's Box exp 20k", "Pandora", 20);
            yield return new MapObject("Pandora's Box (5k gold)", 5000, "Pandora's Box gold 05k", "Pandora", 5);
            yield return new MapObject("Pandora's Box (10k gold)", 10000, "Pandora's Box gold 10k", "Pandora", 5); ;
            yield return new MapObject("Pandora's Box (15k gold)", 15000, "Pandora's Box gold 15k", "Pandora", 5);
            yield return new MapObject("Pandora's Box (20k gold)", 20000, "Pandora's Box gold 20k", "Pandora", 5);
            yield return new MapObject("Pandora's Box (1st lvl spells)", 5000, "Pandora's Box spell 1", "Pandora", 2);
            yield return new MapObject("Pandora's Box (2nd lvl spells)", 7500, "Pandora's Box spell 2", "Pandora", 2);
            yield return new MapObject("Pandora's Box (3rd lvl spells)", 10000, "Pandora's Box spell 3", "Pandora", 2);
            yield return new MapObject("Pandora's Box (4th lvl spells)", 12500, "Pandora's Box spell 4", "Pandora", 2);
            yield return new MapObject("Pandora's Box (5th lvl spells)", 15000, "Pandora's Box spell 5", "Pandora", 2);
            yield return new MapObject("Pandora's Box (all air spells)", 15000, "Pandora's Box spell all air", "Pandora", 2);
            yield return new MapObject("Pandora's Box (all earth spells)", 15000, "Pandora's Box spell all earth", "Pandora", 2);
            yield return new MapObject("Pandora's Box (all fire spells)", 15000, "Pandora's Box spell all fire", "Pandora", 2);
            yield return new MapObject("Pandora's Box (all water spells)", 15000, "Pandora's Box spell all water", "Pandora", 2);
            yield return new MapObject("Pandora's Box (all spells)", 30000, "Pandora's Box spell full all", "Pandora", 2);
            yield return new MapObject("Pillar of Fire", 750);
            yield return new MapObject("Pirate Cavern", 3500);
            yield return new MapObject("Plate of Dying Light", 20000);
            yield return new MapObject("Prison (lvl 1 hero)", 2500, "Prison 01", "Prison");
            yield return new MapObject("Prison (lvl 5 hero)", 5000, "Prison 05", "Prison");
            yield return new MapObject("Prison (lvl 10 hero)", 10000, "Prison 10", "Prison");
            yield return new MapObject("Prison (lvl 20 hero)", 20000, "Prison 20", "Prison");
            yield return new MapObject("Prison (lvl 30 hero)", 30000, "Prison 30", "Prison");
            yield return new MapObject("Prospector", 500);
            yield return new MapObject("Pyramid", 5000);
            yield return new MapObject("Rally Flag", 100);
            yield return new MapObject("Random Resource", 1500);
            yield return new MapObject("Redwood Observatory", 750);
            yield return new MapObject("Red Tower", 4000);
            yield return new MapObject("Refugee Camp", 5000);
            yield return new MapObject("Relic Artifact", 20000);
            yield return new MapObject("Ruins", 1000);
            yield return new MapObject("Sanctuary", 100);
            yield return new MapObject("Sawmill", 1500);
            yield return new MapObject("Scholar", 1500);
            yield return new MapObject("School of Magic", 1000);
            yield return new MapObject("School of War", 1000);
            yield return new MapObject("Sea Barrel", 500);
            yield return new MapObject("Sea Chest", 1500);
            yield return new MapObject("Seafaring Academy", 8000);
            yield return new MapObject("Seal of Sunset", 5000);
            yield return new MapObject("Seer's Hut (5k exp)", 2000, "Seer's Hut exp 05", "Quest", 10);
            yield return new MapObject("Seer's Hut (10k exp)", 5333, "Seer's Hut exp 10", "Quest", 10);
            yield return new MapObject("Seer's Hut (15k exp)", 8666, "Seer's Hut exp 15", "Quest", 10);
            yield return new MapObject("Seer's Hut (20k exp)", 12000, "Seer's Hut exp 20", "Quest", 10);
            yield return new MapObject("Seer's Hut (5k gold)", 2000, "Seer's Hut gold 05", "Quest", 10);
            yield return new MapObject("Seer's Hut (10k gold)", 5333, "Seer's Hut gold 10", "Quest", 10);
            yield return new MapObject("Seer's Hut (15k gold)", 8666, "Seer's Hut gold 15", "Quest", 10);
            yield return new MapObject("Seer's Hut (20k gold)", 12000, "Seer's Hut gold 20", "Quest", 10);
            yield return new MapObject("Shipwreck Survivor", 1500);
            yield return new MapObject("Shipwreck", 2000);
            yield return new MapObject("Shrine of Magic Incantation (1st lvl spell)", 500, "Shrine of Magic 1");
            yield return new MapObject("Shrine of Magic Gesture (2nd lvl spell)", 2000, "Shrine of Magic 2");
            yield return new MapObject("Shrine of Magic Mystery (4th lvl spell)", 7000, "Shrine of Magic 4");
            yield return new MapObject("Shrine of Magic Thought (3rd lvl spell)", 3000, "Shrine of Magic 3");
            yield return new MapObject("Skeleton Transformer", 500);
            yield return new MapObject("Spell Scroll lvl 1", 500, "Spell Scroll lvl 1", "Spell");
            yield return new MapObject("Spell Scroll lvl 2", 2000, "Spell Scroll lvl 2", "Spell");
            yield return new MapObject("Spell Scroll lvl 3", 3000, "Spell Scroll lvl 3", "Spell");
            yield return new MapObject("Spell Scroll lvl 4 (exc. TP and Water Walk)", 8000, "Spell Scroll lvl 4", "Spell");
            yield return new MapObject("Spell Scroll lvl 5 (exc. DD and Fly)", 10000, "Spell Scroll lvl 5", "Spell");
            yield return new MapObject("Spell Scroll (TP, Water Walk, DD or Fly)", 20000, "Spell Scroll lvl air", "Spell");
            yield return new MapObject("Spit", 1500);
            yield return new MapObject("Stables", 200);
            yield return new MapObject("Star Axis", 1500);
            yield return new MapObject("Sulfur", 2000);
            yield return new MapObject("Sulfur Dune", 3500);
            yield return new MapObject("Swan Pond", 100);
            yield return new MapObject("Tavern", 100);
            yield return new MapObject("Temple of Loyalty", 100);
            yield return new MapObject("Temple of the Sea", 10000);
            yield return new MapObject("Temple", 100);
            yield return new MapObject("Town Gate", 10000);
            yield return new MapObject("Trading Post", 3000);
            yield return new MapObject("Trailblazer", 200);
            yield return new MapObject("Treasure Artifact", 2000);
            yield return new MapObject("Treasure Chest", 1500);
            yield return new MapObject("Tree of Knowledge", 2500);
            yield return new MapObject("University", 2500);
            yield return new MapObject("Vial of Mana", 3000);
            yield return new MapObject("Wagon", 500);
            yield return new MapObject("War Machine Factory", 1500);
            yield return new MapObject("Warehouse (Crystal)", 2500);
            yield return new MapObject("Warehouse (Gems)", 2500);
            yield return new MapObject("Warehouse (Gold)", 6000);
            yield return new MapObject("Warehouse (Mercury)", 2500);
            yield return new MapObject("Warehouse (Ore)", 2250);
            yield return new MapObject("Warehouse (Sulfur)", 2500);
            yield return new MapObject("Warehouse (Wood)", 2250);
            yield return new MapObject("Warlock's Lab", 10000);
            yield return new MapObject("Warrior's Tomb", 6000);
            yield return new MapObject("Water Wheel", 750);
            yield return new MapObject("Watering Hole", 500);
            yield return new MapObject("Watering Place", 500);
            yield return new MapObject("Windmill", 2500);
            yield return new MapObject("Witch Hut", 1500);
            yield return new MapObject("Wolf Raider Picket", 9500);
            yield return new MapObject("Wood", 1400);
        }

        private static IEnumerable<MapObject> ListArtifacts()
        {
            yield return new MapObject("Amulet of the Undertaker", 2000);
            yield return new MapObject("Badge of Courage", 2000);
            yield return new MapObject("Bird of Perception", 2000);
            yield return new MapObject("Bow of Elven Cherrywood", 2000);
            yield return new MapObject("Breastplate of Petrified Wood", 2000);
            yield return new MapObject("Cape of Conjuring", 2000);
            yield return new MapObject("Cards of Prophecy", 2000);
            yield return new MapObject("Centaur's Axe", 2000);
            yield return new MapObject("Charm of Mana", 2000);
            yield return new MapObject("Clover of Fortune", 2000);
            yield return new MapObject("Collar of Conjuring", 2000);
            yield return new MapObject("Crest of Valor", 2000);
            yield return new MapObject("Dragonbone Greaves", 2000);
            yield return new MapObject("Glyph of Gallantry", 2000);
            yield return new MapObject("Helm of the Alabaster Unicorn", 2000);
            yield return new MapObject("Hourglass of the Evil Hour", 2000);
            yield return new MapObject("Ladybird of Luck", 2000);
            yield return new MapObject("Legs of Legion", 2000);
            yield return new MapObject("Mystic Orb of Mana", 2000);
            yield return new MapObject("Necklace of Swiftness", 2000);
            yield return new MapObject("Pendant of Death", 2000);
            yield return new MapObject("Pendant of Dispassion", 2000);
            yield return new MapObject("Pendant of Free Will", 2000);
            yield return new MapObject("Pendant of Holiness", 2000);
            yield return new MapObject("Pendant of Life", 2000);
            yield return new MapObject("Pendant of Total Recall", 2000);
            yield return new MapObject("Quiet Eye of the Dragon", 2000);
            yield return new MapObject("Ring of Conjuring", 2000);
            yield return new MapObject("Ring of Vitality", 2000);
            yield return new MapObject("Shield of the Dwarven Lords", 2000);
            yield return new MapObject("Skull Helmet", 2000);
            yield return new MapObject("Speculum", 2000);
            yield return new MapObject("Spirit of Oppression", 2000);
            yield return new MapObject("Spyglass", 2000);
            yield return new MapObject("Still Eye of the Dragon", 2000);
            yield return new MapObject("Stoic Watchman", 2000);
            yield return new MapObject("Talisman of Mana", 2000);
            yield return new MapObject("Demon's Horseshoe", 2000);
            yield return new MapObject("Ring of Suppression", 2000);
            yield return new MapObject("Runes of Imminency", 2000);
            yield return new MapObject("Armor of Wonder", 5000);
            yield return new MapObject("Blackshard of the Dead Knight", 5000);
            yield return new MapObject("Boots of Speed", 5000);
            yield return new MapObject("Bowstring of the Unicorn's Mane", 5000);
            yield return new MapObject("Buckler of the Gnoll King", 5000);
            yield return new MapObject("Crown of the Supreme Magi", 5000);
            yield return new MapObject("Dragon Wing Tabard", 5000);
            yield return new MapObject("Emblem of Cognizance", 5000);
            yield return new MapObject("Equestrian's Gloves", 5000);
            yield return new MapObject("Greater Gnoll's Flail", 5000);
            yield return new MapObject("Helm of Chaos", 5000);
            yield return new MapObject("Inexhaustible Cart of Ore", 5000);
            yield return new MapObject("Inexhaustible Cart of Lumber", 5000);
            yield return new MapObject("Loins of Legion", 5000);
            yield return new MapObject("Red Dragon Flame Tongue", 5000);
            yield return new MapObject("Rib Cage", 5000);
            yield return new MapObject("Ring of Life", 5000);
            yield return new MapObject("Scales of the Greater Basilisk", 5000);
            yield return new MapObject("Shield of the Yawning Dead", 5000);
            yield return new MapObject("Torso of Legion", 5000);
            yield return new MapObject("Vampire's Cowl", 5000);
            yield return new MapObject("Hideous Mask", 5000);
            yield return new MapObject("Shaman's Puppet", 5000);
            yield return new MapObject("Ambassador's Sash", 10000);
            yield return new MapObject("Angel Feather Arrows", 10000);
            yield return new MapObject("Arms of Legion", 10000);
            yield return new MapObject("Breastplate of Brimstone", 10000);
            yield return new MapObject("Cape of Velocity", 10000);
            yield return new MapObject("Dead Man's Boots", 10000);
            yield return new MapObject("Diplomat's Ring", 10000);
            yield return new MapObject("Dragon Scale Shield", 10000);
            yield return new MapObject("Endless Bag of Gold", 10000);
            yield return new MapObject("Endless Purse of Gold", 10000);
            yield return new MapObject("Everflowing Crystal Cloak", 10000);
            yield return new MapObject("Everpouring Vial of Mercury", 10000);
            yield return new MapObject("Eversmoking Ring of Sulfur", 10000);
            yield return new MapObject("Garniture of Interference", 10000);
            yield return new MapObject("Golden Bow", 10000);
            yield return new MapObject("Head of Legion", 10000);
            yield return new MapObject("Hellstorm Helmet", 10000);
            yield return new MapObject("Necklace of Dragonteeth", 10000);
            yield return new MapObject("Necklace of Ocean Guidance", 10000);
            yield return new MapObject("Ogre's Club of Havoc", 10000);
            yield return new MapObject("Orb of Driving Rain", 10000);
            yield return new MapObject("Orb of the Firmament", 10000);
            yield return new MapObject("Orb of Silt", 10000);
            yield return new MapObject("Orb of Tempestuous Fire", 10000);
            yield return new MapObject("Pendant of Courage", 10000);
            yield return new MapObject("Pendant of Negativity", 10000);
            yield return new MapObject("Pendant of Second Sight", 10000);
            yield return new MapObject("Recanter's Cloak", 10000);
            yield return new MapObject("Ring of Infinite Gems", 10000);
            yield return new MapObject("Ring of the Wayfarer", 10000);
            yield return new MapObject("Shackles of War", 10000);
            yield return new MapObject("Shield of the Damned", 10000);
            yield return new MapObject("Sphere of Permanence", 10000);
            yield return new MapObject("Statesman's Medal", 10000);
            yield return new MapObject("Surcoat of Counterpoise", 10000);
            yield return new MapObject("Sword of Hellfire", 10000);
            yield return new MapObject("Targ of the Rampaging Ogre", 10000);
            yield return new MapObject("Tunic of the Cyclops King", 10000);
            yield return new MapObject("Vial of Lifeblood", 10000);
            yield return new MapObject("Cape of Silence", 10000);
            yield return new MapObject("Crown of the Five Seas", 10000);
            yield return new MapObject("Pendant of Downfall", 10000);
            yield return new MapObject("Ring of Oblivion", 10000);
            yield return new MapObject("Royal Armor of Nix", 10000);
            yield return new MapObject("Shield of Naval Glory", 10000);
            yield return new MapObject("Trident of Dominion", 10000);
            yield return new MapObject("Wayfarer's Boots", 10000);
            yield return new MapObject("Angel Wings", 20000);
            yield return new MapObject("Armageddon's Blade", 20000);
            yield return new MapObject("Boots of Levitation", 20000);
            yield return new MapObject("Boots of Polarity", 20000);
            yield return new MapObject("Celestial Necklace of Bliss", 20000);
            yield return new MapObject("Crown of Dragontooth", 20000);
            yield return new MapObject("Dragon Scale Armor", 20000);
            yield return new MapObject("Endless Sack of Gold", 20000);
            yield return new MapObject("Helm of Heavenly Enlightenment", 20000);
            yield return new MapObject("Lion's Shield of Courage", 20000);
            yield return new MapObject("Orb of Vulnerability", 20000);
            yield return new MapObject("Orb of Inhibition", 20000);
            yield return new MapObject("Sandals of the Saint", 20000);
            yield return new MapObject("Sea Captain's Hat", 20000);
            yield return new MapObject("Sentinel's Shield", 20000);
            yield return new MapObject("Spellbinder's Hat", 20000);
            yield return new MapObject("Sword of Judgement", 20000);
            yield return new MapObject("Thunder Helmet", 20000);
            yield return new MapObject("Titan's Cuirass", 20000);
            yield return new MapObject("Titan's Gladius", 20000);
            yield return new MapObject("Tome of Air", 20000);
            yield return new MapObject("Tome of Earth", 20000);
            yield return new MapObject("Tome of Fire", 20000);
            yield return new MapObject("Tome of Water", 20000);
            yield return new MapObject("Vial of Dragon Blood", 20000);
            yield return new MapObject("Horn of the Abyss", 20000);
            yield return new MapObject("Admiral's Hat", 20000);
            yield return new MapObject("Angelic Alliance", 20000);
            yield return new MapObject("Armor of the Damned", 20000);
            yield return new MapObject("Bow of the Sharpshooter", 20000);
            yield return new MapObject("Cloak of the Undead King", 20000);
            yield return new MapObject("Cornucopia", 20000);
            yield return new MapObject("Elixir of Life", 20000);
            yield return new MapObject("Power of the Dragon Father", 20000);
            yield return new MapObject("Ring of the Magi", 20000);
            yield return new MapObject("Statue of Legion", 20000);
            yield return new MapObject("Titan's Thunder", 20000);
            yield return new MapObject("Wizard's Well", 20000);
            yield return new MapObject("Diplomat's Cloak", 20000);
            yield return new MapObject("Golden Goose", 20000);
            yield return new MapObject("Ironfist of the Ogre ", 20000);
            yield return new MapObject("Pendant of Reflection", 20000);
        }

        private IEnumerable<DwellingMapObject> ListDwellings()
        {
            yield return new DwellingMapObject("Guardhouse (Pikeman)", this.monsterFactory.GetMonster("Pikeman"));
            yield return new DwellingMapObject("Archers' Tower (Archer)", this.monsterFactory.GetMonster("Archer"));
            yield return new DwellingMapObject("Griffin Tower (Griffint)", this.monsterFactory.GetMonster("Griffin"));
            yield return new DwellingMapObject("Barracks (Swordsman)", this.monsterFactory.GetMonster("Swordsman"));
            yield return new DwellingMapObject("Monastery (Monk)", this.monsterFactory.GetMonster("Monk"));
            yield return new DwellingMapObject("Training Grounds (Cavalier)", this.monsterFactory.GetMonster("Cavalier"));
            yield return new DwellingMapObject("Portal of Glory (Angel)", this.monsterFactory.GetMonster("Angel"));
            yield return new DwellingMapObject("Centaur Stables (Centaur)", this.monsterFactory.GetMonster("Centaur"));
            yield return new DwellingMapObject("Dwarf Cottage (Dwarf)", this.monsterFactory.GetMonster("Dwarf"));
            yield return new DwellingMapObject("Homestead (Wood Elf)", this.monsterFactory.GetMonster("Wood Elf"));
            yield return new DwellingMapObject("Enchanted Spring (Pegasus)", this.monsterFactory.GetMonster("Pegasus"));
            yield return new DwellingMapObject("Dendroid Arches (Dendroid)", this.monsterFactory.GetMonster("Dendroid Guard"));
            yield return new DwellingMapObject("Unicorn Glade (Unicorn)", this.monsterFactory.GetMonster("Unicorn"));
            yield return new DwellingMapObject("Dragon Cliffs (Green Dragon)", this.monsterFactory.GetMonster("Green Dragon"));
            yield return new DwellingMapObject("Workshop (Gremlin)", this.monsterFactory.GetMonster("Gremlin"));
            yield return new DwellingMapObject("Parapet (Stone Gargoyle)", this.monsterFactory.GetMonster("Stone Gargoyle"));
            yield return new DwellingMapObject("Golem Factory (Golem)", this.monsterFactory.GetMonster("Stone Golem"), this.monsterFactory.GetMonster("Iron Golem"), this.monsterFactory.GetMonster("Gold Golem"), this.monsterFactory.GetMonster("Diamond Golem"));
            yield return new DwellingMapObject("Mage Tower (Mage)", this.monsterFactory.GetMonster("Mage"));
            yield return new DwellingMapObject("Altar of Wishes (Genie)", this.monsterFactory.GetMonster("Genie"));
            yield return new DwellingMapObject("Golden Pavilion (Naga)", this.monsterFactory.GetMonster("Naga"));
            yield return new DwellingMapObject("Cloud Temple (Giant)", this.monsterFactory.GetMonster("Giant"));
            yield return new DwellingMapObject("Imp Crucible (Imp)", this.monsterFactory.GetMonster("Imp"));
            yield return new DwellingMapObject("Hall of Sins (Gog)", this.monsterFactory.GetMonster("Gog"));
            yield return new DwellingMapObject("Kennels (Hell Hound)", this.monsterFactory.GetMonster("Hell Hound"));
            yield return new DwellingMapObject("Demon Gate (Demon)", this.monsterFactory.GetMonster("Demon"));
            yield return new DwellingMapObject("Hell Hole (Pit Fiend)", this.monsterFactory.GetMonster("Pit Fiend"));
            yield return new DwellingMapObject("Fire Lake (Efreet)", this.monsterFactory.GetMonster("Efreet"));
            yield return new DwellingMapObject("Forsaken Palace (Devil)", this.monsterFactory.GetMonster("Devil"));
            yield return new DwellingMapObject("Cursed Temple (Skeleton)", this.monsterFactory.GetMonster("Skeleton"));
            yield return new DwellingMapObject("Graveyard (Walking Dead)", this.monsterFactory.GetMonster("Walking Dead"));
            yield return new DwellingMapObject("Tomb of Souls (Wight)", this.monsterFactory.GetMonster("Wight"));
            yield return new DwellingMapObject("Estate (Vampire)", this.monsterFactory.GetMonster("Vampire")) { ExtraMultiplier = 3 };
            yield return new DwellingMapObject("Mausoleum (Lich)", this.monsterFactory.GetMonster("Lich"));
            yield return new DwellingMapObject("Hall of Darkness (Black Knight)", this.monsterFactory.GetMonster("Black Knight"));
            yield return new DwellingMapObject("Dragon Vault (Bone Dragon)", this.monsterFactory.GetMonster("Bone Dragon"));
            yield return new DwellingMapObject("Warren (Troglodyte)", this.monsterFactory.GetMonster("Troglodyte"));
            yield return new DwellingMapObject("Harpy Loft (Harpy)", this.monsterFactory.GetMonster("Harpy"));
            yield return new DwellingMapObject("Pillar of Eyes (Beholder)", this.monsterFactory.GetMonster("Beholder"));
            yield return new DwellingMapObject("Chapel of Stilled Voices (Medusa)", this.monsterFactory.GetMonster("Medusa"));
            yield return new DwellingMapObject("Labyrinth (Minotaur)", this.monsterFactory.GetMonster("Minotaur"));
            yield return new DwellingMapObject("Manticore Lair (Manticore)", this.monsterFactory.GetMonster("Manticore"));
            yield return new DwellingMapObject("Dragon Cave (Red Dragon)", this.monsterFactory.GetMonster("Red Dragon"));
            yield return new DwellingMapObject("Goblin Barracks (Goblin)", this.monsterFactory.GetMonster("Goblin"));
            yield return new DwellingMapObject("Wolf Pen (Wolf Rider)", this.monsterFactory.GetMonster("Wolf Rider"));
            yield return new DwellingMapObject("Orc Tower (Orc)", this.monsterFactory.GetMonster("Orc"));
            yield return new DwellingMapObject("Ogre Fort (Ogre)", this.monsterFactory.GetMonster("Ogre"));
            yield return new DwellingMapObject("Cliff Nest (Roc)", this.monsterFactory.GetMonster("Roc"));
            yield return new DwellingMapObject("Cyclops Cave (Cyclops)", this.monsterFactory.GetMonster("Cyclops"));
            yield return new DwellingMapObject("Behemoth Crag (Behemoth)", this.monsterFactory.GetMonster("Behemoth"));
            yield return new DwellingMapObject("Gnoll Hut (Gnoll)", this.monsterFactory.GetMonster("Gnoll"));
            yield return new DwellingMapObject("Lizard Den (Lizardman)", this.monsterFactory.GetMonster("Lizardman"));
            yield return new DwellingMapObject("Serpent Fly Hive (Serpent Fly)", this.monsterFactory.GetMonster("Serpent Fly"));
            yield return new DwellingMapObject("Basilisk Pit (Basilisk)", this.monsterFactory.GetMonster("Basilisk"));
            yield return new DwellingMapObject("Gorgon Lair (Gorgon)", this.monsterFactory.GetMonster("Gorgon"));
            yield return new DwellingMapObject("Wyvern Nest (Wyvern)", this.monsterFactory.GetMonster("Wyvern"));
            yield return new DwellingMapObject("Hydra Pond (Hydra)", this.monsterFactory.GetMonster("Hydra"));
            yield return new DwellingMapObject("Magic Lantern (Pixie)", this.monsterFactory.GetMonster("Pixie"));
            yield return new DwellingMapObject("Altar of Air (Air Elem.)", this.monsterFactory.GetMonster("Air Elemental"));
            yield return new DwellingMapObject("Air Elemental Conflux (Air Elem.)", this.monsterFactory.GetMonster("Air Elemental"));
            yield return new DwellingMapObject("Altar of Water (Water Elem.)", this.monsterFactory.GetMonster("Water Elemental"));
            yield return new DwellingMapObject("Water Elemental Conflux (Water Elem.)", this.monsterFactory.GetMonster("Water Elemental"));
            yield return new DwellingMapObject("Altar of Fire (Fire Elem.)", this.monsterFactory.GetMonster("Fire Elemental"));
            yield return new DwellingMapObject("Fire Elemental Conflux (Fire Elem.)", this.monsterFactory.GetMonster("Fire Elemental"));
            yield return new DwellingMapObject("Altar of Earth (Earth Elem.)", this.monsterFactory.GetMonster("Earth Elemental"));
            yield return new DwellingMapObject("Earth Elemental Conflux (Earth Elem.)", this.monsterFactory.GetMonster("Earth Elemental"));
            yield return new DwellingMapObject("Elemental Conflux (Multiple Elem.)", this.monsterFactory.GetMonster("Air Elemental"), this.monsterFactory.GetMonster("Water Elemental"), this.monsterFactory.GetMonster("Fire Elemental"), this.monsterFactory.GetMonster("Earth Elemental"));
            yield return new DwellingMapObject("Altar of Thought (Psychic Elem.)", this.monsterFactory.GetMonster("Psychic Elemental"));
            yield return new DwellingMapObject("Pyre (Firebird)", this.monsterFactory.GetMonster("Firebird"));
            yield return new DwellingMapObject("Nymph Waterfall (Nymph)", this.monsterFactory.GetMonster("Nymph"));
            yield return new DwellingMapObject("Shack (Crew Mate)", this.monsterFactory.GetMonster("Crew Mate"));
            yield return new DwellingMapObject("Frigate (Pirate)", this.monsterFactory.GetMonster("Pirate"));
            yield return new DwellingMapObject("Nest (Stormbird)", this.monsterFactory.GetMonster("Stormbird"));
            yield return new DwellingMapObject("Tower of the Seas (Sea Witch)", this.monsterFactory.GetMonster("Sea Witch"));
            yield return new DwellingMapObject("Nix Fort (Nix)", this.monsterFactory.GetMonster("Nix"));
            yield return new DwellingMapObject("Maelstrom (Sea Serpent)", this.monsterFactory.GetMonster("Sea Serpent"));
            yield return new DwellingMapObject("Hovel (Peasant)", this.monsterFactory.GetMonster("Peasant")) { ExtraMultiplier = 3 };
            yield return new DwellingMapObject("Thatched Hut (Halfling)", this.monsterFactory.GetMonster("Halfling"));
            yield return new DwellingMapObject("Rogue Cavern (Rogue)", this.monsterFactory.GetMonster("Rogue"));
            yield return new DwellingMapObject("Alehouse (Leprechaun)", this.monsterFactory.GetMonster("Leprechaun"));
            yield return new DwellingMapObject("Boar Glen (Boar)", this.monsterFactory.GetMonster("Boar"));
            yield return new DwellingMapObject("Nomad Tent (Nomad)", this.monsterFactory.GetMonster("Nomad"));
            yield return new DwellingMapObject("Tomb of Curses (Mummy)", this.monsterFactory.GetMonster("Mummy"));
            yield return new DwellingMapObject("Wineyard (Satyr)", this.monsterFactory.GetMonster("Satyr"));
            yield return new DwellingMapObject("Treetop Tower (Sharpshooter)", this.monsterFactory.GetMonster("Sharpshooter"));
            yield return new DwellingMapObject("Troll Bridge (Troll)", this.monsterFactory.GetMonster("Troll"));
            yield return new DwellingMapObject("Ziggurat (Fangarm)", this.monsterFactory.GetMonster("Fangarm"));
            yield return new DwellingMapObject("Enchanter's Hollow (Enchanter)", this.monsterFactory.GetMonster("Enchanter"));
            yield return new DwellingMapObject("Magic Forest (Faerie Dragon)", this.monsterFactory.GetMonster("Faerie Dragon"));
            yield return new DwellingMapObject("Sulfurous Lair (Rust Dragon)", this.monsterFactory.GetMonster("Rust Dragon"));
            yield return new DwellingMapObject("Crystal Cave (Crystal Dragon)", this.monsterFactory.GetMonster("Crystal Dragon"));
            yield return new DwellingMapObject("Frozen Cliffs (Azure Dragon)", this.monsterFactory.GetMonster("Azure Dragon"));


            yield return new DwellingMapObject("Halfling Adobe (Halfling)", this.monsterFactory.GetMonster("Halfling (Factory)"));
            yield return new DwellingMapObject("Foundry (Mechanic)", this.monsterFactory.GetMonster("Mechanic"));
            yield return new DwellingMapObject("Ranch (Armadillo)", this.monsterFactory.GetMonster("Armadillo"));
            yield return new DwellingMapObject("Manufactory (Automaton)", this.monsterFactory.GetMonster("Automaton"));
            yield return new DwellingMapObject("Catacombs (Sandworm)", this.monsterFactory.GetMonster("Sandworm"));
            yield return new DwellingMapObject("Watchtower (Gunslinger)", this.monsterFactory.GetMonster("Gunslinger"));
            yield return new DwellingMapObject("Serpentarium (Couatl)", this.monsterFactory.GetMonster("Couatl"));
            yield return new DwellingMapObject("Gantry (Dreadnought)", this.monsterFactory.GetMonster("Dreadnought"));
        }
    }
}
