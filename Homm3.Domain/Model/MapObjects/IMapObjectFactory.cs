namespace Homm3.Domain.Model.MapObjects
{
    public interface IMapObjectFactory
    {
        MapObject GetMapObject(string name);
        List<MapObject> ListMapObjects();
    }
}