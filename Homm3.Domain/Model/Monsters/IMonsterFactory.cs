namespace Homm3.Domain.Model.Monsters
{
    public interface IMonsterFactory
    {
        Monster GetMonster(string name);
        List<Monster> ListMonsters();
    }
}