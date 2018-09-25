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


        public static int[] GetIdx(string w)
        {
            var idx = new int[w.Length];
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

        public static string DropChar(string w, int idx)
        {
            var wArray = w.ToCharArray();
            var chars = new char[w.Length - 1];
            if (idx > 0)
            {
                Array.Copy(wArray, chars, idx);
            }
            if (idx < w.Length - 1)
            {
                Array.Copy(wArray, idx + 1, chars, idx, w.Length - 1 - idx);
            }
            return new string(chars);

        }

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

        public static string NextString(string w)
        {
            var stopWatch = Stopwatch.StartNew();
            var idx = GetIdx(w);
            Debug.WriteLine("after getidx " + stopWatch.Elapsed);
            while(true)
            {
                idx = AddOne(idx);
                Debug.WriteLine("after addone" + stopWatch.Elapsed);
                if (idx == null)
                {
                    return "no answer";
                }
                var next = GetStringN(w, idx);
                Debug.WriteLine("after getstringn" + stopWatch.Elapsed);
                if (next != w)
                    return next;
            } 
            
            //while (GetStringN(w,idx2)==w)
        }





    }
}
