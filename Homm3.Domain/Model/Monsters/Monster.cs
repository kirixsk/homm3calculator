using Homm3.Domain.Model.Enums;
namespace Homm3.Domain.Model.Monsters
{
    public class Monster
    {
        public Town Town { get; set; }
        public string Name { get; set; }
        public int AiValue { get; set; }
        public int Growth { get; set; }
        public int Level { get; set; }
        public int UpgradeLevel { get; set; }
        public string TierName
        {
            get
            {
                var tierName = $"{Town.ToString().Substring(0, 3)}.{Level}";
                for (int i = 0; i < UpgradeLevel; i++)
                {
                    tierName += "+";
                }
                return tierName;
            }
        }

        public string DisplayName
        {
            get
            {
                if (UpgradeLevel == 0)
                {
                    return Name;
                }
                else if (UpgradeLevel == 1)
                {
                    return $"{Name} (upg.)";
                }
                else
                {
                    return $"{Name} ({UpgradeLevel}x upg.)";
                }
            }
        }

        public Monster(Town town, string name, int aiValue, int growth, int level, int upgradeLevel = 0)
        {
            Town = town;
            Name = name;
            AiValue = aiValue;
            Growth = growth;
            UpgradeLevel = upgradeLevel;
            Level = level;
        }
    }
}
