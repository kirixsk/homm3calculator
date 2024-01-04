namespace Homm3.Domain.Model.Monsters
{
    public class CalculatedMonsterValues
    {
        public Monster Monster { get; internal set; }
        public int AverageMonsterCount { get; set; }
        public int MonsterCountDeviation { get; set; }
        public int MinimalCount { get; set; }
        public int MaximalCount { get; set; }
    }
}
