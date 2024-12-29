namespace Arkanoid2
{
    /*
 * Autor: Gabriel Hernández Collado
 * Fecha: 19-12-2024
 * Descripción:
 * 
 */

    using System;
    using System.Threading;
    using System.Media;

    public class Arkanoid
    {
        static SoundPlayer barReboundSound = new SoundPlayer(@"Sounds\barRebound.wav");
        static SoundPlayer brickReboundSound = new SoundPlayer(@"Sounds\brickRebound.wav");
        static int rows = 5;
        static int cols = Console.WindowWidth / 4;
        static int brickWidth = 4;
        static int brickHeight = 1;
        static int barX;
        static int barY;
        static int ballX;
        static int ballY;
        static int dx = 1;
        static int dy = 1;
        static bool running = true;

        public static void DrawBrick(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("▓▓▓▓");
        }

        public static void EraseBrick(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("    ");
        }

        public static bool[,] DrawBricks()
        {
            bool[,] bricks = new bool[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    int x = j * brickWidth;
                    int y = i * brickHeight;
                    bricks[i, j] = true;
                    if (i % 2 == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                    }

                    DrawBrick(x, y);
                    Console.ResetColor();
                }
            }
            return bricks;
        }
        public static void BrickCollision(int rows, int cols, bool[,] bricks)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (bricks[i, j])
                    {
                         int brickX = j * brickWidth;
                         int brickY = i * brickHeight;


                        if (ballX >= brickX && ballX < brickX + brickWidth &&
                            ballY == brickY)
                        {
                            dy *= -1;
                            brickReboundSound.Play();
                            EraseBrick(brickX, brickY);
                            bricks[i, j] = false;
                            break;
                        }
                    }
                }
            }
        }
        public static void MoveBar(ConsoleKeyInfo? key)
        {
            if (key.HasValue)
            {
                switch (key.Value.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (barX > 0)
                        {
                            EraseBar(barX + 8, barY);
                            barX -= 2; // Velocidad barra
                            DrawBar(barX, barY);
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (barX < Console.WindowWidth - 16)
                        {
                            EraseBar(barX, barY);
                            barX += 2; // Velocidad barra
                            DrawBar(barX, barY);
                        }
                        break;
                }
            }
        }

        public static void DrawBar(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓");
            Console.ResetColor();
        }

        public static void EraseBar(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("        ");
        }

        public static void MoveBall()
        {
            EraseBall(ballX, ballY);
            ballX += dx;
            ballY += dy;

            // Colisiones consola
            if (ballX >= Console.WindowWidth - 1 || ballX <= 0)
            {
                dx *= -1;
                ballX = Math.Clamp(ballX, 0, Console.WindowWidth - 1); // Mantener dentro del rango
            }

            if (ballY >= Console.WindowHeight - 1 || ballY <= 0)
            {
                dy *= -1;
                ballY = Math.Clamp(ballY, 0, Console.WindowHeight - 1); // Mantener dentro del rango
            }

            // Colisiones barra
            if (ballY + dy == barY && ballX + dx >= barX && ballX + dx <= barX + 16)
            {
                dy *= -1;
                barReboundSound.Play();
            }

            DrawBall(ballX, ballY);
        }


        public static void DrawBall(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("O");
        }

        public static void EraseBall(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(" ");
        }

        public static void Main()
        {
            barX = Console.WindowWidth / 2;
            barY = Console.WindowHeight - 3;
            ballX = Console.WindowWidth / 2;
            ballY = Console.WindowHeight / 2;

            Console.CursorVisible = false;

            bool[,] bricks = DrawBricks();

            DrawBar(barX, barY);

            while (running)
            {
                ConsoleKeyInfo? key = Console.KeyAvailable ? Console.ReadKey(true) : (ConsoleKeyInfo?)null;
                MoveBar(key);
                MoveBall();
                BrickCollision(rows, cols, bricks);
                Thread.Sleep(25);
            }
        }

    }
}