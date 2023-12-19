using System;
using System.Collections.Generic;
using System.Linq;

namespace CMCore.Utilities.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Randomly picks one elements from the enumerable
        /// </summary>
        /// <typeparam name="T">The type of the item</typeparam>
        /// <param name="items">The items to random from</param>
        /// <returns></returns>
        public static T Random<T>(this IEnumerable<T> items)
        {
            if (items == null) throw new ArgumentException("Cannot randomly pick an item from the list, the list is null!");
            var enumerable = items as T[] ?? items.ToArray();
            if (!enumerable.Any()) throw new ArgumentException("Cannot randomly pick an item from the list, there are no items in the list!");
            var r = UnityEngine.Random.Range(0, enumerable.Count());
            return enumerable.ElementAt(r);
        }

        /// <summary>
        /// Randomly picks one element from the enumerable, taking into account a weight
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="weightSelector"></param>
        /// <returns></returns>
        public static T WeightedRandomOne<T>(this IEnumerable<T> sequence, Func<T, float> weightSelector)
        {
            var enumerable = sequence as T[] ?? sequence.ToArray();
            var totalWeight = enumerable.Sum(weightSelector);
            // The weight we are after...
            var itemWeightIndex = UnityEngine.Random.value * totalWeight;
            float currentWeightIndex = 0;

            foreach (var item in from weightedItem in enumerable select new { Value = weightedItem, Weight = weightSelector(weightedItem) })
            {
                currentWeightIndex += item.Weight;

                // If we've hit or passed the weight we are after for this item then it's the one we want....
                if (currentWeightIndex >= itemWeightIndex)
                    return item.Value;

            }

            return default;
        }
        
        public static void ChangeIndex<T>(this List<T> list, int oldIndex, int newIndex)
        {
            var item = list[oldIndex];
            list.RemoveAt(oldIndex);
            list.Insert(newIndex, item);
        }
        public static List<T> Randoms<T>(this IEnumerable<T> list, int elementsCount)
        {
            return list.OrderBy(arg => Guid.NewGuid()).Take(elementsCount).ToList();
        }

        public static T[] Randoms<T>(this T[] list, int elementsCount)
        {
            return list.OrderBy(arg => Guid.NewGuid()).Take(elementsCount).ToArray();
        }
    }
}
