namespace Homm3.Domain.Model.MapObjects
{
    public class GuessedObjectResult
    {
        public MapObject MapObject { get; set; }
        public int DifferenceFromAverage { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
    }
}
