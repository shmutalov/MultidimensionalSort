using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultidimensionalSort.Comparers
{
    /// <summary>
    /// Noop comparer. Compare result will be always equal to zero
    /// </summary>
    public class NoopComparer<T> : IComparer<T>
    {
        public int Compare(T x, T y)
        {
            return 0;
        }
    }
}
