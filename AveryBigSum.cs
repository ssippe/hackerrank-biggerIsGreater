using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Shouldly;
using Xunit;

namespace compare_the_triplets
{
    public class AveryBigSum
    {
        // Complete the designerPdfViewer function below.
        // Complete the aVeryBigSum function below.
        static long aVeryBigSum(long[] ar) => ar.Sum();
        

        [Fact]
        public void Test1()
        {
            aVeryBigSum("1000000001 1000000002 1000000003 1000000004 1000000005".Split(' ').Select(f=>long.Parse(f)).ToArray()).ShouldBe(5000000015);
        }
    }
}
