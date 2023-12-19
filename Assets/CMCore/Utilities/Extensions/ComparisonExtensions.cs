using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CMCore.Utilities.Extensions
{
    public static class ComparisonExtensions
    {
        
        public static int GetWeightedIndex(List<int> weights)
        {
            if (weights == null || weights.Count == 0)
            {
                throw new ArgumentException("Weights list cannot be null or empty.");
            }
        
            int totalWeight = 0;
            foreach (int weight in weights)
            {
                if (weight <= 0)
                {
                    throw new ArgumentException("Weight values must be greater than zero.");
                }
            
                totalWeight += weight;
            }
        
            int randomValue = UnityEngine.Random.Range(1, totalWeight + 1);
        
            int currentIndex = 0;
            int cumulativeWeight = 0;
            foreach (int weight in weights)
            {
                cumulativeWeight += weight;
            
                if (randomValue <= cumulativeWeight)
                {
                    return currentIndex;
                }
            
                currentIndex++;
            }
        
            // Shouldn't reach this point, but return last index as fallback
            return currentIndex - 1;
        }
        public static bool IsAllTrue(this bool[] booleans) => booleans.All(t => t);


        public static bool SearchInside(this string[] source, string[] searchInside) =>
            searchInside.All(source.Contains);
        public static bool SearchInside(this float[] source, float[] searchInside) => searchInside.All(source.Contains);
        public static bool SearchInside(this int[] source, int[] searchInside) => searchInside.All(source.Contains);
        public static bool SearchInside(this Transform[] source, Transform[] searchInside) =>
            searchInside.All(source.Contains);
        public static bool SearchInside(this Vector3[] source, Vector3[] searchInside) =>
            searchInside.All(source.Contains);


        public static bool HasSameElements(this bool[] a, bool[] b) =>
            (a != null && b != null) && (a.Length > 0 && b.Length > 0) && a.Length == b.Length &&
            a.OrderBy(c => c).SequenceEqual(b.OrderBy(c => c));

        public static bool HasSameElements(this string[] a, string[] b) =>
            (a != null && b != null) && (a.Length > 0 && b.Length > 0) &&
            a.Length == b.Length &&
            a.OrderBy(c => c).SequenceEqual(b.OrderBy(c => c));

        public static bool HasSameElements(this float[] a, float[] b) =>
            (a != null && b != null) && (a.Length > 0 && b.Length > 0) &&
            a.Length == b.Length && a.OrderBy(c => c)
                .SequenceEqual(b.OrderBy(c => c));

        public static bool HasSameElements(this int[] a, int[] b) =>
            (a != null && b != null) && (a.Length > 0 && b.Length > 0) && a.Length == b.Length &&
            a.OrderBy(c => c).SequenceEqual(b.OrderBy(c => c));

        public static bool HasSameElements(this double[] a, double[] b) =>
            (a != null && b != null) && (a.Length > 0 && b.Length > 0) &&
            a.Length == b.Length &&
            a.OrderBy(c => c).SequenceEqual(b.OrderBy(c => c));

        public static bool HasSameElements(this char[] a, char[] b) =>
            (a != null && b != null) && (a.Length > 0 && b.Length > 0) && a.Length == b.Length &&
            a.OrderBy(c => c).SequenceEqual(b.OrderBy(c => c));

        public static bool HasSameElements(this Vector3[] a, Vector3[] b) =>
            (a != null && b != null) && (a.Length > 0 && b.Length > 0) &&
            a.Length == b.Length &&
            a.OrderBy(c => c).SequenceEqual(b.OrderBy(c => c));

        public static bool HasSameElements(this Transform[] a, Transform[] b) =>
            (a != null && b != null) && (a.Length > 0 && b.Length > 0) &&
            a.Length == b.Length &&
            a.OrderBy(c => c)
                .SequenceEqual(b.OrderBy(c => c));
    }
}