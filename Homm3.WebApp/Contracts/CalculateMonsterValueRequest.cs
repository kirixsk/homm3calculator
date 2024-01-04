namespace Homm3.WebApp.Contracts
{
    public class CalculateMonsterValueRequest
    {
        public string TownName { get; set; }
        public string MapMonsterStrengthName { get; set; }
        public string ZoneStrengthName { get; set; }
        public int TotalTownZoneCount { get; set; }
        public int CurrentTownZoneCount { get; set; }
        public string MonsterName { get; set; }
        public IList<string> ObjectNames { get; set; }
        public int Week { get; set; }
    }
}
