using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Shouldly;
using Xunit;

namespace compare_the_triplets
{
    public class DeterminingDnaHealth
    {
        //store the longest gene so we know when to stop. there could be 1 really long gene?
        //linked list store a->aa->aab->aaba etc.
        //tree of gene values for binary seach 
        public static string GetNextGene(int idx, string dna, IReadOnlyList<string> genes)
        {
            var remainingDna = dna.Substring(idx);
            return genes.FirstOrDefault(gene => remainingDna.StartsWith(gene));
        }

        public class Input
        {
            public IReadOnlyList<string> Genes { get; private set; }
            public IReadOnlyList<int> HeathVals { get; private set; }
            public int DnaCount { get; private set; }
            public int MinHealth { get; private set; } = Int32.MaxValue;
            public int MaxHeath { get; private set; } = Int32.MinValue;
            public int DnaProcessedCount { get; private set; }

            

            public static Input FromReadLine()
            {
                return From(() => Console.ReadLine());               
            }


            public static Input FromString(string s)
            {
                var splits = s.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
                int i = 0;
                return From(() => splits[i++]);
            }

            // iteration #1 3.5s per 100
            // iteration #2 1.8s per 100

            public static Input From(Func<string> newLineFunc)
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
                        Debug.WriteLine($"{si++} {stopWatch.Elapsed} {i}/{input.DnaCount}, {stopWatch.Elapsed-lastTime} per 100");
                        lastTime = stopWatch.Elapsed;
                    }
                    var health= input.ProcessDnaLine(newLineFunc());
                    input.MaxHeath = Math.Max(health, input.MaxHeath);
                    input.MinHealth = Math.Min(health, input.MinHealth);
                }
                return input;
            }


            static Input ReadHeader(List<string> lines)
            {
                
                var geneCount = int.Parse(lines[0]);
                var genes = lines[1].Split(' ');
                var heathVals = lines[2].Split(' ').Select(int.Parse).ToList();
                int dnaCount = int.Parse(lines[3]);
                return new Input {DnaCount = dnaCount, Genes = genes, HeathVals = heathVals};
            }

            int ProcessDnaLine(string s)
            {
                var sw = Stopwatch.StartNew();
                var splits = s.Split(' ');
                int first = int.Parse(splits[0]);
                int last = int.Parse(splits[1]);
                var dna = splits[2];
                var genes2 = Genes.Skip(first).Take(last - first + 1).ToList();
                var heath2 = HeathVals.Skip(first).Take(last - first + 1).ToList();
                var healthDict = genes2.Select((gene, idx) => new {gene, idx}).GroupBy(f => f.gene)
                    .ToDictionary(k => k.Key, v => v.Select(g => heath2[g.idx]).Sum());
                int healthSum = 0;
                var t0 = sw.Elapsed;
                for (int i = 0; i < dna.Length; i++)
                {
                    var gene = GetNextGene(i, dna, genes2);
                    if (gene == null) 
                        continue;                    
                    var healthSumLocal = healthDict[gene];
                    healthSum += healthSumLocal;
                }

                var t1 = sw.Elapsed - t0;
                var totalTicks = sw.ElapsedTicks;
                Debug.WriteLine($"PDL t0/1 = {(double)t0.Ticks/totalTicks:0.00}/{(double)t1.Ticks/totalTicks:0.00}");
                return healthSum;
            }
            
            public string ResultToString() => $"{MinHealth} {MaxHeath}";
        }

        //static void Main(string[] args)
        //{
        //    var input = Input.FromReadLine();
        //    Console.WriteLine(input.ResultToString());
        //}

        [Fact]
        public void DeterminingDnaHealthTest0()
        {
            Input.FromString(@"6
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
            Input.From(() => sr.ReadLine()).ResultToString().ShouldBe("15806635 20688978289");
        }
    }
}
