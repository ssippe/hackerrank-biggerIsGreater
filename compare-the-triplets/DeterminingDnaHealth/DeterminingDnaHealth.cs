using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Shouldly;
using Xunit;

namespace compare_the_triplets.DeterminingDnaHealth
{
    public class DeterminingDnaHealth
    {

        public class Node
        {
            public bool IsToken;
            public Node[] Children;
            public int[] Idxs;// = new List<int>();
            public int[] HealthVals;

            public long GetHeath(int first, int last)
            {
                if (Idxs == null)
                    return 0;
                long health = 0;
                for (int k = 0; k < Idxs.Length; k++)
                {
                    var idx = Idxs[k];
                    if (idx < first || idx > last)
                        continue;
                    health += HealthVals[k];
                }
                return health;
            }
        }

        public const int CharA = 97;




        public static Node BuildTree(string[] genes, int[] healthVals)
        {
            var root = new Node();
            for (int i = 0; i < genes.Length; i++)
            {
                BuildTree2(root, 0, genes[i], i, healthVals[i]);
            }

            return root;
        }

        private static void BuildTree2(Node node, int depth, string gene, int idx, int healthVal)
        {
            var nextCharIdx = gene[depth] - CharA;
            if (node.Children == null)
            {
                node.Children = new Node[26];
                node.Children[nextCharIdx] = new Node();
            }
            else if (node.Children[nextCharIdx] == null)
            {
                node.Children[nextCharIdx] = new Node();
            }
            var child = node.Children[nextCharIdx];
            if (depth + 1 == gene.Length)
            {
                if (child.Idxs == null)
                {
                    child.Idxs = new int[] { };
                    child.HealthVals = new int[] { };
                }

                child.IsToken = true;
                child.Idxs = child.Idxs.Concat(new[] { idx }).ToArray();
                child.HealthVals = child.HealthVals.Concat(new[] { healthVal }).ToArray();
                return;
            }

            BuildTree2(child, depth + 1, gene, idx, healthVal);
        }

        public class Input
        {
            public string[] Genes;
            public int[] HeathVals;
            public int DnaCount;
            public long MinHealth = long.MaxValue;
            public long MaxHealth = long.MinValue;
            public Node Root;

            public static Input FromReadLine()
            {
                var input = ReadHeader();
                for (int i = 0; i < input.DnaCount; i++)
                {
                    var health = input.ProcessDnaLine(Console.ReadLine());
                    input.MaxHealth = Math.Max(health, input.MaxHealth);
                    input.MinHealth = Math.Min(health, input.MinHealth);
                }

                input.MinHealth = input.MinHealth == long.MaxValue ? input.MaxHealth : input.MinHealth;
                input.MaxHealth = input.MaxHealth == long.MinValue ? input.MinHealth : input.MaxHealth;
                return input;
            }




            // iteration #1 3.5s per 100
            // iteration #2 1.8s per 100
            // iteration #3 0.1s per 100
            // iteration #5 0.025s per 100
            // iteration #6 0.01s per 100
            // iteration #7 0.008 per 100


            static Input ReadHeader()
            {
                var geneCount = Convert.ToInt32(Console.ReadLine());
                var genes = Console.ReadLine().Split(' ');
                var heathVals = Array.ConvertAll(Console.ReadLine().Split(' '), healthTemp => Convert.ToInt32(healthTemp));
                int dnaCount = Convert.ToInt32(Console.ReadLine());

                var treeRoot = BuildTree(genes, heathVals);
                return new Input { DnaCount = dnaCount, Genes = genes, HeathVals = heathVals, Root = treeRoot };
            }

            public long SearchTree(string dna, int dnaStartIdx, int healthFirst, int healthLast)
            {
                var node = Root;
                long score = 0;
                var dnaLength = 1;
                while (true)
                {
                    var currScore = node.GetHeath(healthFirst, healthLast);
                    score += currScore;
                    if (dnaStartIdx + dnaLength > dna.Length)
                    {
                        return score;
                    }

                    if (node.Children == null)
                    {
                        return score;
                    }

                    var nextCharIdx = dna[dnaStartIdx + dnaLength - 1] - CharA;
                    var child = node.Children[nextCharIdx];
                    if (child == null)
                    {
                        return score;
                    }

                    node = child;
                    dnaLength += 1;
                    //return SearchTree(child, dna, dnaStartIdx, dnaLength + 1, currScore + prevScore, healthFirst,
                    //    healthLast);
                }
            }

            public long ProcessDnaLine(string s)
            {
                //var sw = Stopwatch.StartNew();
                string[] splits = s.Split(' ');
                int first = int.Parse(splits[0]);
                int last = int.Parse(splits[1]);
                string dna = splits[2];
                long healthSum = 0;
                //var t0 = sw.Elapsed;
                for (int i = 0; i < dna.Length; i++)
                {
                    var heath = SearchTree(dna, i, first, last);
                    healthSum += heath;
                }

                //var t1 = sw.Elapsed - t0;
                //var totalTicks = sw.ElapsedTicks;
                //Debug.WriteLine($"PDL t0/1 = {(double)t0.Ticks / totalTicks:0.00}/{(double)t1.Ticks / totalTicks:0.00}");
                return healthSum;
            }

            public string ResultToString() => $"{MinHealth} {MaxHealth}";
        }

        //static void Main(string[] args)
        //{
        //    var input = Input.FromReadLine();
        //    Console.WriteLine(input.ResultToString());
        //}


    }
}
