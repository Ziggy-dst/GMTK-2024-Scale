using UnityEngine;

namespace _Scripts.Utilities
{
    public class Utils
    {
        public static T GetRandomEnumValue<T>()
        {
            T[] values = (T[])System.Enum.GetValues(typeof(T));
            return values[Random.Range(0, values.Length)];
        }
    }
}