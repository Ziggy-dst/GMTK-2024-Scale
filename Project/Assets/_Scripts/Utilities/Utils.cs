using System;
using System.Linq;
// using Random = UnityEngine.Random;

namespace _Scripts.Utilities
{
    public class Utils
    {
        private static readonly Random random = new Random();

        // public static T GetRandomEnumValue<T>()
        // {
        //     T[] values = (T[])System.Enum.GetValues(typeof(T));
        //     return values[Random.Range(0, values.Length)];
        // }

        public static T GetRandomEnumValue<T>(params T[] exclude) where T : Enum
        {
            // 获取所有枚举值，并排除指定的值
            var values = Enum.GetValues(typeof(T)).Cast<T>().Except(exclude).ToArray();

            // 如果没有剩余的值，抛出异常
            if (values.Length == 0)
            {
                throw new ArgumentException("No valid enum values available after exclusion.");
            }

            // 随机选择一个值
            return values[random.Next(values.Length)];
        }
    }
}