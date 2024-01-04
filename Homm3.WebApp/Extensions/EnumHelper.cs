namespace Homm3.WebApi.Extensions
{
    public static class EnumHelper
    {
        public static List<KeyValuePair<string, int>> ConvertEnumToKeyValuePair<T>()
        {
            return Enum.GetNames(typeof(T)).Select((value => new KeyValuePair<string, int>(value, (int)Enum.Parse(typeof(T), value)))).ToList();
        }
    }
}
