using System.Collections.Generic;
using MultidimensionalSort.Enums;
using MultidimensionalSort.Models;
using NUnit.Framework;

namespace MultidimensionalSort
{
    [TestFixture]
    public class Tests
    {
        private readonly Member[][] _testListOfMembers = {
                new [] { new Member("North"), new Member("A") },
                new [] { new Member("North"), new Member("C") },
                new [] { new Member("South"), new Member("A") },
                new [] { new Member("South"), new Member("B") },
                new [] { new Member("South"), new Member("C") },
            };

        [Test]
        public void TestAlphabeticalSortFirstColumnThenManualSortSecondColumn()
        {
            var memberTypes = new[] {
                new MemberType
                {
                    Name = "Region",
                    SortingType = new SortingType
                    {
                        Method = SortMethod.Alphabetic,
                        Order = SortOrderType.Descending
                    }
                },
                new MemberType{
                    Name = "Class",
                    SortingType = new SortingType
                    {
                        Method = SortMethod.Manual, Order = SortOrderType.Ascending
                    },
                    ManualOrderedValues = new List<string>(new []{"C", "A", "B"})
                },
            };

            var expectedListOfMembers = new[] {
                new[] { new Member("South"), new Member("C") },
                new[] { new Member("South"), new Member("A") },
                new[] { new Member("South"), new Member("B") },
                new[] { new Member("North"), new Member("C") },
                new[] { new Member("North"), new Member("A") },
            };

            var sortedListOfMembers = Program.ApplySorting(_testListOfMembers, memberTypes);

            // check result
            Assert.IsTrue(IsListsEqual(expectedListOfMembers, sortedListOfMembers));

            Program.PrintMembers(sortedListOfMembers);
        }

        [Test]
        public void TestManualSortSecondColumn()
        {
            var memberTypes = new[] {
                new MemberType
                {
                    Name = "Region",
                },
                new MemberType{
                    Name = "Class",
                    SortingType = new SortingType
                    {
                        Method = SortMethod.Manual, Order = SortOrderType.Ascending
                    },
                    ManualOrderedValues = new List<string>(new []{"C", "A", "B"})
                },
            };

            var expectedListOfMembers = new[] {
                new[] { new Member("North"), new Member("C") },
                new[] { new Member("North"), new Member("A") },
                new[] { new Member("South"), new Member("C") },
                new[] { new Member("South"), new Member("A") },
                new[] { new Member("South"), new Member("B") },
            };

            var sortedListOfMembers = Program.ApplySorting(_testListOfMembers, memberTypes);

            // check result
            Assert.IsTrue(IsListsEqual(expectedListOfMembers, sortedListOfMembers));

            Program.PrintMembers(sortedListOfMembers);
        }

        private static bool IsListsEqual(IReadOnlyList<Member[]> firstList, IReadOnlyList<Member[]> secondList)
        {
            if (firstList.Count != secondList.Count)
                return false;

            for (var membersId = 0; membersId < firstList.Count; membersId++)
            {
                var firstListMembers = firstList[membersId];
                var secondListMembers = secondList[membersId];

                if (firstListMembers.Length != secondListMembers.Length)
                    return false;

                for (var memberId = 0; memberId < firstListMembers.Length; memberId++)
                {
                    var firstMember = firstListMembers[memberId];
                    var secondMember = secondListMembers[memberId];

                    if (!firstMember.Name.Equals(secondMember.Name))
                        return false;
                }
            }

            return true;
        }
    }
}
