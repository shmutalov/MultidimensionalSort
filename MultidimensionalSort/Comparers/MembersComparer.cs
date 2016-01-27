using System.Collections.Generic;
using MultidimensionalSort.Models;

namespace MultidimensionalSort.Comparers
{
    public class MembersComparer : IComparer<Member[]>
    {
        private readonly int _memberId;
        private readonly IComparer<string> _comparer;

        public MembersComparer(int memberId)
        {
            _memberId = memberId;
            _comparer = new NaturalComparer();
        }

        public MembersComparer(int memberId, IComparer<string> comparer)
        {
            _memberId = memberId;
            _comparer = comparer;
        }

        public int Compare(Member[] x, Member[] y)
        {
            // check bounds
            if (_memberId >= x.Length || _memberId >= y.Length)
                return 0;

            if (_memberId > 0)
            {
                if (!x[_memberId - 1].Name.Equals(y[_memberId - 1].Name))
                    return 0;
            }

            // compare member name with natural comparer
            return _comparer.Compare(x[_memberId].Name, y[_memberId].Name);
        }
    }
}
