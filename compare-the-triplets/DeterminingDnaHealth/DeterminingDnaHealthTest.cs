using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Shouldly;
using Xunit;

namespace compare_the_triplets.DeterminingDnaHealth
{

    public class DeterminingDnaHealthTest
    {
        public DeterminingDnaHealth.Input FromString(string s)
        {
            int i = 0;
            var splits = s.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
            return From(() => splits[i++]);
        }

        static DeterminingDnaHealth.Input From(Func<string> newLineFunc)
        {
            var stopWatch = Stopwatch.StartNew();
            int si = 0;
            Debug.WriteLine($"{si++} {stopWatch.Elapsed}");
            var headerLines = new List<string>
            {newLineFunc(), newLineFunc(), newLineFunc(), newLineFunc()};
            var input = ReadHeader(headerLines);
            Debug.WriteLine($"{si++} {stopWatch.Elapsed}");
            var lastTime = stopWatch.Elapsed;
            for (int i = 0; i < input.DnaCount; i++)
            {
                if (i % 100 == 0)
                {
                    Debug.WriteLine($"{si++} {stopWatch.Elapsed} {i}/{input.DnaCount}, {stopWatch.Elapsed - lastTime} per 100");
                    lastTime = stopWatch.Elapsed;
                }
                var health = (long)input.ProcessDnaLine(newLineFunc());
                input.MaxHealth = Math.Max(health, input.MaxHealth);
                input.MinHealth = Math.Min(health, input.MinHealth);
            }

            input.MinHealth = input.MinHealth == int.MaxValue ? input.MaxHealth : input.MinHealth;
            input.MaxHealth = input.MaxHealth == int.MinValue ? input.MinHealth : input.MaxHealth;
            return input;
        }


        static DeterminingDnaHealth.Input ReadHeader(List<string> lines)
        {

            var geneCount = int.Parse(lines[0]);
            var genes = lines[1].Split(' ');
            var heathVals = lines[2].Split(' ').Select(int.Parse).ToArray();
            int dnaCount = int.Parse(lines[3]);
            var treeRoot =  DeterminingDnaHealth.BuildTree(genes, heathVals);
            return new DeterminingDnaHealth.Input { DnaCount = dnaCount, Genes = genes, HeathVals = heathVals, Root = treeRoot };
        }


        [Fact]
        public void DeterminingDnaHealthTest0()
        {
            FromString(@"6
a b c aa d b
1 2 3 4 5 6
3
1 5 caaab
0 4 xyz
2 4 bcdybc").ResultToString().ShouldBe("0 19");
        }

        [Fact]
        public void DeterminingDnaHealthTest2()
        {
            var sr = new StreamReader(this.GetType().Assembly
                .GetManifestResourceStream(this.GetType(), "DeterminingDnaHealth.In2.txt"));
            From(() => sr.ReadLine()).ResultToString().ShouldBe("15806635 20688978289");
        }

        [Fact]
        public void DeterminingDnaHealthTest7()
        {
            var sr = new StreamReader(this.GetType().Assembly
                .GetManifestResourceStream(this.GetType(), "DeterminingDnaHealth.In7.txt"));
            From(() => sr.ReadLine()).ResultToString().ShouldBe("0 7353994");
        }



        [Fact]
        public void DeterminingDnaHealthTest8()
        {
            var sr = new StreamReader(this.GetType().Assembly
                .GetManifestResourceStream(this.GetType(), "DeterminingDnaHealth.In8.txt"));
            From(() => sr.ReadLine()).ResultToString().ShouldBe("0 8652768");
        }

        [Fact]
        public void DeterminingDnaHealthTest9()
        {
            var sr = new StreamReader(this.GetType().Assembly
                .GetManifestResourceStream(this.GetType(), "DeterminingDnaHealth.In9.txt"));
            From(() => sr.ReadLine()).ResultToString().ShouldBe("0 9920592");
        }
        [Fact]
        public void DeterminingDnaHealthTest10()
        {
            var sr = new StreamReader(this.GetType().Assembly
                .GetManifestResourceStream(this.GetType(), "DeterminingDnaHealth.In10.txt"));
            From(() => sr.ReadLine()).ResultToString().ShouldBe("0 292498482");
        }

        [Fact]
        public void DeterminingDnaHealthTest30()
        {
            var sr = new StreamReader(this.GetType().Assembly
                .GetManifestResourceStream(this.GetType(), "DeterminingDnaHealth.In30.txt"));
            From(() => sr.ReadLine()).ResultToString().ShouldBe("12317773616 12317773616");
        }

    }
}
