using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Shouldly;
using Xunit;

namespace compare_the_triplets
{
    public class QueensAttack2
    {
        enum Direction
        {
            N,
            Ne,
            E,
            Se,
            S,
            Sw,
            W,
            Nw
        }

        static Point NextPoint(Point p, Direction d, int stepCount)
        {
            int dR = 0;
            int dC = 0;
            switch (d)
            {
                case Direction.N:
                    dR = +stepCount;
                    break;
                case Direction.S:
                    dR = -stepCount;
                    break;
                case Direction.E:
                    dC = +stepCount;
                    break;
                case Direction.W:
                    dC = -stepCount;
                    break;
                case Direction.Ne:
                    dR = +stepCount;
                    dC = +stepCount;
                    break;
                case Direction.Se:
                    dR = -stepCount;
                    dC = +stepCount;
                    break;
                case Direction.Sw:
                    dR = -stepCount;
                    dC = -stepCount;
                    break;
                case Direction.Nw:
                    dR = +stepCount;
                    dC = -stepCount;
                    break;
            }
            return new Point {C = p.C + dC, R = p.R + dR};
        }

        static bool CanMove(Direction d, Point queenPoint, int n, int stepCount, Point[] obstacles)
        {
            var nextPoint = NextPoint(queenPoint, d, stepCount);
            if (!nextPoint.IsValid(n))
                return false;

            if (obstacles.Any(f => nextPoint.Eq(f)))
                return false;
            return true;
        }

        static int DirCount(Direction d, Point queenPoint, int n, Point[] obstacles)
        {

            int stepCount = 0;
            while (CanMove(d, queenPoint, n, stepCount + 1, obstacles))
            {
                stepCount += 1;
            }       
            return stepCount;
        }



        static int queensAttack(Input input)
        {

            //var res = Enum.GetValues(typeof(Direction)).Cast<Direction>()
            //    .Select(d => DirCount(d, input.QueenLoc, input.BoardSide, input.Obstacles)).ToList();
            //return res.Sum();
            return input.Moves;
        }

        public struct Point
        {
            public int R;
            public int C;

            public Point(string s)
            {
                var ps = s.Split(' ').Select(int.Parse).ToArray();
                R = ps[0];
                C = ps[1];
            }

            public bool IsValid(int boardSize)
            {
                return R > 0 && C > 0 && R <= boardSize && C <= boardSize;
            }

            public bool Eq(Point p) => R == p.R && C == p.C;
            public int Steps(Point p) => Math.Max(Math.Abs(R - p.R), Math.Abs(C - p.C));
            public override string ToString()
            {
                return $"{C},{R}";
            }
        }

        public class Input
        {
            

            public Point QueenLoc { get; }
            public Point[] Obstacles { get; }
            public int BoardSide { get; }
            Dictionary<Direction, int> DirMoves { get; }
            public int Moves => DirMoves.Values.Sum();
            

            public Input(string s) : this(s.Split(new []{ Environment.NewLine},StringSplitOptions.RemoveEmptyEntries)) { }
           
            public Input(string[] lines)
            {
                BoardSide = int.Parse(lines[0].Split(' ')[0]);
                var obstacleCount = int.Parse(lines[0].Split(' ')[1]);
                QueenLoc = new Point(lines[1]);
                Obstacles = Enumerable.Range(0, obstacleCount).Select(i => new Point(lines[i + 2])).ToArray();
                DirMoves = InitialMoves(QueenLoc, BoardSide);
                for (int i = 0; i < obstacleCount; i++)
                {
                    var oPoint = new Point(lines[i + 2]);
                    var dir = GetDirection(QueenLoc, oPoint);
                    if (dir == null)
                        continue;
                    DirMoves[dir.Value] = Math.Min(DirMoves[dir.Value], QueenLoc.Steps(oPoint)-1);
                }                
            }

            public static Input FromReadLine()
            {
                var lines = new List<string>();
                lines.Add(Console.ReadLine());
                var obstacleCount = int.Parse(lines[0].Split(' ')[1]);
                lines.Add(Console.ReadLine());
                for(int i=0;i<obstacleCount;i++)
                    lines.Add(Console.ReadLine());
                return new Input(lines.ToArray());                
            }

            Direction? GetDirection(Point p1, Point p2)
            {
                if (p1.C == p2.C)
                {
                    return p1.R < p2.R ? Direction.N : Direction.S;
                }
                if (p1.R == p2.R)
                {
                    return p1.C < p2.C ? Direction.E : Direction.W;
                }
                var dC = p2.C - p1.C;
                var dR = p2.R - p1.R;
                if (dC == dR)
                {
                    return dC > 0 ? Direction.Ne : Direction.Sw;
                }
                if (dC == -dR)
                {
                    return dC > 0 ? Direction.Se : Direction.Nw;
                }
                return null;                
            }

            static Dictionary<Direction, int> InitialMoves(Point queenPoint, int boardSize) =>
                Enum.GetValues(typeof(Direction)).Cast<Direction>().ToDictionary(k => k, d =>
                {
                    switch (d)
                    {
                        case Direction.N:
                            return boardSize - queenPoint.R;
                        case Direction.S:
                            return queenPoint.R - 1;
                        case Direction.E:
                            return boardSize - queenPoint.C;
                        case Direction.W:
                            return queenPoint.C - 1;
                        case Direction.Ne:
                            return Math.Min(boardSize - queenPoint.R, boardSize - queenPoint.C);
                        case Direction.Se:
                            return Math.Min(queenPoint.R - 1, boardSize - queenPoint.C);
                        case Direction.Sw:
                            return Math.Min(queenPoint.R - 1, queenPoint.C - 1);
                        case Direction.Nw:
                            return Math.Min(boardSize - queenPoint.R, queenPoint.C - 1);
                    }
                    throw new Exception("not found");
                });

        }

        static void Main2(string[] args)
        {
            TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

            var input = Input.FromReadLine();

            int result = queensAttack(input);

            textWriter.WriteLine(result);

            textWriter.Flush();
            textWriter.Close();
        }


        [Fact]
        public void Test1()
        {
            queensAttack(new Input(@"5 3
4 3
5 5
4 2
2 3")).ShouldBe(10);
            queensAttack(new Input(@"4 0
4 4")).ShouldBe(9);
            queensAttack(new Input(@"1 0
1 1")).ShouldBe(0);

        }
    }
}
