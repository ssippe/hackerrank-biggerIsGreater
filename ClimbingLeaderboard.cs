using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Shouldly;
using Xunit;

namespace compare_the_triplets
{
    public class ClimbingLeaderboard
    {

        static int[] climbingLeaderboard(int[] scores, int[] alice)
        {
            var ranks = new int[alice.Length];
            var i = alice.Length - 1;
            var j = 0;
            var currRank = 1;
            while (true)
            {
                if (i < 0)
                {
                    break;
                }

                if (j > scores.Length - 1)
                {
                    ranks[i] = currRank;
                    i--;
                    continue;
                }

                if (alice[i] >= scores[j])
                {
                    ranks[i] = currRank;
                    i--;
                    continue;                    
                }
                if (j == 0 || scores[j] != scores[j - 1])
                {
                    currRank += 1;                    
                }
                j++;

            }

            return ranks;
            
            
            //var dscores = scores.Distinct().ToArray();
            //return alice.Select(score => rank(dscores, score)).ToArray();

        }

        //static int rank(int[] scores, int score)
        //{
        //    int rank = 1;
        //    for(int i=0; i<scores.Length; i++)
        //    {
        //        if (score >= scores[i])
        //            return i + 1;
        //    }

        //    return scores.Length + 1;
        //}

        [Fact]
        public void Test1()
        {
            climbingLeaderboard(ToArr("100 90 90 80 75 60"), ToArr("50 65 77 90 102")).ShouldBe(ToArr("6 5 4 2 1"));
        }

        int[] ToArr(string s) => s.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
    }
}
