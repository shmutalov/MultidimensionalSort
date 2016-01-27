using System.Collections.Generic;

namespace MultidimensionalSort.Models
{
    public class MemberType
    {
        public string Name { get; set; }
        public SortingType SortingType { get; set; }
        public List<string> ManualOrderedValues { get; set; }

        public override string ToString()
        {
            return string.Format("{0}:{1}", Name, SortingType?.ToString() ?? "None");
        }
    }
}
