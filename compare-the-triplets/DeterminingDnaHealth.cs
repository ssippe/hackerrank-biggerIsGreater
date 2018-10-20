using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using Shouldly;
using Xunit;

namespace compare_the_triplets
{
    public class DeterminingDnaHealth
    {
        //store the longest gene so we know when to stop. there could be 1 really long gene?
        //linked list store a->aa->aab->aaba etc.
        //tree of gene values for binary seach 
        //prepare dic outside
        public static string GetNextGene(int idx, string dna, IReadOnlyList<string> genes)
        {
            var remainingDna = dna.Substring(idx);
            return genes.FirstOrDefault(gene => remainingDna.StartsWith(gene));
        }

        public class Node
        {
            public string Value;
            public Dictionary<int, int> IdxHealthDict = new Dictionary<int, int>();
            public Dictionary<char, Node> Children = new Dictionary<char, Node>();
        }

        public static Node SearchTree(Node node, string searchText, int depth = 0)
        {
            //exact match
            if (node.Value == searchText) return node;
            if (depth == searchText.Length)
            {
                if (node.Children.Any())
                {
                    //adding to searchText may get match
                    return node;
                }
                //adding to searchText with not get match
                return null;
            }
            var nextChar = searchText.Substring(depth, 1)[0];
            if (!node.Children.ContainsKey(nextChar)) return null;
            return SearchTree(node.Children[nextChar], searchText, depth+1);
        }

        public static Node BuildTree(string[] genes, int[] healthVals)
        {
            var root = new Node();
            for (int i = 0; i < genes.Length; i++)
            {
                BuildTree2(root, 0, genes[i], healthVals[i], i);
            }

            return root;
        }

        private static void BuildTree2(Node node, int depth, string gene, int health, int idx)
        {
            var nextChar = gene.Substring(depth, 1)[0];
            if (!node.Children.ContainsKey(nextChar))
            {
                node.Children.Add(nextChar,new  Node() );
            }
            var child = node.Children[nextChar];
            if (depth + 1 == gene.Length)
            {
                child.Value = gene;
                child.IdxHealthDict.Add(idx, health);
                return;
            }

            BuildTree2(child, depth + 1, gene, health, idx);
        }

        public class Input
        {
            public IReadOnlyList<string> Genes { get; private set; }
            public IReadOnlyList<int> HeathVals { get; private set; }
            public int DnaCount { get; private set; }
            public long MinHealth { get; private set; } = Int32.MaxValue;
            public long MaxHeath { get; private set; } = Int32.MinValue;
            public int DnaProcessedCount { get; private set; }
            private Node _root;



            public static Input FromReadLine()
            {
                return From(() => Console.ReadLine());
            }


            public static Input FromString(string s)
            {
                var splits = s.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                int i = 0;
                return From(() => splits[i++]);
            }

            // iteration #1 3.5s per 100
            // iteration #2 1.8s per 100
            // iteration #3 0.1s per 100
            // iteration #5 0.025s per 100

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
                        Debug.WriteLine($"{si++} {stopWatch.Elapsed} {i}/{input.DnaCount}, {stopWatch.Elapsed - lastTime} per 100");
                        lastTime = stopWatch.Elapsed;
                    }
                    var health = input.ProcessDnaLine(newLineFunc());
                    input.MaxHeath = Math.Max(health, input.MaxHeath);
                    input.MinHealth = Math.Min(health, input.MinHealth);
                }
                return input;
            }


            static Input ReadHeader(List<string> lines)
            {

                var geneCount = int.Parse(lines[0]);
                var genes = lines[1].Split(' ');
                var heathVals = lines[2].Split(' ').Select(int.Parse).ToArray();
                int dnaCount = int.Parse(lines[3]);
                var treeRoot = BuildTree(genes, heathVals);
                return new Input { DnaCount = dnaCount, Genes = genes, HeathVals = heathVals, _root = treeRoot };
            }

            long ProcessDnaLine(string s)
            {
                //var sw = Stopwatch.StartNew();
                var splits = s.Split(' ');
                int first = int.Parse(splits[0]);
                int last = int.Parse(splits[1]);
                var dna = splits[2];
                long healthSum = 0;
                //var t0 = sw.Elapsed;
                for (int i = 0; i < dna.Length; i++)
                {
                    for (int j = 1; j <= dna.Length - i; j++)
                    {
                        var searchText = dna.Substring(i, j);
                        var node = SearchTree(_root, searchText);
                        if (node == null)
                        {
                            break;
                        }

                        if (node.Value == searchText)
                        {
                            var heath = node.IdxHealthDict.Where(f => first <= f.Key && f.Key <= last)
                                .Select(f => f.Value).Sum();
                            healthSum += heath;
                        }
                    }
                }

                //var t1 = sw.Elapsed - t0;
                //var totalTicks = sw.ElapsedTicks;
                //Debug.WriteLine($"PDL t0/1 = {(double)t0.Ticks / totalTicks:0.00}/{(double)t1.Ticks / totalTicks:0.00}");
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

        [Fact]
        public void DeterminingDnaHealthTest8()
        {
            var sr = new StreamReader(this.GetType().Assembly
                .GetManifestResourceStream(this.GetType(), "DeterminingDnaHealth.In8.txt"));
            Input.From(() => sr.ReadLine()).ResultToString().ShouldBe("0 8652768");
        }

        //[Fact]
        //public void BuildTreeTest()
        //{
        //    var genes = new[] {"a", "ab", "bab", "bc", "bca", "c", "caa"};
        //    var root = BuildTree(genes);
        //}
    }
}
