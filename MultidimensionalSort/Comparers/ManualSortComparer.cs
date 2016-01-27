using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultidimensionalSort.Comparers
{
    /// <summary>
    /// Comparer for manual sorting
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ManualSortComparer<T> : IComparer<T>
    {
        private readonly IList<T> _manualOrderList;

        public ManualSortComparer(IList<T> manualOrderList)
        {
            _manualOrderList = manualOrderList;
        }

        public int Compare(T x, T y)
        {
            if (_manualOrderList == null)
                return 0;

            var xId = _manualOrderList.IndexOf(x);
            var yId = _manualOrderList.IndexOf(y);

            return xId.CompareTo(yId);
        }
    }
}
