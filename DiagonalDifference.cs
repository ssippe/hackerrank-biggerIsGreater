using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Shouldly;
using Xunit;

namespace compare_the_triplets
{
    public class DiagonalDifference
    {
        static int diagonalDifference(int[][] arr)
        {
            int d1 = 0, d2 = 0;
            for (int i=0; i < arr.Length; i++)
            {
                d1 += arr[i][i];
                int j = arr.Length - i - 1;
                d2 += arr[i][j];
            }

            return Math.Abs(d1 - d2);
        }


        [Fact]
        public void Test1()
        {
            diagonalDifference(ToArr(@"11 2 4
4 5 6
10 8 -12")).ShouldBe(15);
        }

        int[][] ToArr(string s)
        {
            var arr = s.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(li => int.Parse(li))
                    .ToArray()).ToArray();
            return arr;

        }
    }
}
