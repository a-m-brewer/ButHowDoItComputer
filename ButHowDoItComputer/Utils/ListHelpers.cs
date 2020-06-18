using System.Collections.Generic;

namespace ButHowDoItComputer.Utils
{
    public static class ListHelpers
    {
        public static IList<T> ReverseList<T>(this IList<T> items)
        {
            var revListCount = 0;
            var revList = new T[items.Count];
            for (var i = items.Count - 1; i >= 0; i--)
            {
                revList[revListCount] = items[i];
                revListCount++;
            }

            return revList;
        }

        public static IList<T> TakeList<T>(this IList<T> items, int amount)
        {
            var tempList = new T[amount];

            for (int i = 0; i < amount; i++)
            {
                tempList[i] = items[i];
            }

            return tempList;
        }

        public static IList<T> SkipList<T>(this IList<T> items, int amount)
        {
            var tempList = new T[items.Count - amount];

            for (var i = amount; i < items.Count; i++)
            {
                tempList[i - amount] = items[i];
            }

            return tempList;
        }
    }
}