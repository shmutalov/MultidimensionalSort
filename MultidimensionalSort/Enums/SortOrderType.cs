using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultidimensionalSort.Enums
{
    /// <summary>
    /// Order types used in sorting
    /// </summary>
    public enum SortOrderType
    {
        /// <summary>
        /// Order ascending
        /// </summary>
        Ascending,

        /// <summary>
        /// Order descending
        /// </summary>
        Descending,

        /// <summary>
        /// Order manual
        /// </summary>
        Manual,

        /// <summary>
        /// Order with NOOP (does not actually order values)
        /// </summary>
        Noop,
    }
}
