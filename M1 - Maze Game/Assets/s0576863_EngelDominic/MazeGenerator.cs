using UnityEngine;

/*
 * I used the following links to create the recursive-backtracking algorithm
 * Sources:
 * http://weblog.jamisbuck.org/2010/12/27/maze-generation-recursive-backtracking
 * http://rosettacode.org/wiki/Maze_generation#Java
*/

public class MazeGenerator
{
    public int x;
    public int y;
    public int[,] maze;

    public MazeGenerator(int x, int y)
    {
        this.x = x;
        this.y = y;
        maze = new int[this.x, this.y];
        generateMaze(0, 0);
    }

    private void generateMaze(int cx, int cy)
    {
        DIR N = new DIR(1, 0, -1);
        DIR S = new DIR(2, 0, 1);
        DIR E = new DIR(4, 1, 0);
        DIR W = new DIR(8, -1, 0);


        N.opposite = S;
        S.opposite = N;
        E.opposite = W;
        W.opposite = E;

        DIR[] dirs = new DIR[] { N, S, E, W};

        /*
         * As there is no direct shuffle function for shuffling an array in C#, I needed to implement one.
         * Source:
         * https://developerslogblog.wordpress.com/2020/02/04/how-to-shuffle-an-array/
         */
        for (int i = dirs.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            DIR temp = dirs[i];
            dirs[i] = dirs[randomIndex];
            dirs[randomIndex] = temp;
        }

        foreach (DIR dir in dirs)
        {
            int nx = cx + dir.dx;
            int ny = cy + dir.dy;
            if (between(nx, x) && between(ny, y) && (maze[nx, ny] == 0))
            {
                maze[cx, cy] |= dir.bit;
                maze[nx, ny] |= dir.opposite.bit;
                generateMaze(nx, ny);
            }
        }
    }

    public class DIR
    {
        public int bit;
        public int dx;
        public int dy;
        public DIR opposite;

        public DIR() { }
        public DIR(int bit, int dx, int dy)
        {
            this.bit = bit;
            this.dx = dx;
            this.dy = dy;
        }
    }

    /*
     * As there is no "between" function in C#, I needed to implement one.
     * Source:
     * https://stackoverflow.com/questions/3188672/how-to-elegantly-check-if-a-number-is-within-a-range
     */
    public bool between(int a, int b)
    {
        return (a >= 0) && (a < b) ? true : false;
    }
}
