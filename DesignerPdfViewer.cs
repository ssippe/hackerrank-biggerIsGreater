using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Shouldly;
using Xunit;

namespace compare_the_triplets
{
    public class DesignerPdfViewer
    {
        // Complete the designerPdfViewer function below.
        static int designerPdfViewer(int[] h, string word) => word.ToCharArray().Select(c => h[(int) c - 97]).Max() * word.Length;
        
        [Fact]
        public void Test1()
        {
            designerPdfViewer(new[] { 1, 3, 1, 3, 1, 4, 1, 3, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5}, "abc").ShouldBe(9);
            designerPdfViewer("1 3 1 3 1 4 1 3 2 5 5 5 5 5 5 5 5 5 5 5 5 5 5 5 5 7".Split(' ').Select(f=>int.Parse(f)).ToArray(), "zaba").ShouldBe(28);
        }
    }
}
