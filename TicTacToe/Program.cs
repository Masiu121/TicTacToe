using System;

namespace TicTacToe
{
    internal class Program
    {
        private static char[,] map;
        private static int cursorX, cursorY, cursorDrawX, cursorDrawY;
        private static bool exit, roundX;
        
        public static void Main(string[] args)
        {
            initialize();
            while(!exit)
            {
                draw();
                update();
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static void initialize()
        {
            map = new char[3,3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    map[i, j] = ' ';
                }
            }
            cursorX = 0;
            cursorY = 0;
            cursorDrawX = 3;
            cursorDrawY = 1;
            exit = false;
            roundX = true;
        }

        private static void draw()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.CursorVisible = false;
            Console.SetCursorPosition(2, 1);
            Console.WriteLine(" " + map[0, 0] + " | " + map[1, 0] + " | " + map[2, 0]);
            Console.SetCursorPosition(2, 2);
            Console.WriteLine("-----------");
            Console.SetCursorPosition(2, 3);
            Console.WriteLine(" " + map[0, 1] + " | " + map[1, 1] + " | " + map[2, 1]);
            Console.SetCursorPosition(2, 4);
            Console.WriteLine("-----------");
            Console.SetCursorPosition(2, 5);
            Console.WriteLine(" " + map[0, 2] + " | " + map[1, 2] + " | " + map[2, 2]);

            Console.SetCursorPosition(cursorDrawX, cursorDrawY);
            Console.ForegroundColor = ConsoleColor.Green;
            switch (map[cursorX, cursorY])
            {
                case 'X':
                    Console.Write("X");
                    break;
                case 'O':
                    Console.Write("O");
                    break;
                default:
                    Console.Write("_");
                    break;
            }
        }

        private static void update()
        {
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.UpArrow && cursorY > 0)
            {
                cursorDrawY -= 2;
                cursorY--;
            }
            else if (key.Key == ConsoleKey.DownArrow && cursorY < 2)
            {
                cursorDrawY += 2;
                cursorY++;
            }
            else if (key.Key == ConsoleKey.RightArrow && cursorX < 2)
            {
                cursorDrawX += 4;
                cursorX++;
            }
            else if (key.Key == ConsoleKey.LeftArrow && cursorX > 0)
            {
                cursorDrawX -= 4;
                cursorX--;
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                if (map[cursorX, cursorY] == ' ')
                {
                    if (roundX)
                    {
                        map[cursorX, cursorY] = 'X';
                        roundX = false;
                    }
                    else
                    {
                        map[cursorX, cursorY] = 'O';
                        roundX = true;
                    }
                }
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                exit = true;
            }

            char winner = checkForWin();
            if(winner != ' ')
                endGame(winner);
        }

        private static char checkForWin()
        {
            char player;
            
            for (int i = 0; i < 3; i++)
            {
                player = map[i, 0];
                if (player != ' ' && map[i, 1] == player && map[i, 2] == player)
                    return player;
            }
            
            for (int j = 0; j < 3; j++)
            {
                player = map[0, j];
                if (player != ' ' && map[1, j] == player && map[2, j] == player)
                    return player;
            }

            player = map[0, 0];
            if (player != ' ' && map[1, 1] == player && map[2, 2] == player)
                return player;

            player = map[2, 0];
            if (player != ' ' && map[1, 1] == player && map[0, 2] == player)
                return player;

            bool mapFull = true;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (map[i, j] == ' ')
                        mapFull = false;

            if (mapFull)
                return 'D';

            return ' ';
        }

        private static void endGame(char winner)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;
            if (winner != 'D')
                Console.WriteLine(winner + " won!");
            else
                Console.WriteLine("Draw!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            exit = true;
        }
    }
}