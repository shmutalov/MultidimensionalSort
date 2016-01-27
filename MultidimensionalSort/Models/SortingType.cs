using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultidimensionalSort.Enums;

namespace MultidimensionalSort.Models
{
    public class SortingType
    {
        public SortMethod Method { get; set; }
        public SortOrderType Order { get; set; }
    }
}
