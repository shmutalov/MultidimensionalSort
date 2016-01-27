using System.Collections.Generic;
using System.Linq;
using MultidimensionalSort.Comparers;
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

            Program.PrintMembers(sortedListOfMembers);

            // check result
            Assert.IsTrue(IsListsEqual(expectedListOfMembers, sortedListOfMembers));
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

            Program.PrintMembers(sortedListOfMembers);

            // check result
            Assert.IsTrue(IsListsEqual(expectedListOfMembers, sortedListOfMembers));
        }

        [Test]
        public void TestAlphabeticalSortSecondColumn()
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
                        Method = SortMethod.Alphabetic,
                        Order = SortOrderType.Descending
                    },
                },
            };

            var expectedListOfMembers = new[] {
                new[] { new Member("North"), new Member("C") },
                new[] { new Member("North"), new Member("A") },
                new[] { new Member("South"), new Member("C") },
                new[] { new Member("South"), new Member("B") },
                new[] { new Member("South"), new Member("A") },
            };

            var sortedListOfMembers = Program.ApplySorting(_testListOfMembers, memberTypes);

            Program.PrintMembers(sortedListOfMembers);

            // check result
            Assert.IsTrue(IsListsEqual(expectedListOfMembers, sortedListOfMembers));
        }

        [Test]
        public void TestManualOrder()
        {
            var expectedListOfMembers = new[] {
                new[] { new Member("North"), new Member("C") },
                new[] { new Member("North"), new Member("A") },
                new[] { new Member("South"), new Member("C") },
                new[] { new Member("South"), new Member("B") },
                new[] { new Member("South"), new Member("A") },
            };

            var sortedListOfMembers = _testListOfMembers
                .OrderBy(members => members, new MembersComparer(0, new NoopComparer<string>()))
                .ThenByDescending(members => members, new MembersComparer(1))
                .ToList();

            Program.PrintMembers(sortedListOfMembers);

            // check result
            Assert.IsTrue(IsListsEqual(expectedListOfMembers, sortedListOfMembers));
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
