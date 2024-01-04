namespace Homm3.Domain.Model.MapPresets
{
    public class PresetFactory : IPresetFactory
    {
        private readonly List<Preset> presets;
        public PresetFactory()
        {
            this.presets = new List<Preset>
            {
                new Preset
                {
                    Name = "Jebus Cross Start",
                    MonsterStrength = "Strong",
                    ZoneStrength = "Weak",
                    TotalTownZoneCount = 5,
                    CurrentTownZoneCount = 1,
                },
                new Preset
                {
                    Name = "Jebus Cross Treasury",
                    MonsterStrength = "Strong",
                    ZoneStrength = "Strong",
                    TotalTownZoneCount = 5,
                    CurrentTownZoneCount = 1,
                },
                new Preset
                {
                    Name = "Jebus Cross Zone Guard",
                    MonsterStrength = "Strong",
                    ZoneStrength = "ZoneGuard",
                    TotalTownZoneCount = 5,
                    CurrentTownZoneCount = 1,
                    ZoneGuardValue = 45000,
                },
                new Preset
                {
                    Name = "Mlyn Start/Dirt",
                    MonsterStrength = "Strong",
                    ZoneStrength = "Weak",
                    TotalTownZoneCount = 10,
                    CurrentTownZoneCount = 2,
                },
                new Preset
                {
                    Name = "Mlyn Zone Guard",
                    MonsterStrength = "Strong",
                    ZoneStrength = "ZoneGuard",
                    ZoneGuardValue = 35000,
                    TotalTownZoneCount = 10,
                    CurrentTownZoneCount = 2,
                },
                new Preset
                {
                    Name = "Mlyn Treasury",
                    MonsterStrength = "Strong",
                    ZoneStrength = "Strong",
                    TotalTownZoneCount = 10,
                    CurrentTownZoneCount = 2
                },
                new Preset
                {
                    Name = "Mini Nostalgia",
                    MonsterStrength = "Strong",
                    ZoneStrength = "Average",
                    TotalTownZoneCount = 14,
                    CurrentTownZoneCount = 2
                }
            };
        }
        public List<Preset> GetPresets()
        {
            return this.presets;
        }
    }
}
