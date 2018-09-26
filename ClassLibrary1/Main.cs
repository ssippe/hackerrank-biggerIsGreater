using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ClassLibrary1
{
    public class Main
    {

        // Complete the biggerIsGreater function below.
        static string biggerIsGreater(string w) => NextString(w);

        static string ToOrdered(string s) => new string(s.OrderBy(f => f).ToArray());
        static int GetOrderedIndex(string s, char c) => ToOrdered(s).IndexOf(c);

        //https://www.hackerrank.com/challenges/bigger-is-greater/problem


        static int IPow(int x, int exp)
        {
            int result = 1;
            while (exp > 0)
            {
                if ((exp & 1) != 0)
                {
                    result *= x;
                }
                exp >>= 1;
                x *= x;
            }
            return result;
        }

        public static long Fact(int i)
        {
            if (i == 0)
                return 1;
            return i * Fact(i - 1);
           
        }

        public static long GetScoreHelper(int i, int n)
        {
            return (i) * Fact(n);
        }

        public static long GetScore(string w)
        {
            var wOrdered = ToOrdered(w);
            long sum = 0;
            for (int i = 0; i < w.Length; i++)
            {
                var n = w.Length - 1 - i;
                var idx = wOrdered.IndexOf(w[i]);
                wOrdered = DropChar(wOrdered, idx);
                var iScore = GetScoreHelper(idx, n);
                Debug.WriteLine(iScore);
                sum += iScore;
            }
            return sum;
        }

        public static int[] GetIdx(string w)
        {
            var idx = new Queue<int>();
            var wOrdered = ToOrdered(w);
            var wRem = new Queue<char>(w);

            while (wRem.Count > 0)
            {
                idx.Enqueue(GetOrderedIndex(new string(wRem.ToArray()), wRem.Peek()));
                wRem.Dequeue();
            }
            return idx.ToArray();
        }

        public static int[] AddOne(int[] idx)
        {
            var idxLen = idx.Length;
            var addOk = false;
            for (var i = idxLen-2; i >= 0; i--)
            {                
                if (!addOk)
                {
                    var iTotal = idx[i] + 1;
                    var iMax = idxLen - 1 - i;
                    if (iTotal <= iMax)
                    {
                        idx[i] = iTotal;
                        addOk = true;
                    }
                    else
                    {
                        idx[i] = 0;
                    }
                }
                else
                {
                    //idx2[i] = idx[i];
                }

            }

            if (!addOk)
                return null;
            
            return idx;
        }

        public static string DropChar(string w, int idx) => w.Remove(idx, 1);
        

        //public static Tuple<char, string> GetChar(string w, int idx)
        //{
        //    var c = w[idx];

        //    var s = new string(w.Where((f, fidx) => fidx != idx).ToArray());
        //    return Tuple.Create(c, s);
        //}

        public static string GetStringN(string w, int[] idx)
        {
            var wRem = ToOrdered(w);
            string result = "";
            while (wRem.Length > 0)
            {
                var i = idx[result.Length];                
                result += wRem[i];
                wRem = DropChar(wRem, i);
            }

            return result;
        }

        public static string GetStringN2(string w, long n)
        {
            var wOrdered = ToOrdered(w);
            var result = new char[w.Length];
            long nRem = n;
            for (int i = 0; i < w.Length; i++)
            {
                var divisorAtI = Fact(w.Length - i - 1);
                var idxAtI =(int) (nRem / divisorAtI);
                if (idxAtI > wOrdered.Length - 1)
                {
                    return null;
                }
                result[i] = wOrdered[idxAtI];
                nRem -= idxAtI * divisorAtI;
                wOrdered = DropChar(wOrdered, idxAtI);
            }

            return new string(result);            
        }

        public static string NextString(string w)
        {
            var stopWatch = Stopwatch.StartNew();
            var score = GetScore(w);
            Debug.WriteLine("after getidx " + stopWatch.Elapsed);
            while(true)
            {
                score += 1;
                Debug.WriteLine("after addone" + stopWatch.Elapsed);
                
                var next = GetStringN2(w, score);
                if (next == null)
                    return "no answer";
                Debug.WriteLine("after getstringn" + stopWatch.Elapsed);
                if (next != w)
                    return next;
            } 
            
            //while (GetStringN(w,idx2)==w)
        }





    }
}
