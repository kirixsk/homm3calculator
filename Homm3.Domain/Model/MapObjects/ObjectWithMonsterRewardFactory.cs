using Homm3.Domain.Model.Enums;
using Homm3.Domain.Model.Monsters;

namespace Homm3.Domain.Model.MapObjects
{
    public class ObjectWithMonsterRewardFactory : IObjectWithMonsterRewardFactory
    {
        private readonly List<(int, int)> BaseRewardLevelsAndAiValue;

        public ILookup<Town, Monster> Monsters { get; }

        public ObjectWithMonsterRewardFactory(IMonsterFactory monsterFactory)
        {
            this.BaseRewardLevelsAndAiValue = new List<(int, int)>
            {
                (1, 5000),
                (2, 7000),
                (3, 9000),
                (4, 12000),
                (5, 16000),
                (6, 21000),
                (7, 27000)
            };
            this.Monsters = monsterFactory.ListMonsters().ToLookup(x => x.Town); ;
        }

        public IEnumerable<MapObject> GenerateObjectsWithMonstersRewards()
        {
            var result = new List<MapObject>();
            foreach (var boxLevel in BaseRewardLevelsAndAiValue)
            {
                foreach (Town town in Enum.GetValues(typeof(Town)))
                {
                    var boxes = GenerateObjectsWithMonstersRewards(boxLevel, town);
                    result.AddRange(boxes);
                }
            }
            return result;
        }

        private List<MapObject> GenerateObjectsWithMonstersRewards((int, int) boxLevel, Town town)
        {
            var result = new List<MapObject>();
            var monsters = this.Monsters[town].Where(x => x.Level == boxLevel.Item1);
            foreach (var monster in monsters)
            {
                var monsterValue = monster.AiValue;
                var monsterQty = GetMonsterQuantityInBox(boxLevel.Item2, monsterValue);
                if (monsterQty < 1)
                {
                    continue;
                }
                var pandoraValue = monsterQty * monsterValue;
                var pandoraBoxName = $"Pandora's Box level {monster.Level} with {monster.Name}({monsterQty})";
                var questName = $"Quest Reward level {monster.Level} with {monster.Name}({monsterQty})";
                var pandoraBox = new PandoraWithMonstersMapObject(pandoraBoxName, pandoraValue, pandoraBoxName, town);
                var quest = new QuestArtifactWithMonstersMapObject(questName, pandoraValue, questName, town);
                result.Add(pandoraBox);
                result.Add(quest);
            }
            return result;
        }

        private int GetMonsterQuantityInBox(int pandoraValue, int monsterValue)
        {
            var rawQty = pandoraValue / monsterValue;
            if (rawQty <= 5)
            {
                return rawQty;
            }
            if (rawQty <= 12)
            {
                return rawQty + rawQty % 2;
            }
            if (rawQty <= 50)
            {
                return 5 * (int)Math.Round(rawQty / 5.0);
            }
            return Math.Min(150, 10 * (int)Math.Round(rawQty / 10.0));
        }
    }
}
