using System.Text;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test Program starts below...");

            Test_Question1();
            Test_Question3();
            Test_Question4();
        }

        private static void Test_Question1()
        {
            Test test = new Test();
            Console.WriteLine(test.MinMatchCount());
        }

        private static void Test_Question3()
        {
            Test test = new Test();
            Console.WriteLine(test.TestHarness("1010", "1011"));
            Console.WriteLine(test.TestHarness("1111101", "1011"));
        }

        private static void Test_Question4()
        {
            char[][] grid1 = {
            new char[]{'0','1','1','0','0'},
            new char[]{'0','1','1','0','0'},
            new char[]{'1','0','0','0','0'},
            new char[]{'0','0','0','1','1'} };
            //  Expected Output : 3

            char[][] grid2 = {
            new char[]{'0', '0', '1', '1', '0'},
            new char[]{'1', '1', '0', '1', '0'},
            new char[]{'1', '0', '0', '0', '1'},
            new char[]{'1', '0', '1', '0', '1'} };
            //  Expected Output : 4

            char[][] grid3 = {
            new char[]{ '0', '0', '0', '1', '1' },
            new char[]{ '0', '0', '1', '1', '0' },
            new char[]{ '1', '1', '1', '0', '1' },
            new char[]{ '0', '0', '1', '0', '1' } };
            //  Expected Output : 2

            Test test = new Test();
            Console.WriteLine(test.MartianCraterCount(grid1));
            Console.WriteLine(test.MartianCraterCount(grid2));
            Console.WriteLine(test.MartianCraterCount(grid3));
        }
    }


    public class Test
    {

        /*
        Question 1 - eSports Tournament
        EA is hosting an eSports tournament. There are 25 participants, and a maximum of 5 are allowed to compete in any given match. 
        What is the minimum number of matches required to find the top 3 players?

        Note: with each match you’re only able to determine their relative skill level, and skill levels don’t change between matches.
        With your answer, please provide an explanation - or attach any work associated with how you reached your answer.
        */

        // Explanation:
        // To be equally fair with any participants, I should allow all possible combination to form the team and play the match.
        // To minimize the number of matches, I should try to make any team with maximum of 5 member, whenever possible.
        // From each match of 5 members team, only 1 top player selected and remaining 4 members allowed to join new team for new match.
        // So,
        // after the 1st match: we should have 1 top player and 24 members to join new team for new match.
        // after the 2nd match: we should have 2 top players and 23 members to join new team for new match.        
        //  ....
        // after the last match:
        // we should create teams out of the all top players as found in the above iteration.
        // 
        // run next iteration with the same logic.
        // repeat the iterations till we find 'total top player count' == 3 
        //         
        //  ....
        // after the last match: we should have 5 top players and 4 members not to join new team and no more new match.
        // 
        // Now we've to select 3 top players among these 5 members
        // play 1 match with 5 members and select 1 top
        // play next match with 4 members and select 1 top
        // play next match with 3 members and select 1 top
        //
        // At this point, we've selected top 3 plauers among the 25 participants.
        // 

        // There are 25 participants, and a maximum of 5 are allowed to compete in any given match. 
        // What is the minimum number of matches required to find the top 3 players?
        public int MinMatchCount(int participants = 25, int teamSize = 5, int topPlayerNeeded = 3)
        {
            // input validation
            if (topPlayerNeeded < 1 || teamSize < 2 || participants <= topPlayerNeeded) return 0;

            int minMatchCount = 0;

            int availablePlayerCount = participants;
            int topPlayerCount = 0;

            while (topPlayerCount != topPlayerNeeded)
            {

                while (availablePlayerCount >= teamSize)
                {
                    topPlayerCount++;
                    minMatchCount++;
                    availablePlayerCount--;
                }

                if (topPlayerCount > topPlayerNeeded)
                {
                    // now, we need find 'topPlayerNeeded' within 'topPlayerCount'.
                    // go for next iteration.
                    availablePlayerCount = topPlayerCount;
                    topPlayerCount = 0;
                }
                else if (topPlayerCount < topPlayerNeeded)
                {
                    // here we've less number of players
                    // so, we've to play more games among 'availablePlayerCount - topPlayerCount' players

                    availablePlayerCount -= topPlayerCount;
                    while (availablePlayerCount > 1 && topPlayerCount < topPlayerNeeded)
                    {
                        topPlayerCount++;
                        minMatchCount++;
                        availablePlayerCount--;
                    }
                }
            }

            return minMatchCount;
        }


        // 'Question 2- Open Source Project' solution is in separate project. Zipped and attached separately.

        /*
        Question 3- Quantum Computing
        A quantum computer has successfully added two numbers and returned a result. You have been tasked with
        verifying the quantum computer’s accuracy. As this is a special dedicated test harness, you must write the
        conventional method yourself, taking strings containing binary numbers as input.
        */
        public string TestHarness(string a, string b)
        {
            if (string.IsNullOrEmpty(a) || string.IsNullOrEmpty(b))
                throw new ArgumentException("Invalid Input");

            // make both string of same length, otherwise I'll need to check their length multiple times
            // and accordingly I'll have to write multiple conditional code.
            StringBuilder result = new StringBuilder();
            int i = -1;
            if (a.Length > b.Length)
            {
                while (++i < a.Length - b.Length)
                {
                    result.Append('0');
                }
                b = result.ToString() + b;
            }
            else if (a.Length < b.Length)
            {
                while (++i < b.Length - a.Length)
                {
                    result.Append('0');
                }
                a = result.ToString() + a;
            }

            i = 0; result.Clear();
            int carry = 0;
            while (++i <= a.Length)
            {
                if (a[a.Length - i] == '0' && b[b.Length - i] == '0')
                {
                    result.Append(carry == 0 ? '0' : '1');
                    carry = 0;
                }
                else if (a[a.Length - i] == '0' && b[b.Length - i] == '1')
                {
                    result.Append(carry == 0 ? '1' : '0');
                    carry = carry == 0 ? 0 : 1;
                }
                else if (a[a.Length - i] == '1' && b[b.Length - i] == '0')
                {
                    result.Append(carry == 0 ? '1' : '0');
                    carry = carry == 0 ? 0 : 1;
                }
                else if (a[a.Length - i] == '1' && b[b.Length - i] == '1')
                {
                    result.Append(carry == 0 ? '0' : '1');
                    carry = 1;
                }
                else
                {
                    throw new ArgumentException("Invalid Input");
                }
            }

            if (carry == 1)
                result.Append('1');

            char ch1;
            i = -1;
            while (++i < result.Length / 2)
            {
                ch1 = result[i];
                result[i] = result[result.Length - 1 - i];
                result[result.Length - 1 - i] = ch1;
            }

            return result.ToString();
        }


        /*
        Question 4- Craters on Mars
        In order to move to Mars, a new next generation space travel company SpaceY has commissioned us to help
        map all the craters on the red planet, so a safe place can be found to land, with the least amount of craters.
        They will provide a [m x n] 2D grid, where a “1” indicates part of a crater, and a “0” indicates normal surface
        conditions. Your job is to determine how many craters exist in the area defined by the grid. Craters only run in
        horizontal and vertical directions (not diagonally). Both m and n will be values greater than 0 and less than or
        equal to 300.
        */

        public int MartianCraterCount(char[][] grid)
        {
            if (grid == null) return 0;
            int m = grid.GetLength(0);

            if (m > 300) return 0; // input condition violates, so return 0

            // Dictionary<int, List<int>> dictPathGroupIdPathIds = new();
            Dictionary<int, List<(int, int)>> dictPath = new();

            for (int y = 0; y < m; y++)
            {
                int n = grid[y].GetLength(0);
                if (n > 300) return 0; // input condition violates, so return 0
                for (int x = 0; x < n; x++)
                {
                    if (grid[y][x] == '1')
                    {

                        // check if (x,y) is adjacent to any existing path in 'dictPath'
                        var pathids = dictPath.Where(dictItem => dictItem.Value.Where(p1 => (p1.Item1 == x && Math.Abs(p1.Item2 - y) == 1)
                                                                                         || (p1.Item2 == y && Math.Abs(p1.Item1 - x) == 1)
                                                                                     ).Any())
                                                    .Select(di => di.Key).ToList<int>();

                        // if multiple adjacent paths found then
                        // merge all these paths to the first path and then add (x,y) to the merged(first) path
                        for (int k = 1; k < pathids.Count; k++)
                        {
                            dictPath[pathids[0]].AddRange(dictPath[pathids[k]]);
                            dictPath.Remove(pathids[k]);
                        }

                        if (pathids.Count == 0)
                        {
                            // if no adjacent path found
                            dictPath.Add(dictPath.Count, new List<(int, int)>() { new(x, y) });
                        }
                        else
                        {
                            // if adjacent path found
                            dictPath[pathids[0]].Add(new(x, y));
                        }
                    }
                }
            }

            return dictPath.Count;
        }
    }
}
