using System;
using System.Collections.Generic;
using System.Linq;
using MultidimensionalSort.Comparers;
using MultidimensionalSort.Enums;
using MultidimensionalSort.Extensions;
using MultidimensionalSort.Models;

namespace MultidimensionalSort
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var memberTypes = new[]
            {
                new MemberType
                {
                    Name = "Region",
                    SortingType = new SortingType
                    {
                        Method = SortMethod.Alphabetic,
                        Order = SortOrderType.Ascending
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

            var listOfMembers = new[]
            {
                new [] { new Member("North"), new Member("A") },
                new [] { new Member("North"), new Member("C") },
                new [] { new Member("South"), new Member("A") },
                new [] { new Member("South"), new Member("B") },
                new [] { new Member("South"), new Member("C") },
            };

            // print members before sort
            PrintMembers(listOfMembers);

            listOfMembers = ApplySorting(listOfMembers, memberTypes);

            // print members after sort
            PrintMembers(listOfMembers);

            Console.ReadLine();
        }

        private static Member[][] ApplySorting(Member[][] listOfMembers, MemberType[] memberTypes)
        {
            var ordered = listOfMembers.ApplySorter(members => members, null, true, SortOrderType.Noop);

            for (var memberTypeId = 0; memberTypeId < memberTypes.Length; memberTypeId++)
            {
                var memberType = memberTypes[memberTypeId];
                var memberTypeIdCopy = memberTypeId;

                if (memberType.SortingType == null)
                {
                    continue;
                }

                switch (memberType.SortingType.Method)
                {
                    case SortMethod.Alphabetic:
                        switch (memberType.SortingType.Order)
                        {
                            case SortOrderType.Ascending:
                                ordered = ordered.ApplyCustomSorter(members => members, ordered, false,
                                    new MembersComparer(memberTypeIdCopy));
                                break;
                            case SortOrderType.Descending:
                                ordered = ordered.ApplyCustomSorter(members => members, ordered, false,
                                    new MembersComparer(memberTypeIdCopy), SortOrderType.Descending);
                                break;
                        }
                        break;
                    case SortMethod.Manual:
                        ordered = ordered.ApplyCustomSorter(members => members, ordered, false,
                                    new MembersComparer(memberTypeIdCopy, new ManualSortComparer<string>(memberType.ManualOrderedValues)));
                        break;
                    default:
                        continue;
                }
            }

            return ordered.ToArray();
        }

        /// <summary>
        /// Prints list of members list
        /// </summary>
        /// <param name="listOfMembers"></param>
        private static void PrintMembers(Member[][] listOfMembers)
        {
            Console.WriteLine();

            foreach (var members in listOfMembers)
            {
                var line = string.Join<Member>(" # ", members);

                Console.WriteLine(line);
            }
        }
    }
}
