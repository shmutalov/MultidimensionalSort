using System;
using System.Collections.Generic;
using System.Linq;
using MultidimensionalSort.Comparers;
using MultidimensionalSort.Enums;

namespace MultidimensionalSort.Extensions
{
    /// <summary>
    /// Sorting extension method for IEnumerables
    /// </summary>
    public static class SorterExt
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="values"></param>
        /// <param name="keySelector"></param>
        /// <param name="presorted"></param>
        /// <param name="isFirst"></param>
        /// <param name="sortOrder"></param>
        /// <param name="manualOrder"></param>
        /// <returns></returns>
        public static IOrderedEnumerable<T> ApplySorter<T, TKey>(
            this IEnumerable<T> values,
            Func<T, TKey> keySelector,
            IOrderedEnumerable<T> presorted,
            bool isFirst,
            SortOrderType sortOrder = SortOrderType.Ascending,
            IList<TKey> manualOrder = null)
        {
            switch (sortOrder)
            {
                case SortOrderType.Ascending:
                    return isFirst
                        ? values.OrderBy(keySelector)
                        : presorted.ThenBy(keySelector);
                case SortOrderType.Descending:
                    return isFirst
                        ? values.OrderByDescending(keySelector)
                        : presorted.ThenByDescending(keySelector);
                case SortOrderType.Manual:
                    return isFirst
                        ? values.OrderBy(keySelector, new ManualSortComparer<TKey>(manualOrder))
                        : presorted.ThenBy(keySelector, new ManualSortComparer<TKey>(manualOrder));
                default:
                    return values.OrderBy(_ => 1);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="values"></param>
        /// <param name="keySelector"></param>
        /// <param name="presorted"></param>
        /// <param name="isFirst"></param>
        /// <param name="comparer"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public static IOrderedEnumerable<T> ApplyCustomSorter<T, TKey>(
            this IEnumerable<T> values,
            Func<T, TKey> keySelector,
            IOrderedEnumerable<T> presorted,
            bool isFirst,
            IComparer<TKey> comparer,
            SortOrderType sortOrder = SortOrderType.Ascending)
        {
            switch (sortOrder)
            {
                case SortOrderType.Ascending:
                    return isFirst
                        ? values.OrderBy(keySelector, comparer)
                        : presorted.ThenBy(keySelector, comparer);
                case SortOrderType.Descending:
                    return isFirst
                        ? values.OrderByDescending(keySelector, comparer)
                        : presorted.ThenByDescending(keySelector, comparer);
                default:
                    return values.OrderBy(_ => 1);
            }
        }
    }
}
