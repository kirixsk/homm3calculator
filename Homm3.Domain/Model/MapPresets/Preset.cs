namespace Homm3.Domain.Model.MapPresets
{
    public class Preset
    {
        public string Name { get; set; }
        public string ZoneStrength { get; set; }
        public string MonsterStrength { get; set; }
        public int TotalTownZoneCount { get; set; }
        public int CurrentTownZoneCount { get; set; }
        public int ZoneGuardValue { get; set; }
    }
}
