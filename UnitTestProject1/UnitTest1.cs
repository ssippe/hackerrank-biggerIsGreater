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
        public void NextString()
        {
            Main.NextString("abc").ShouldBe("acb");
            Main.NextString("acb").ShouldBe("bac");



            Main.NextString("ab").ShouldBe("ba");
            Main.NextString("bb").ShouldBe("no answer");
            Main.NextString("hefg").ShouldBe("hegf");
            Main.NextString("dhck").ShouldBe("dhkc");
            Main.NextString("dkhc").ShouldBe("hcdk");



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
        public void GetIdx()
        {
            Main.GetIdx("abc").ShouldBe(new List<int> { 0, 0, 0 });
            Main.GetIdx("cba").ShouldBe(new List<int> { 2, 1, 0 });
            Main.GetIdx("acb").ShouldBe(new List<int> { 0, 1, 0 });
            Main.GetIdx("cab").ShouldBe(new List<int> { 2, 0, 0 });
            //Main.GetN("cba").ShouldBe(5);            
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

