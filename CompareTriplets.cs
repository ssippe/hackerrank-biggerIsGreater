using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Shouldly;
using Xunit;

namespace compare_the_triplets
{
    public class CompareTriplets
    {
        static List<int> compareTriplets(List<int> a, List<int> b)
        {
            var items =
                a.Select((f, idx) =>
                    {
                        if (a[idx] > b[idx]) return new[] {1, 0};
                        if (a[idx] < b[idx]) return new[] {0, 1};
                        return new[] {0, 0};
                    }).Aggregate(new int[] {0, 0}, (f1, f2) => new int[] {f1[0] + f2[0], f1[1] + f2[1]})
                    .ToList();            

            return items;
        }

        [Fact]
        public void Test1()
        {
            compareTriplets(new List<int>{17 ,28, 30}, new List<int> { 99 ,16, 8} ).ShouldBe(new List<int>{2,1});
            compareTriplets(new List<int> { 5,6,7}, new List<int> { 3,6,10}).ShouldBe(new List<int> { 1, 1 });
        }
    }
}
