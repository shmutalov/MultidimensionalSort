using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultidimensionalSort.Comparers
{
    public class NaturalComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (x == y)
                return 0;

            if (x == null || y == null)
                return 0;

            var xLen = x.Length;
            var yLen = y.Length;

            var xMarker = 0;
            var yMarker = 0;

            // Walk through two the strings with two markers.
            while (xMarker < xLen && yMarker < yLen)
            {
                var xChar = x[xMarker];
                var yChar = y[yMarker];

                // Some buffers we can build up characters in for each chunk.
                var xSpace = new char[xLen];
                var xLoc = 0;
                var ySpace = new char[yLen];
                var yLoc = 0;

                // Walk through all following characters that are digits or
                // characters in BOTH strings starting at the appropriate marker.
                // Collect char arrays.
                do
                {
                    xSpace[xLoc++] = xChar;
                    xMarker++;

                    if (xMarker < xLen)
                    {
                        xChar = x[xMarker];
                    }
                    else
                    {
                        break;
                    }
                } while (char.IsDigit(xChar) == char.IsDigit(xSpace[0]));

                do
                {
                    ySpace[yLoc++] = yChar;
                    yMarker++;

                    if (yMarker < yLen)
                    {
                        yChar = y[yMarker];
                    }
                    else
                    {
                        break;
                    }
                } while (char.IsDigit(yChar) == char.IsDigit(ySpace[0]));

                // If we have collected numbers, compare them numerically.
                // Otherwise, if we have strings, compare them alphabetically.
                var xStr = new string(xSpace);
                var yStr = new string(ySpace);

                int result;

                if (char.IsDigit(xSpace[0]) && char.IsDigit(ySpace[0]))
                {
                    var thisNumericChunk = int.Parse(xStr);
                    var thatNumericChunk = int.Parse(yStr);
                    result = thisNumericChunk.CompareTo(thatNumericChunk);
                }
                else
                {
                    result = string.Compare(xStr, yStr, StringComparison.InvariantCultureIgnoreCase);
                }

                if (result != 0)
                {
                    return result;
                }
            }

            return xLen - yLen;
        }
    }
}
