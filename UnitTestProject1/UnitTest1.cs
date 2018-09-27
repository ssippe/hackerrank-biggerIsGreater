using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using ClassLibrary1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {


        [TestMethod]
        public void GetChar()
        {
            Main.DropChar("abc", 0).ShouldBe("bc");
            Main.DropChar("abc", 1).ShouldBe("ac");
        }

        [TestMethod]
        public void GetN()
        {
            Main.GetStringN("abc", new[] { 0, 0, 0 }).ShouldBe("abc");
            Main.GetStringN("abc", new[] { 2, 1, 0 }).ShouldBe("cba");
            Main.GetStringN("abc", new[] { 2, 0, 0 }).ShouldBe("cab");

        }

        [TestMethod]
        public void GetN2()
        {
            Main.GetStringN2("abc", 0).ShouldBe("abc");
            Main.GetStringN2("abc", 5).ShouldBe("cba");
            Main.GetStringN2("abc", 4).ShouldBe("cab");
            Main.GetStringN2("abcd", 23).ShouldBe("dcba");
            Main.GetStringN2("abcd", 0).ShouldBe("abcd");
            Main.GetStringN2("abcd", 1).ShouldBe("abdc");


        }

        [TestMethod]
        public void NextStringRepeat()
        {
            var s = "abbb";
            do
            {
                s = Main.NextString(s);
                Debug.WriteLine(s);
            } while (s != "no answer");

        }

        [TestMethod]
        public void NextString()
        {
            Main.NextString("abc").ShouldBe("acb");
            Main.NextString("acb").ShouldBe("bac");



            Main.NextString("ab").ShouldBe("ba");
            Main.NextString("bb").ShouldBe("no answer");
            Main.NextString("hefg").ShouldBe("hegf");
            Main.NextString("dhck").ShouldBe("dhkc");
            Main.NextString("dkhc").ShouldBe("hcdk");
            Main.NextString("bbba").ShouldBe("no answer");
            



            Main.NextString("cba").ShouldBe("no answer");

        }

        [TestMethod]
        public void NextStringLong()
        {
            Main.NextString("ocsmerkgidvddsazqxjbqlrrxcotrnfvtnlutlfcafdlwiismslaytqdbvlmcpapfbmzxmftrkkqvkpflxpezzapllerxyzlcf")
             .ShouldBe("ocsmerkgidvddsazqxjbqlrrxcotrnfvtnlutlfcafdlwiismslaytqdbvlmcpapfbmzxmftrkkqvkpflxpezzapllerxyzlfc");

            //Main.NextString("zyyxwwtrrnmlggfeb")
            //    .ShouldBe("no answer");
            Main.NextString(
                    "tccjaoahruyblpejzgkfnpmqoajnvqnvqmcdwpioxkrllofvixidannpvzxtpnzdtyxfkcloanztgkvgsngqxahnzmtrh")
                .ShouldBe(
                    "tccjaoahruyblpejzgkfnpmqoajnvqnvqmcdwpioxkrllofvixidannpvzxtpnzdtyxfkcloanztgkvgsngqxahnzrhmt");
        }

        [TestMethod]
        public void TestCase1()
        {
            //var wc = new WebClient();
            //var input =
            //    wc.DownloadString(
            //            "https://hr-testcases-us-east-1.s3.amazonaws.com/4187/input01.txt?AWSAccessKeyId=AKIAJ4WZFDFQTZRGO3QA&Expires=1537874570&Signature=awCohq5bD1KHhCceBff9rCT0Xhk%3D&response-content-type=text%2Fplain")
            //        .Split(Environment.NewLine)
            //        .Skip(1).ToList();
            //var output =
            //    wc.DownloadString(
            //        "https://hr-testcases-us-east-1.s3.amazonaws.com/4187/output01.txt?AWSAccessKeyId=AKIAJ4WZFDFQTZRGO3QA&Expires=1537874678&Signature=5mX5qLEdgC99gCdDWaaZMMvdjuc%3D&response-content-type=text%2Fplain").Split(Environment.NewLine)
            //        .Skip(1).ToList();

            var input =
                new StreamReader(this.GetType()
                        .Assembly.GetManifestResourceStream(this.GetType(), "TestCase1.Input.txt")).ReadToEnd()
                    .Split(Environment.NewLine)
                    .Skip(1)
                    .ToList();
            var output =
                new StreamReader(this.GetType()
                        .Assembly.GetManifestResourceStream(this.GetType(), "TestCase1.Output.txt")).ReadToEnd()
                    .Split(Environment.NewLine)
                    .Skip(1)
                    .ToList();
            var stopWatch = Stopwatch.StartNew();
            for (int i = 0; i < input.Count; i++)
            {
                Main.NextString(input[i]).ShouldBe(output[i]);
            }
            Debug.WriteLine(stopWatch.Elapsed);

        }

        [TestMethod]
        public void TestCase1Web()
        {
            var wc = new WebClient();
            var input =
                wc.DownloadString(
                        "https://hr-testcases-us-east-1.s3.amazonaws.com/4187/input01.txt?AWSAccessKeyId=AKIAJ4WZFDFQTZRGO3QA&Expires=1537874570&Signature=awCohq5bD1KHhCceBff9rCT0Xhk%3D&response-content-type=text%2Fplain")
                    .Split(Environment.NewLine)
                    .Skip(1).ToList();
            var output =
                wc.DownloadString(
                    "https://hr-testcases-us-east-1.s3.amazonaws.com/4187/output01.txt?AWSAccessKeyId=AKIAJ4WZFDFQTZRGO3QA&Expires=1537874678&Signature=5mX5qLEdgC99gCdDWaaZMMvdjuc%3D&response-content-type=text%2Fplain").Split(Environment.NewLine)
                    .Skip(1).ToList();


            var stopWatch = Stopwatch.StartNew();
            for (int i = 0; i < input.Count; i++)
            {
                Main.NextString(input[i]).ShouldBe(output[i]);
            }
            Debug.WriteLine(stopWatch.Elapsed);

        }

        [TestMethod]
        public void GetScore()
        {
            Main.GetScore("abc").ShouldBe(new List<int> { 0, 0, 0 });
            Main.GetScore("cba").ShouldBe(new List<int> { 2, 1, 0 });
            Main.GetScore("acb").ShouldBe(new List<int> { 0, 1, 0 });
            Main.GetScore("cab").ShouldBe(new List<int> { 2, 0, 0 });
            //Main.GetN("cba").ShouldBe(5);            
        }

        //[TestMethod]
        //public void GetScoreHelper()
        //{
        //    Main.GetScoreHelper(3,3).ShouldBe(18);
        //    Main.GetScoreHelper(2,2).ShouldBe(4);
        //}

        //[TestMethod]
        //public void GetScore()
        //{
        //    Main.GetScore("cba").ShouldBe(5);
        //    Main.GetScore("dcba").ShouldBe(23);
        //    Main.GetScore("dcab").ShouldBe(22);
        //    Main.GetScore("abdc").ShouldBe(1);
        //    Main.GetScore("abc").ShouldBe(0);
            
        //    Main.GetScore("ab").ShouldBe(0);
        //    Main.GetScore("ba").ShouldBe(1);
        //    Main.GetScore("abcd").ShouldBe(0);
        //    Main.GetScore("ocsmerkgidvddsazqxjbqlrrxcotrnfvtnlutlfcafdlwiismslaytqdbvlmcpapfbmzxmftrkkqvkpflxpezzapllerxyzlcf").ShouldBe(7593789072679204070L);
            


        //}

        [TestMethod]
        public void Bitshift()
        {
            (1 << 2).ShouldBe(4);
        }

        [TestMethod]
        public void AddOne()
        {
            Main.AddOne(new[] { 0, 0, 0 }).ShouldBe(new[] { 0, 1, 0 });
            Main.AddOne(new[] { 0, 1, 0 }).ShouldBe(new[] { 1, 0, 0 });
            Main.AddOne(new[] { 1, 0, 0 }).ShouldBe(new[] { 1, 1, 0 });
            Main.AddOne(new[] { 1, 1, 0 }).ShouldBe(new[] { 2, 0, 0 });
            Main.AddOne(new[] { 2, 1, 0 }).ShouldBe(null);
        }


    }
}

