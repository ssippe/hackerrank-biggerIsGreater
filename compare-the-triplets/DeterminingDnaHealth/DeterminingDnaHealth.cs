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
            public string Value;
            public Dictionary<char, Node> Children;// = new Dictionary<char, Node>();
            public List<int> Idxs;// = new List<int>();
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
            var nextChar = searchText[depth];
            if (node.Children == null || !node.Children.ContainsKey(nextChar))
            {
                return null;
            }

            return SearchTree(node.Children[nextChar], searchText, depth+1);
        }

        public static Node BuildTree(string[] genes, int[] healthVals)
        {
            var root = new Node();
            for (int i = 0; i < genes.Length; i++)
            {
                BuildTree2(root, 0, genes[i], i);
            }

            return root;
        }

        private static void BuildTree2(Node node, int depth, string gene, int idx)
        {
            var nextChar = gene[depth];
            if (node.Children == null)
            {
                node.Children = new Dictionary<char, Node>();
                node.Children.Add(nextChar, new Node());
            }
            else if (!node.Children.ContainsKey(nextChar))
            {
                node.Children.Add(nextChar,new  Node() );
            }
            var child = node.Children[nextChar];
            if (depth + 1 == gene.Length)
            {
                if (child.Idxs == null)
                {
                    child.Idxs=new List<int>();
                }
                child.Value = gene;
                child.Idxs.Add(idx);
                return;
            }

            BuildTree2(child, depth + 1, gene,  idx);
        }

        public class Input
        {
            public string[] Genes;
            public int[] HeathVals;
            public int DnaCount;
            public long MinHealth = long.MaxValue;
            public long MaxHealth= long.MinValue;
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

            
            static Input ReadHeader()
            {
                var geneCount = Convert.ToInt32(Console.ReadLine());
                var genes = Console.ReadLine().Split(' ');
                var heathVals = Array.ConvertAll(Console.ReadLine().Split(' '), healthTemp => Convert.ToInt32(healthTemp));
                int dnaCount = Convert.ToInt32(Console.ReadLine());
                
                var treeRoot = BuildTree(genes, heathVals);
                return new Input { DnaCount = dnaCount, Genes = genes, HeathVals = heathVals, Root = treeRoot };
            }

            public long ProcessDnaLine(string s)
            {
                //var sw = Stopwatch.StartNew();
                string[] splits = s.Split(' ');
                long first = long.Parse(splits[0]);
                long last = long.Parse(splits[1]);
                string dna = splits[2];
                long healthSum = 0;
                //var t0 = sw.Elapsed;
                for (int i = 0; i < dna.Length; i++)
                {
                    for (int j = 1; j <= dna.Length - i; j++)
                    {
                        var searchText = dna.Substring(i, j);
                        var node = SearchTree(Root, searchText);
                        if (node == null)
                        {
                            break;
                        }

                        if (node.Value == searchText)
                        {
                            long health = 0;
                            for (int k = 0; k < node.Idxs.Count; k++)
                            {
                                var idx = node.Idxs[k];
                                if (idx < first || idx > last)
                                    continue;
                                health += HeathVals[idx];
                            }
                            healthSum += health;
                        }

                    }
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
