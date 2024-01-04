using Homm3.WebApi.Contracts;

namespace Homm3.WebApp.Contracts
{
    public class CalculatedMonsterValuesContract
    {
        public MonsterContract Monster { get; internal set; }
        public int AverageMonsterCount { get; set; }
        public int MonsterCountDeviation { get; set; }
        public int MinimalCount { get; set; }
        public int MaximalCount { get; set; }
    }
}
